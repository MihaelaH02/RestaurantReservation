CREATE PROCEDURE ADD_DAILY_RESTAURANT_SCHEDULE
	@ADD_NEW_DATE		DATETIME,
	@WEEK_DAY			SMALLINT,
	@DELETE_PAST_DATE	DATETIME,
	@ERROR_CODE			INT				OUTPUT,
	@ERROR_MSG			NVARCHAR(MAX)   OUTPUT
AS
BEGIN TRY
	DECLARE @SQL_QUERY		 NVARCHAR(MAX)

	--Създаване на времмени таблици за обработка на резултата
	--------------------
	 SET @ERROR_CODE = 10;
     SET @ERROR_MSG = N'Грешка при създаване на времмени таблици за обработка на резултата.';

	 CREATE TABLE #TEMP_DAILY_SCHEDULE
	 (
		RESTAURANT_ID	SMALLINT	NOT NULL,
		HOUR_FROM		INT			NOT NULL,
		HOUR_TO			INT			NOT NULL,
		WORKING			TINYINT		NOT NULL
	 )

	CREATE TABLE #BULK_INSERT_SCHEDULE
	(
		DAY			DATETIME	NOT NULL,
		SHEME_ID	SMALLINT	NOT NULL,
		FREE_TABLES	INT			NOT NULL
	)

	--Подготовка на графика за всеки ресторант
	--------------------
	SET @ERROR_CODE = 20
	SET @ERROR_MSG = N'Грешка при подготовка на графика за всеки ресторант.'

	INSERT INTO #TEMP_DAILY_SCHEDULE
	SELECT
		RES.ID											 AS RESTAURANT_ID,
		ISNULL(SETTINGS.HOUR_FROM, 0)					 AS HOUR_FROM,
		ISNULL(SETTINGS.HOUR_TO, 0)						 AS HOUR_TO,
		IIF(dbo.CHECKBIT(COND.STATUS, 1) = 1, 0, 1)    AS WORKING /*STS_NOT_ACCEPT*/
	FROM RESTAURANTS AS RES WITH(NOLOCK)
	LEFT JOIN RESTAURANT_SCHEDULE_SETTINGS AS SETTINGS WITH(NOLOCK)
		ON SETTINGS.RESTAURANT_ID = RES.ID
	LEFT JOIN RESTAURANT_SPECIAL_CONDITIONS AS COND WITH(NOLOCK)
		ON COND.ID = ISNULL( SETTINGS.SPECIAL_CONDITION_ID, 0) 
	WHERE SETTINGS.WEEK_DAY = @WEEK_DAY
		AND ISNULL(SETTINGS.SPECIFIC_DATE, @ADD_NEW_DATE ) = @ADD_NEW_DATE


	--Задаване на дефолтен график за ресторантите без настройки
	--------------------
	SET @ERROR_CODE = 30
	SET @ERROR_MSG = N'Грешка при задаване на дефолтен график за ресторантите без настройки.'

	UPDATE #TEMP_DAILY_SCHEDULE
	SET HOUR_FROM = SETTINGS.HOUR_FROM,
		HOUR_TO = SETTINGS.HOUR_TO,
		WORKING = 1
	FROM #TEMP_DAILY_SCHEDULE AS TEMP
	JOIN RESTAURANT_SCHEDULE_SETTINGS AS SETTINGS WITH(NOLOCK)
		ON SETTINGS.RESTAURANT_ID = -1 /* Дефолтни настройки */
	WHERE TEMP.HOUR_FROM = 0 
		AND TEMP.HOUR_TO = 0


	--Генериране на часове за графика по всяка схема на всеки ресторант
	--------------------
	SET @ERROR_CODE = 40
	SET @ERROR_MSG = N'Грешка при генериране на часове за графика по всяка схема на всеки ресторант'

	INSERT INTO #BULK_INSERT_SCHEDULE
        SELECT
            @ADD_NEW_DATE 
				+ CAST(HH.HOURS AS DATETIME)
				+ CAST(MM.MINS AS DATETIME) AS DAY,
            SCHEME.ID						AS SHEME_ID,
            CASE 
				WHEN TEMP.WORKING = 1 
					THEN SCHEME.TABLE_COUNT 
				ELSE 0 
			END								AS FREE_TABLES
        FROM #TEMP_DAILY_SCHEDULE AS TEMP
        JOIN RESTAURANT_CAPACITY_SCHEME AS SCHEME WITH(NOLOCK)
            ON SCHEME.RESTAURANT_ID = TEMP.RESTAURANT_ID
		CROSS JOIN 
		(
			SELECT 0 AS HOURS UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL SELECT 4 
            UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9 
            UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12 UNION ALL SELECT 13 UNION ALL SELECT 14 
            UNION ALL SELECT 15 UNION ALL SELECT 16 UNION ALL SELECT 17 UNION ALL SELECT 18 UNION ALL SELECT 19 
            UNION ALL SELECT 20 UNION ALL SELECT 21 UNION ALL SELECT 22 UNION ALL SELECT 23
		) AS HH /* Часови диапазон */
		CROSS JOIN 
        (
            SELECT 0 AS MINS UNION ALL SELECT 30
        ) AS MM /* Минутен диапазан */
		WHERE HH.HOURS BETWEEN TEMP.HOUR_FROM 
						AND TEMP.HOUR_TO


	--Добавяне и изтриване на часове в графика на ресторанти
	--------------------
	BEGIN TRANSACTION
		SET @ERROR_CODE = 40
		SET @ERROR_MSG = N'Грешка при добавяне на график за нов ден.'
	
		INSERT INTO RESTAURANT_MONTHLY_SCHEDULE
		SELECT * FROM #BULK_INSERT_SCHEDULE

		SET @ERROR_CODE = 40
		SET @ERROR_MSG = N'Грешка при изтриване на график за изминал ден.'
		
		DELETE FROM RESTAURANT_MONTHLY_SCHEDULE
		WHERE DAY <= CONVERT( DATETIME, CONCAT(CONVERT(DATE, @DELETE_PAST_DATE), N' 23:30:00'), 120)
	COMMIT TRANSACTION

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION

	SET @ERROR_CODE = ERROR_NUMBER()
    SET @ERROR_MSG = ERROR_MESSAGE()
	RETURN
END CATCH
GO