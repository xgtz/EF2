create or replace package PK_STUDENT is

  TYPE a_CURSOR IS REF CURSOR;
  Procedure USP_STUDENT(a_BASE_XML IN CLOB, io_cursor IN OUT a_CURSOR);

end PK_STUDENT;
/
create or replace package body PK_STUDENT is

  Procedure USP_STUDENT(a_BASE_XML IN CLOB, io_cursor IN OUT a_CURSOR) AS
    v_ROWCOUNT      number; /*回传执行状态 >0:成功 <=0:失败*/
    v_ERRMSG        VARCHAR2(4000);
    v_ID            VARCHAR2(100);
    v_NAME          NVARCHAR2(200);
    v_ISMALE        NUMERIC(10, 0);
    v_STUDENTNUMBER NUMERIC(10, 0);
  BEGIN
    v_ROWCOUNT := 0;
  
    BEGIN
      SELECT ExtractValue(Value(p), '/ITEM/@ID') ID,
             ExtractValue(Value(p), '/ITEM/@NAME') NAME,
             ExtractValue(Value(p), '/ITEM/@ISMALE') ISMALE,
             ExtractValue(Value(p), '/ITEM/@STUDENTNUMBER') STUDENTNUMBER
        INTO v_ID, v_NAME, v_ISMALE, v_STUDENTNUMBER
        FROM TABLE(XMLSequence(Extract(XMLType(a_BASE_XML), '/DATA/ITEM'))) P;
    EXCEPTION
      WHEN OTHERS THEN
        v_ERRMSG := '解析基本信息(DATA)失败！' || SQLERRM;
    END;
  
    INSERT INTO STUDENT
      (ID, NAME, ISMALE, STUDENTNUMBER)
    VALUES
      (v_ID, v_NAME, v_ISMALE, v_STUDENTNUMBER);
    v_ROWCOUNT := v_ROWCOUNT + sql%rowcount;
    Commit;
    v_ERRMSG:='执行存储过程返回的游标';
    OPEN io_cursor FOR
      SELECT v_ROWCOUNT "ROWCOUNT", v_ERRMSG ERR_MSG FROM DUAL;
  
  END USP_STUDENT;
end PK_STUDENT;
/
