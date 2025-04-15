CREATE PROCEDURE ARCHIVATE_RESERVATIONS
	@ARCH_SERVER	NVARCHAR(32),
	@ARCH_DB_NAME	NVARCHAR(32),
	@ERROR_CODE		INT				OUTPUT,
	@ERROR_MSG		NVARCHAR(MAX)   OUTPUT
AS
BEGIN TRY
	DECLARE @SQL_QUERY		 NVARCHAR(MAX)
	DECLARE @YYYYMM			 NVARCHAR(6)
	DECLARE @ARCH_TABLE_NAME NVARCHAR(32)
	DECLARE @MONTH			 NVARCHAR(2) = MONTH( GETDATE() )

	--Проверка на връзката към архивния сървър
	--------------------
	SET @ERROR_CODE = 10;
    SET @ERROR_MSG = N'Грешка при проверка на връзката към архивния сървър ' + @ARCH_SERVER + N'.';

    DECLARE @TEST_LINKED_SERVER TABLE (srvname NVARCHAR(128));

    INSERT INTO @TEST_LINKED_SERVER 
		EXEC sp_testlinkedserver @ARCH_SERVER;

    IF NOT EXISTS ( SELECT * 
					FROM @TEST_LINKED_SERVER )
    BEGIN
        EXEC sp_addlinkedserver @SERVER = @ARCH_SERVER;
    END

	--Конструиране наименованието на таблицата по текуща дата
	--------------------
	SET @ERROR_CODE = 20
	SET @ERROR_MSG = N'Грешка при конструиране на наименованието на архивна таблицата според текуща дата.'

	SET @MONTH = IIF( LEN( @MONTH ) < 2, 
						CONCAT( N'0', @MONTH), 
						@MONTH )

	SET @YYYYMM  =  CONCAT( YEAR(GETDATE()), @MONTH)
    SET @ARCH_TABLE_NAME = CONCAT( N'RESERVATIONS', @YYYYMM )

   IF EXISTS ( SELECT * 
			   FROM SYS.TABLES WITH(NOLOCK)
			   WHERE NAME = @ARCH_TABLE_NAME)
    BEGIN
		BEGIN TRANSACTION

		--Определяме кои записи са за архивиране
		--------------------
		SET @ERROR_CODE = 30
		SET @ERROR_MSG = N'Грешка при извлизане на записи за архивиране от таблица RESERVATIONS от онлайн база.'

		SELECT * 
		INTO #TEMP_ARCH_RESERVATION
		FROM RESERVATIONS 
		WHERE DBO.CHECKBIT([STATUS], 8) = 1 /*STS_CLOSE*/

		IF EXISTS ( SELECT TOP 1 * 
					FROM #TEMP_ARCH_RESERVATION WITH(NOLOCK) )
		BEGIN
			--Добавяне на данни към архивна таблица
			--------------------
			SET @ERROR_CODE = 40
			SET @ERROR_MSG = N'Грешка при добавяне на записи в архивна таблица ' + @ARCH_TABLE_NAME + N' .'

			SET @SQL_QUERY = 'INSERT INTO [@@ARCH_SERVER@@].[@@ARCH_DB_NAME@@].DBO.[@@ARCH_DB_TABLE_NAME@@]
							  VALUES
							  SELECT * 
							  FROM @@TEMP_TABLE_NAME@@'

			SET @SQL_QUERY = REPLACE(@SQL_QUERY, N'@@ARCH_SERVER@@', @ARCH_SERVER)
			SET @SQL_QUERY = REPLACE(@SQL_QUERY, N'@@ARCH_DB_NAME@@', @ARCH_DB_NAME)
			SET @SQL_QUERY = REPLACE(@SQL_QUERY, N'@@ARCH_DB_TABLE_NAME@@', @ARCH_TABLE_NAME)
			SET @SQL_QUERY = REPLACE(@SQL_QUERY, N'@@TEMP_TABLE_NAME@@', N'#TEMP_ARCH_RESERVATION')

			EXEC sp_executesql @SQL_QUERY;

			--Изтриване на данни от онлайн таблица
			--------------------
			SET @ERROR_CODE = 50
			SET @ERROR_MSG = N'Грешка при изтриване на вече архивирани записи от таблица RESERVATIONS от онлайн база.'

			DELETE FROM RESERVATIONS
			WHERE DBO.CHECKBIT([STATUS], 8) = 1 /*STS_CLOSE*/

			COMMIT TRANSACTION
		END
	END
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	SET @ERROR_CODE = ERROR_NUMBER()
    SET @ERROR_MSG = ERROR_MESSAGE()
END CATCH
GO