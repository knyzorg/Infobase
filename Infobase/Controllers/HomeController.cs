﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infobase.Models;
using Models.Metadata;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Infobase.Controllers
{
    public class OpenController : Controller
    {
        private readonly Dictionary<string, SortedDictionary<Type, ICollection<dynamic>>> contexts;

        public OpenController(Dictionary<string, SortedDictionary<Type, ICollection<dynamic>>> contextLookup)
        {
            contexts = contextLookup;
        }
        public SortedDictionary<Type, ICollection<dynamic>> GetContext(string datatool) {
            return contexts[datatool + "Context"];
        }
        public async Task<IActionResult> Index(string datatool, string language)
        {
            var context = GetContext(datatool);
            var topLevelType = context.Keys.First();
            // var topLevelEntities = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new[] { topLevelType }).Invoke(typeof(Enumerable), new[] {  });
            // Load top-level
                return View($"{datatool}/index", context[topLevelType]);
        }

        [ActionName("data-tool")]
        public async Task<IActionResult> Datatool(string datatool, string language, int index = 1, bool api = false)
        {
            var context = GetContext(datatool);
            var dataBreakdownLevelType = context.Keys.SkipLast(1).Last();
            var disaggregatorLevelType = context.Keys.Last();
            var selectedBreakdown = context[dataBreakdownLevelType]
                .Where(s => (int)s.Index >= index)
                .First();
                
            var children = Enumerable.Cast<dynamic>((IEnumerable)Metadata
                                .FindPropertyOnType<ChildrenAttribute>(dataBreakdownLevelType)
                                .GetValue((object)selectedBreakdown));

            var measureDescription = Metadata
                            .FindTextPropertiesOnTree((object)selectedBreakdown, "en-ca", TextAppearance.MeasureDescription)
                            .Where(mp => !string.IsNullOrEmpty(mp.Name) && !string.IsNullOrEmpty(mp.Value.ToString()));

            var notes = Metadata
                            .FindTextPropertiesOnTree((object)selectedBreakdown, "en-ca", TextAppearance.Notes)
                            .Where(mp => !string.IsNullOrEmpty(mp.Name) && !string.IsNullOrEmpty(mp.Value.ToString()));

            PropertyInfo GetProperty<T>() where T: Attribute {
                return Metadata.FindPropertyOnType<T>(disaggregatorLevelType);
            }
            var averageValueProperty = GetProperty<PointAverageAttribute>();
            var lowerValueProperty = GetProperty<PointLowerAttribute>();
            var upperValueProperty = GetProperty<PointUpperAttribute>();
            var cVValueProperty = GetProperty<CVValueAttribute>();
            var cVInterpretationProperty = GetProperty<CVInterpretationAttribute>();
            var typeProperty = GetProperty<TypeAttribute>();
            
            ChartData chart = chart = new ChartData
            {
                XAxis = (string)Metadata.FindTextPropertiesOnNode<ShowOnAttribute>((object)selectedBreakdown, "en-ca", TextAppearance.Filter).First().Value,
                YAxis = (string)Metadata.FindTextPropertiesOnTree<UnitLongAttribute>((object)selectedBreakdown, "en-ca").First().Value,
                Unit = (string)Metadata.FindTextPropertiesOnTree<UnitShortAttribute>((object)selectedBreakdown, "en-ca").First().Value,
                Title = (string)Metadata.FindTextPropertiesOnTree<TitleAttribute>((object)selectedBreakdown, "en-ca").First().Value,
                Points = children.Select((child) => new Point {
                    Label = (string)Metadata.FindTextPropertiesOnTree<DataLabelChartAttribute>((object)child, "en-ca").First().Value,
                    Text = (string)Metadata.FindTextPropertiesOnTree<DataLabelTableAttribute>((object)child, "en-ca").First().Value,
                    CVInterpretation = cVInterpretationProperty.GetValue(child),
                    CVValue = cVValueProperty.GetValue(child),
                    Value = averageValueProperty.GetValue(child),
                    ValueLower = lowerValueProperty.GetValue(child),
                    ValueUpper = upperValueProperty.GetValue(child),
                    Type = typeProperty.GetValue(child)
                }).ToList(),
                WarningCV = null,
                SuppressCV = null,
                DescriptionTable = measureDescription.ToDictionary(mp => mp.Name, mp => (string)mp.Value),
                Notes = notes.ToDictionary(mp => mp.Name, mp => (string)mp.Value)
            };

            var cpm = new ChartPageModel(datatool, language, chart);

            var dropdowns = context.SkipLast(1).Select(pair =>
            {
                Type type = pair.Key;

                ICollection<dynamic> entities = pair.Value;

                var parentOfCurrentType = Metadata.GetAllParentNodes((object)selectedBreakdown).First(p => p.GetType() == type);

                var dropdownItems = entities
                    .Where(entity => Metadata.FindPropertyOnType<DefaultChildAttribute>(type).GetValue(entity) != null)
                    .Where(entity =>
                    {
                        var parentOfEntity = Metadata.GetParentOf(entity);
                        // Top-level selectors should always be displayed
                        if (parentOfEntity == null) return true;

                        // Check for common ancestor
                        return Metadata.GetParentOf(parentOfCurrentType) == parentOfEntity;
                    })
                    .Select(entity =>
                    {
                        var currentLevel = entity;
                        
                        var entityText = (string)Metadata.FindTextPropertiesOnNode<ShowOnAttribute>((object)entity, "en-ca", TextAppearance.Filter).First().Value;

                        while (currentLevel.GetType() != dataBreakdownLevelType)
                        {
                            currentLevel = Metadata.FindPropertyOnType<DefaultChildAttribute>(((object)currentLevel).GetType()).GetValue(currentLevel);
                            if (currentLevel == null) throw new Exception("Default tree structure is broken");
                        }
                        var entityIndex = currentLevel.Index;

                        return new { Text = entityText, Value = entityIndex, Entity = entity };

                    }).ToList();
                
                var filterName = Metadata.FindTextPropertiesOnNode<ShowOnAttribute>((object)dropdownItems.First().Entity, "en-ca", TextAppearance.Filter).First().Name;
                return new DropdownMenuModel(
                        filterName,
                        dropdownItems.Select(di => new DropdownItem { Text = di.Text, Value = di.Value }),
                        dropdownItems.First(di => di.Entity == parentOfCurrentType).Value
                    );
            });

            foreach (var dropdown in dropdowns)
            {
                cpm.Filters.Add(dropdown);
            };

            if (Request.Method == "GET" && !api)
                return View(cpm);
            else
                return Json(cpm);

        }

        [ActionName("indicator-details")]
        public async Task<IActionResult> Details(string datatool, string language, int id)
        {
            var context = GetContext(datatool);
            var measure = context[context.Keys.SkipLast(2).Last()].FirstOrDefault(m => m.Index == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(new DescriptionPageModel(datatool, language, measure));
        }

        [ActionName("publications")]
        public IActionResult Publications(string datatool, string language, int id)
        {
            return View($"{datatool}/publications");
        }
    }
}
