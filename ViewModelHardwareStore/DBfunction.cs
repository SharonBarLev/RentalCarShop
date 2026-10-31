using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace ViewModelCarShop
{
    public class DBfunction
    {
        private string connectionstr;
        public DBfunction()
        {
            //C:\Users\Magsihim_AI\Downloads\CarShop\ViewModelCarShop\App_Data\CarShop.accdb

            //this.connectionstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory+"/App_Data/Database4.accdb";
            this.connectionstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Magsihim_AI\Downloads\CarShop\ViewModelCarShop\App_Data\CarShop.accdb";
        }

        // update, insert , delete 
        public void DoCommand(string mysql)
        {
            OleDbConnection connectObj = new OleDbConnection(connectionstr);
            OleDbCommand myCommandObj = new OleDbCommand(mysql, connectObj);
            connectObj.Open();
            myCommandObj.ExecuteNonQuery();
            connectObj.Close();
        }

        // select
        public DataSet Selectcommand(string mysql)
        {
            OleDbConnection connectObj = new OleDbConnection(connectionstr);
            OleDbDataAdapter da = new OleDbDataAdapter(mysql, connectObj);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}

