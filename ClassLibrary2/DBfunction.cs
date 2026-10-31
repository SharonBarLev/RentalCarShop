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
            this.connectionstr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\0000.Maabada.Profile\Downloads\nemer26-27\CarShop\ClassLibrary2\App_Data\CarShop.accdb";
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

        public void DoCommandInjection(OleDbCommand cmd)
        {
            OleDbConnection connectObj = new OleDbConnection(connectionstr);

            cmd.Connection = connectObj;

            connectObj.Open();

            cmd.ExecuteNonQuery();

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


        public DataSet SelectcommandInjection(OleDbCommand cmd)
        {
            OleDbConnection connectObj = new OleDbConnection(connectionstr);

            cmd.Connection = connectObj;

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
    }
}

