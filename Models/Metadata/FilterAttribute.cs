using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Models.Metadata
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class FilterAttribute : Attribute
    {
        public int Level { get; set; }
        public FilterAttribute(int level)
        {
            Level = level;
        }
    }
}