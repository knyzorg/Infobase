/**
PASS-DB Normalization Script v2
 - Improved to allow after-the-fact-insertions
 - No longer expanding schema for joins
 - No longer using temporary tables
**/

insert into Activity ([Index])
	select min(id) from master group by Activity;


insert into indicatorGroup([ActivityId], [Index])
	select
		ActivityId,
		min(masterA.id)
	from
		master masterA, 
		master masterB,
		Activity
	where 
		masterB.id = [Index] and 
		masterB.Activity = mastera.Activity
	group by 
		ActivityId,
		MasterA.[Indicator Group];


insert into lifeCourse([IndicatorGroupId], [Index])
	select
		IndicatorGroupId,
		min(masterA.id)
	from
		master masterA, 
		master masterB,
		IndicatorGroup
	where 
		masterB.id = IndicatorGroup.[Index] and 
		masterB.[Indicator Group] = mastera.[Indicator Group] and
		masterb.Activity = mastera.Activity
	group by 
		IndicatorGroupId,
		masterA.[Life Course];

insert into Indicator([LifeCourseId], [Index])
	select
		LifeCourseId,
		min(masterA.id)
	from
		master masterA, 
		master masterB,
		LifeCourse
	where 
		masterB.id = LifeCourse.[Index] and 
		masterB.[Life Course] = mastera.[Life Course] and
		masterB.[Indicator Group] = mastera.[Indicator Group] and
		masterb.Activity = mastera.Activity
	group by 
		LifeCourseId,
		masterA.Indicator;

insert into Measure([IndicatorId], [Index])
	select
		IndicatorId,		
		min(masterA.id)
	from
		master masterA, 
		master masterB,
		Indicator
	where 
		masterB.id = Indicator.[Index] and 
		masterB.Indicator = mastera.Indicator and
		masterB.[Life Course] = mastera.[Life Course] and
		masterB.[Indicator Group] = mastera.[Indicator Group] and
		masterb.Activity = mastera.Activity
	group by 
		IndicatorId,
		masterA.[Specific Measure 1];

insert into Strata([MeasureId], [Index])
select
		MeasureId,		
		min(masterA.id)
	from
		master masterA, 
		master masterB,
		Measure
	where 
		masterB.id = Measure.[Index] and 
		masterB.[Specific Measure 1] = mastera.[Specific Measure 1] and
		masterB.Indicator = mastera.Indicator and
		masterB.[Life Course] = mastera.[Life Course] and
		masterB.[Indicator Group] = mastera.[Indicator Group] and
		masterb.Activity = mastera.Activity and
		masterb.Include_DT = 'Y'
	group by 
		MeasureId,
		masterA.[Data Breakdowns];


insert into Point(StrataId, CVInterpretation, CVValue, ValueAverage, ValueUpper, ValueLower, [Type], [Index])
	select
		StrataId,
		masterA.CV_Interpretation,
		masterA.CV_1,
		masterA.Data,	
		masterA.CI_Upper_95,
		masterA.CI_low_95,
		isnull(masterA.[Display Data], 0),
		masterA.id
	from
		master masterA, 
		master masterB,
		Strata
	where 
		masterB.id = Strata.[Index] and 
		masterB.[Data Breakdowns] = mastera.[Data Breakdowns] and
		masterB.[Specific Measure 1] = mastera.[Specific Measure 1] and
		masterB.Indicator = mastera.Indicator and
		masterB.[Life Course] = mastera.[Life Course] and
		masterB.[Indicator Group] = mastera.[Indicator Group] and
		masterb.Activity = mastera.Activity and
		masterb.Include_DT = 'Y';
		
update
	Activity
set 
	DefaultIndicatorGroupId = IndicatorGroupId
from IndicatorGroup where IndicatorGroup.[Index] = Activity.[Index];

update
	IndicatorGroup
set 
	DefaultLifeCourseId = LifeCourseId
from LifeCourse where LifeCourse.[Index] = IndicatorGroup.[Index];

update
	LifeCourse
set 
	DefaultIndicatorId = IndicatorId
from Indicator where Indicator.[Index] = LifeCourse.[Index];

update
	Indicator
set 
	DefaultMeasureId = Measureid
from Measure where Measure.[Index] = Indicator.[Index];

update
	Measure
set 
	DefaultStrataId = StrataId
from Strata where Strata.[Index] = Measure.[Index];



/* Prepare translation table */

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Activity] FROM Master A
		WHERE  A.[Activity] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Indicator Group] FROM Master A
		WHERE  A.[Indicator Group] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Life Course] FROM Master A
		WHERE  A.[Life Course] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Indicator] FROM Master A
		WHERE  A.[Indicator] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Specific Measure 1], [Type]='Datatool' FROM Master A
		WHERE  A.[Specific Measure 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Datatool')	

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Specific Measure 2], [Type]='Index' FROM Master A
		WHERE  A.[Specific Measure 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Index')	

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Data Breakdowns] FROM Master A
		WHERE  A.[Data Breakdowns] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)			

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Population 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Population 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)				

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Population 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Population 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)				

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Data Source 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Data Source 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)					

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Data Source 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Data Source 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)					

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Data Source 3], [Type] = 'Measure' FROM Master A
		WHERE  A.[Data Source 3] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)		

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Disaggregation], 'Datatool' FROM Master A
		WHERE  A.[Disaggregation] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Unit Label 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Unit Label 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Datatool')
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Unit Label 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Unit Label 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Index')
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Estimate Calculation], [Type] = 'Index' FROM Master A
		WHERE  A.[Estimate Calculation] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Measure')

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Additional Remarks], [Type] = 'Index' FROM Master A
		WHERE  A.[Additional Remarks] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Measure')

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Data Available], [Type] = 'Index' FROM Master A
		WHERE  A.[Data Available] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Measure')
		

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Notes] FROM Master A
		WHERE  A.[Notes] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'en-ca', [Additional Remarks] FROM Master A
		WHERE  A.[Notes] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Defintion], [Type] = 'Measure' FROM Master A
		WHERE  A.[Defintion] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Measure')

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'en-ca', [Population 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Population 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Datatool')

		
insert into ActivityNameTranslation(activityid, translationid)
select activityid, translationid from [master]
inner join activity on [Index] = [master].id
inner join translation on activity = [Text];

insert into IndicatorGroupNameTranslation(indicatorgroupid, translationid)
select indicatorGroupid, translationid from [master]
inner join indicatorGroup on [Index] = [master].id
inner join translation on [Indicator Group] = [Text];

insert into LifeCourseNameTranslation(lifecourseid, translationid)
select lifecourseid, translationid from [master]
inner join lifecourse on [Index] = [master].id
inner join translation on [Life Course] = [Text];

insert into MeasureAdditionalRemarksTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Additional Remarks] = [Text];

insert into MeasureDataAvailableTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Data Available] = [Text];

insert into MeasureDefinitionTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on Defintion = [Text];

insert into MeasureMethodTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Estimate Calculation] = [Text];

insert into MeasureNameTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Specific Measure 1] = [Text] or [Specific Measure 2] = [Text];

insert into MeasurePopulationTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Population 2] = [Text];

insert into MeasureSourceTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Data Source 2] = [Text] or [Data Source 3] = [Text];

insert into MeasureUnitTranslation(measureid, translationid)
select measureid, translationid from [master]
inner join measure on [Index] = [master].id
inner join translation on [Unit Label 1] = [Text] or [Unit Label 2] = [Text];

insert into IndicatorNameTranslation(indicatorid, translationid)
select indicatorid, translationid from [master]
inner join indicator on [Index] = [master].id
inner join translation on [Indicator] = [Text];


insert into StrataNameTranslation(strataid, translationid)
select strataid, translationid from [master]
inner join strata on [Index] = [master].id
inner join translation on [Data Breakdowns] = [Text];

insert into StrataNotesTranslation(strataid, translationid)
select strataid, translationid from [master]
inner join strata on [Index] = [master].id
inner join translation on Notes = [Text];

insert into StrataPopulationTranslation(strataid, translationid)
select strataid, translationid from [master]
inner join strata on [Index] = [master].id
inner join translation on [Population 1] = [Text];

insert into StrataSourceTranslation(strataid, translationid)
select strataid, translationid from [master]
inner join strata on [Index] = [master].id
inner join translation on [Data Source 1] = [Text];

insert into PointLabelTranslation(pointid, translationid)
select pointid, translationid from [master]
inner join point on [Index] = [master].id
inner join translation on Disaggregation = [Text];