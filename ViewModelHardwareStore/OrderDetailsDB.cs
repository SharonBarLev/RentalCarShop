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
    public class OrderDetailsDB
    {
        public OrderDetailsDB() { }
        public DBfunction db = new DBfunction();

        // isExistOrderDetails
        public bool isExistOrderDetails(int OrderDetailsID)
        {
            string sql = string.Format("select * from [OrderDetails] where [OrderDetailsID]={0}", OrderDetailsID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddOrderDetails
        public void AddOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice)
        {
            string sql = string.Format(
                "insert into [OrderDetails] ([OrderID], [ProductID], [Quantity], [UnitPrice]) values ({0}, {1}, {2}, {3})",
                OrderID, ProductID, Quantity, UnitPrice);
            db.DoCommand(sql);
        }

        // DeleteOrderDetails
        public void DeleteOrderDetails(int OrderDetailsID)
        {
            string sql = string.Format("delete from [OrderDetails] where [OrderDetailsID]={0}", OrderDetailsID);
            db.DoCommand(sql);
        }

        // UpdateOrderDetails
        public void UpdateOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice, int OrderDetailsID)
        {
            string sql = string.Format(
                "update [Category] set [OrderID]={0}, [ProductID]={1}, [Quantity]={2}, [UnitPrice]={3} where [OrderDetailsID]={2}",
                OrderID, ProductID, Quantity, UnitPrice, OrderDetailsID);
            db.DoCommand(sql);
        }

        // showAllOrderDetails
        public DataSet showAllOrderDetails()
        {
            string sql = "select * from [OrderDetails]";
            return db.Selectcommand(sql);
        }

        // showOrderDetails
        public DataSet showOrderDetails(int OrderDetailsID)
        {
            string sql = string.Format("select * from [OrderDetails] where [OrderDetailsID]={0}", OrderDetailsID);
            return db.Selectcommand(sql);
        }
    }
}

