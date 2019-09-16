create or replace package PK_TEACHER is

  Procedure USP_TEACHER(a_BASE_XML IN CLOB,
                        o_COUNT    IN OUT NUMERIC);

end PK_TEACHER;
/
create or replace package body PK_TEACHER is

  Procedure USP_TEACHER(a_BASE_XML IN CLOB,
                        o_COUNT    IN OUT NUMERIC) AS
    v_ID   VARCHAR2(100);
    v_NAME NVARCHAR2(200);
  BEGIN
    o_COUNT:=0;
    BEGIN
      SELECT ExtractValue(Value(p), '/ITEM/@ID') ID,
             ExtractValue(Value(p), '/ITEM/@NAME') NAME
        INTO v_ID, v_NAME 
        FROM TABLE(XMLSequence(Extract(XMLType(a_BASE_XML), '/DATA/ITEM'))) P;
    EXCEPTION
      WHEN OTHERS THEN
        --o_MSG := '����������Ϣ(DATA)ʧ�ܣ�' || SQLERRM;
        NULL;
    END;
    
    INSERT INTO TEACHER(ID,NAME) VALUES(v_ID,v_NAME);
    
    o_COUNT := o_COUNT + sql%rowcount;
    --o_MSG:='ִ�гɹ�û�д�����Ϣ';
    COMMIT;
    
  END;
end PK_TEACHER;
/