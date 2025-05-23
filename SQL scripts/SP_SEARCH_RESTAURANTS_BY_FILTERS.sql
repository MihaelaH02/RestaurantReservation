CREATE PROCEDURE SP_SEARCH_RESTAURANTS_BY_FILTERS
    @UUID UNIQUEIDENTIFIER
AS
BEGIN
    DECLARE @ATHMOSPHERE	TABLE (ID SMALLINT);
    DECLARE @LOCATIONS		TABLE (ID SMALLINT);
    DECLARE @DATES			TABLE (Day DATETIME);
	DECLARE @RATING			FLOAT

    INSERT INTO @ATHMOSPHERE
		SELECT CAST(FILTER_VALUE AS SMALLINT)
		FROM ARG_RESTAURANT_SEARCH_FILTERS WITH(NOLOCK)
		WHERE UUID = @UUID 
			AND FILER_TYPE = 1 /* Atmosphere */

    INSERT INTO @LOCATIONS
		SELECT CAST(FILTER_VALUE AS SMALLINT)
		FROM ARG_RESTAURANT_SEARCH_FILTERS WITH(NOLOCK)
		WHERE UUID = @UUID 
			AND FILER_TYPE = 2 /* Location */

    INSERT INTO @DATES
		SELECT CAST(FILTER_VALUE AS DATETIME)
		FROM ARG_RESTAURANT_SEARCH_FILTERS WITH(NOLOCK)
		WHERE UUID = @UUID 
			AND FILER_TYPE = 3 /* Date */

		SELECT @RATING CAST(FILTER_VALUE AS DATETIME)
		FROM ARG_RESTAURANT_SEARCH_FILTERS WITH(NOLOCK)
		WHERE UUID = @UUID 
			AND FILER_TYPE = 4 /* Rating */

    -- ??????? ??????
    SELECT DISTINCT 
		RES.ID, 
		RES.COMPANY_NAME, 
		RES.ADDRESS,
		RES.RATING,
		RES.ATMOSPHERE_ID
    FROM Restaurants AS RES WITH(NOLOCK)
	JOIN RESTAURANT_MONTHLY_SCHEDULE AS SCHEDULE WITH(NOLOCK)
		ON SCHEDULE.SHEME_ID = r.ID
    LEFT JOIN RestaurantScheduleSettings ss 
		ON ss.RESTAURANT_ID = r.ID AND ss.SPECIFIC_DATE IN (SELECT Day FROM @Dates)
    WHERE 
        m.DAY IN (SELECT Day FROM @Dates)
        AND m.FREE_TABLES > 0
        AND (@uuid IS NULL OR r.ATMOSPHERE_ID IN (SELECT Id FROM @Atmospheres))
        AND (r.ADDRESS IN (SELECT Name FROM @Locations))
        AND (ss.ID IS NULL OR ss.SPECIAL_CONDITION_ID IS NULL)
END
