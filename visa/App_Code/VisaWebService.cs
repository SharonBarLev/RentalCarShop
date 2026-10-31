using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Configuration;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VisaWebService : System.Web.Services.WebService
{
    public string connectionStr;

    public VisaWebService()
    {
        connectionStr = @"Provider=Microsoft.ace.OleDb.12.0;Data Source=" + Server.MapPath("App_Data/VisaDB.accdb");
    }

    [WebMethod]
    public void UpdateTotal(string visa, double money)
    {
        // הגנה מפני SQL Injection באמצעות פרמטרים ושימוש בשם העמודה המדויק CardNumber
        string query = "UPDATE Visa SET Total = Total - ? WHERE CardNumber = ?";

        using (OleDbConnection con = new OleDbConnection(connectionStr))
        {
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                // הוספת הפרמטרים לפי סדר הופעת סימני השאלה בשאילתה
                cmd.Parameters.AddWithValue("@money", money);
                cmd.Parameters.AddWithValue("@visa", visa);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }

    [WebMethod]
    public bool isValidMoney(string visa, double money)
    {
        // הגנה מפני SQL Injection ושימוש בשמות העמודות המדויקים CardNumber, Total, Range
        string query = "SELECT * FROM Visa WHERE CardNumber = ? AND (Total - ? >= [Range])";

        using (OleDbConnection con = new OleDbConnection(connectionStr))
        {
            using (OleDbCommand cmd = new OleDbCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@visa", visa);
                cmd.Parameters.AddWithValue("@money", money);

                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    return (ds.Tables[0].Rows.Count > 0);
                }
            }
        }
    }

    [WebMethod]
    public bool isCardExist(string visa, string holdername, string securitycode, int month, int year)
    {
        // הגנה מלאה מ-SQL Injection והתאמה מושלמת לשמות העמודות המדויקים של ה-Access שלך
        string sql = "SELECT * FROM Visa WHERE HolderName = ? AND CardNumber = ? AND SecurityCode = ? AND [Year] = ? AND [Month] = ?";

        using (OleDbConnection con = new OleDbConnection(connectionStr))
        {
            using (OleDbCommand cmd = new OleDbCommand(sql, con))
            {
                // קריטי ב-Access: הפרמטרים מוזנים בדיוק לפי סדר סימני השאלה בשאילתה למעלה!
                cmd.Parameters.AddWithValue("@HolderName", holdername);
                cmd.Parameters.AddWithValue("@CardNumber", visa);
                cmd.Parameters.AddWithValue("@SecurityCode", securitycode);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Month", month);

                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return (ds.Tables[0].Rows.Count > 0);
                }
            }
        }
    }
}