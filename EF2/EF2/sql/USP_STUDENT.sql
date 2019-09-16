use [CodeFirst]
go
if OBJECT_ID(N'dbo.USP_STUDENT') IS NOT NULL 
	DROP PROC dbo.USP_STUDENT
go
CREATE PROCEDURE USP_STUDENT
  @BASIC_XML XML
  AS
BEGIN
	SET NOCOUNT ON;
    SET ARITHABORT ON;
	BEGIN TRY
	
		DECLARE @ROWCOUNT AS INT;	/*�ش�ִ��״̬ >0:�ɹ� <=0:ʧ��*/
		DECLARE @ERRMSG AS NVARCHAR(MAX),	/*�ش�ִ��ʧ��ѶϢ*/
                
				@ID			VARCHAR(10),
				@NAME		VARCHAR(50),
				@ISMALE			VARCHAR(50),
				@STUDENTNUMBER	VARCHAR(50)
                
		SET @ROWCOUNT = 0;
		SET @ERRMSG='';


		--XML��Ϣ����
		SELECT TOP 1 @ID = xmlTB.Col.value('@ID','VARCHAR(50)')
		            ,@NAME = xmlTB.Col.value('@NAME','VARCHAR(200)')
		            ,@ISMALE = xmlTB.Col.value('@ISMALE','VARCHAR(50)')
		            ,@STUDENTNUMBER = xmlTB.Col.value('@STUDENTNUMBER','VARCHAR(50)')
		  FROM @BASIC_XML.nodes('//DATA/ITEM') xmlTB(Col)
		
		  INSERT INTO STUDENT
			  (ID, NAME, ISMALE, STUDENTNUMBER)
			VALUES
			  (@ID, @NAME, @ISMALE, @STUDENTNUMBER);
        SET @ROWCOUNT = @ROWCOUNT + @@ROWCOUNT;  
		GOTO OK;
	


	END TRY
	BEGIN CATCH
		SET @ROWCOUNT = 0;
		SET @ERRMSG = ERROR_MESSAGE();
		GOTO ERROR;
	END CATCH  

	ERROR:
	    IF (@ERRMSG = '')  SET @ERRMSG = 'ִ�д���';
	OK:
		IF (@ROWCOUNT = 0 AND @ERRMSG = '') SET @ERRMSG = '�����춯֮����';
    
     SET @ERRMSG = '�����춯֮����'
     SELECT @ROWCOUNT [ROWCOUNT], @ERRMSG [ERR_MSG]
      
	SET NOCOUNT OFF;
END

	