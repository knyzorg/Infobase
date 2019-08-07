// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Contexts.PASS2;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190807145612_PASS2cvp0akof.j0k")]
    partial class PASS2cvp0akofj0k
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Models.Contexts.PASS2.ColActivity", b =>
                {
                    b.Property<int>("ColActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColActivityNameEn");

                    b.Property<string>("ColActivityNameFr");

                    b.Property<int?>("DefaultColIndicatorGroupId");

                    b.Property<int>("Index");

                    b.HasKey("ColActivityId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultColIndicatorGroupId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColDataBreakdowns", b =>
                {
                    b.Property<int>("ColDataBreakdownsId")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("CVRangeLower");

                    b.Property<double?>("CVRangeUpper");

                    b.Property<string>("ColDataBreakdownsNameEn");

                    b.Property<string>("ColDataBreakdownsNameFr");

                    b.Property<int>("ColSpecificMeasureId");

                    b.Property<int?>("DefaultColStrataId");

                    b.Property<int>("Index");

                    b.Property<string>("UnitLong");

                    b.Property<string>("UnitShort");

                    b.HasKey("ColDataBreakdownsId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColSpecificMeasureId");

                    b.HasIndex("DefaultColStrataId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColIndicator", b =>
                {
                    b.Property<int>("ColIndicatorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColIndicatorNameEn");

                    b.Property<string>("ColIndicatorNameFr");

                    b.Property<int>("ColLifeCourseId");

                    b.Property<int?>("DefaultColSpecificMeasureId");

                    b.Property<int>("Index");

                    b.HasKey("ColIndicatorId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColLifeCourseId");

                    b.HasIndex("DefaultColSpecificMeasureId");

                    b.ToTable("Indicator");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColIndicatorGroup", b =>
                {
                    b.Property<int>("ColIndicatorGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColActivityId");

                    b.Property<string>("ColIndicatorGroupNameEn");

                    b.Property<string>("ColIndicatorGroupNameFr");

                    b.Property<int?>("DefaultColLifeCourseId");

                    b.Property<int>("Index");

                    b.HasKey("ColIndicatorGroupId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColActivityId");

                    b.HasIndex("DefaultColLifeCourseId");

                    b.ToTable("IndicatorGroup");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColLifeCourse", b =>
                {
                    b.Property<int>("ColLifeCourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColIndicatorGroupId");

                    b.Property<string>("ColLifeCourseNameEn");

                    b.Property<string>("ColLifeCourseNameFr");

                    b.Property<int?>("DefaultColIndicatorId");

                    b.Property<int>("Index");

                    b.HasKey("ColLifeCourseId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColIndicatorGroupId");

                    b.HasIndex("DefaultColIndicatorId");

                    b.ToTable("LifeCourse");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColSpecificMeasure", b =>
                {
                    b.Property<int>("ColSpecificMeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColIndicatorId");

                    b.Property<string>("ColSpecificMeasureNameEn");

                    b.Property<string>("ColSpecificMeasureNameFr");

                    b.Property<int?>("DefaultColDataBreakdownsId");

                    b.Property<bool>("Include");

                    b.Property<int>("Index");

                    b.Property<bool>("IsAggregator");

                    b.HasKey("ColSpecificMeasureId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColIndicatorId");

                    b.HasIndex("DefaultColDataBreakdownsId");

                    b.ToTable("Measure");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColStrata", b =>
                {
                    b.Property<int>("ColStrataId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CVInterpretation");

                    b.Property<double?>("CVValue");

                    b.Property<int>("ColDataBreakdownsId");

                    b.Property<string>("ColStrataNameEn");

                    b.Property<string>("ColStrataNameFr");

                    b.Property<int>("Index");

                    b.Property<double?>("ValueAverage");

                    b.Property<double?>("ValueLower");

                    b.Property<double?>("ValueUpper");

                    b.HasKey("ColStrataId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColDataBreakdownsId");

                    b.ToTable("Strata");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Master", b =>
                {
                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColActivity");

                    b.Property<string>("ColAdditionalRemarks");

                    b.Property<string>("ColCILow95");

                    b.Property<string>("ColCIUpper95");

                    b.Property<string>("ColCV");

                    b.Property<string>("ColCVInterpretation");

                    b.Property<string>("ColCVRangeLower");

                    b.Property<string>("ColCVRangeUpper");

                    b.Property<string>("ColData");

                    b.Property<string>("ColDataAvailable");

                    b.Property<string>("ColDataBreakdowns");

                    b.Property<string>("ColDataSource1");

                    b.Property<string>("ColDataSource2");

                    b.Property<string>("ColDataSource3");

                    b.Property<string>("ColDefintion");

                    b.Property<string>("ColEstimateCalculation");

                    b.Property<string>("ColFeatureData");

                    b.Property<string>("ColIndicator");

                    b.Property<string>("ColIndicatorGroup");

                    b.Property<string>("ColIsAggregator");

                    b.Property<string>("ColIsIncluded");

                    b.Property<string>("ColLifeCourse");

                    b.Property<string>("ColNotes");

                    b.Property<string>("ColPTTableLabel");

                    b.Property<string>("ColPopulation1");

                    b.Property<string>("ColPopulation2");

                    b.Property<string>("ColSpecificMeasure");

                    b.Property<string>("ColSpecificMeasure2");

                    b.Property<string>("ColStrata");

                    b.Property<string>("ColUnitLabel2");

                    b.Property<string>("ColUnitLabelLong");

                    b.HasKey("Index");

                    b.ToTable("Master");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColActivity", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColIndicatorGroup", "DefaultColIndicatorGroup")
                        .WithMany()
                        .HasForeignKey("DefaultColIndicatorGroupId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColDataBreakdowns", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColSpecificMeasure", "ColSpecificMeasure")
                        .WithMany("ColDataBreakdowns")
                        .HasForeignKey("ColSpecificMeasureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.PASS2.ColStrata", "DefaultColStrata")
                        .WithMany()
                        .HasForeignKey("DefaultColStrataId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColIndicator", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColLifeCourse", "ColLifeCourse")
                        .WithMany("ColIndicators")
                        .HasForeignKey("ColLifeCourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.PASS2.ColSpecificMeasure", "DefaultColSpecificMeasure")
                        .WithMany()
                        .HasForeignKey("DefaultColSpecificMeasureId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColIndicatorGroup", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColActivity", "ColActivity")
                        .WithMany("ColIndicatorGroups")
                        .HasForeignKey("ColActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.PASS2.ColLifeCourse", "DefaultColLifeCourse")
                        .WithMany()
                        .HasForeignKey("DefaultColLifeCourseId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColLifeCourse", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColIndicatorGroup", "ColIndicatorGroup")
                        .WithMany("ColLifeCourses")
                        .HasForeignKey("ColIndicatorGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.PASS2.ColIndicator", "DefaultColIndicator")
                        .WithMany()
                        .HasForeignKey("DefaultColIndicatorId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColSpecificMeasure", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColIndicator", "ColIndicator")
                        .WithMany("ColSpecificMeasures")
                        .HasForeignKey("ColIndicatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.PASS2.ColDataBreakdowns", "DefaultColDataBreakdowns")
                        .WithMany()
                        .HasForeignKey("DefaultColDataBreakdownsId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.ColStrata", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.ColDataBreakdowns", "ColDataBreakdowns")
                        .WithMany("ColStrata")
                        .HasForeignKey("ColDataBreakdownsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
