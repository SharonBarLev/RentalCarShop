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
    public class OrderDB
    {
        public OrderDB() { }
        public DBfunction db = new DBfunction();

        // isExistOrder
        public bool isExistOrder(int OrderID)
        {
            string sql = string.Format("select * from [Order] where [OrderID]={0}", OrderID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddOrder
        public void AddOrder(string Username, DateTime OrderDate, decimal TotalPrice, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            string Address = CountryRegion + ", " + City + ", " + StreetAddress + ", " + Floor + ", " + ZipCode;

            string sql = string.Format(
                "INSERT INTO [Order] ([Username], [OrderDate], [TotalPrice], [Address], [AdditionalInstructions]) " +
                "VALUES ('{0}', #{1}#, {2}, '{3}', '{4}')",
                Username, OrderDate.ToString("yyyy-MM-dd"), TotalPrice, Address, AdditionalInstructions);

            db.DoCommand(sql);
        }


        // DeleteOrder
        public void DeleteOrder(int OrderID)
        {
            string sql = string.Format("delete from [Order] where [OrderID]={0}", OrderID);
            db.DoCommand(sql);
        }

        // UpdateOrder
        public void UpdateOrder(string Username, DateTime OrderDate, decimal TotalPrice, string Address, string AdditionalInstructions, int OrderID)
        {
            string sql = string.Format(
                "update [Order] set [Username]='{0}', [OrderDate]=#{1}#, [TotalPrice]={2}, [Address]='{3}', [AdditionalInstructions]='{4}' where [OrderID]={5}",
                Username, OrderDate.ToString("yyyy-MM-dd"), TotalPrice, Address, AdditionalInstructions, OrderID);
            db.DoCommand(sql);
        }

        // showAllOrders
        public DataSet showAllOrders()
        {
            string sql = "select * from [Order]";
            return db.Selectcommand(sql);
        }

        // showOrder
        public DataSet showOrder(int OrderID)
        {
            string sql = string.Format("select * from [Order] where [OrderID]={0}", OrderID);
            return db.Selectcommand(sql);
        }
    }
}

