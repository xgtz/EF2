use [CodeFirst]
go
if OBJECT_ID(N'dbo.USP_TEACHER') IS NOT NULL 
	DROP PROC dbo.USP_TEACHER
go
CREATE PROCEDURE USP_TEACHER
  @BASIC_XML XML,
  @COUNT     INT OUT,
  @MSG       VARCHAR OUT
  AS
BEGIN
	SET NOCOUNT ON;
    SET ARITHABORT ON;
	BEGIN TRY

		DECLARE @ID			VARCHAR(10),
				@NAME		VARCHAR(50)
                
		SET @COUNT = 0;
		SET @MSG='';


		--XML信息解析
		SELECT TOP 1 @ID = xmlTB.Col.value('@ID','VARCHAR(50)')
		            ,@NAME = xmlTB.Col.value('@NAME','VARCHAR(200)')
		  FROM @BASIC_XML.nodes('//DATA/ITEM') xmlTB(Col)
		
		  INSERT INTO TEACHER(ID, NAME)
			VALUES  (@ID, @NAME );
        SET @COUNT = @COUNT + @@ROWCOUNT;  
		GOTO OK;
	


	END TRY
	BEGIN CATCH
		SET @COUNT = 0;
		SET @MSG = ERROR_MESSAGE();
		GOTO ERROR;
	END CATCH  

	ERROR:
	    IF (@MSG = '')  SET @MSG = '执行错误';
	OK:
		IF (@COUNT = 0 AND @MSG = '') SET @MSG = '查无异动之资料';
    
	SET @MSG='没有错误信息';

      
	SET NOCOUNT OFF;
END

	