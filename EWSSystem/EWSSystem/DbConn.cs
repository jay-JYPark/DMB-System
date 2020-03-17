using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADEng.Library;
using System.Configuration;
using System.Diagnostics;
using TextLog;

/// <summary>
/// DbConn의 요약 설명입니다.
/// </summary>
public class DbConn
{
    //오라클 접속 
    public static oracleDAC odec { get; set; }

    public DbConn()
    {
        //
        // TODO: 여기에 생성자 논리를 추가합니다.
        //
    }

    public static oracleDAC GetDbConn()
    {
        try
        {
            if (odec == null)
            {
                odec = new ADEng.Library.oracleDAC(ConfigurationManager.AppSettings["OraId"]
                                                                        , ConfigurationManager.AppSettings["OraPw"]
                                                                        , ConfigurationManager.AppSettings["OraIp"]
                                                                        , ConfigurationManager.AppSettings["OraPort"]
                                                                        , ConfigurationManager.AppSettings["OraSid"]);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("EWSSystem.DbConn.GetDbConn()| " + ex.Message);
            Log.WriteLog("EWSSystem.DbConn.GetDbConn()| " + ex.Message);
        }

        return odec;
    }
}