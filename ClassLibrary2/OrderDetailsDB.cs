using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class OrderDetailsDB
    {
        public OrderDetailsDB() { }
        public DBfunction db = new DBfunction();

        // isExistOrderDetails
        public bool isExistOrderDetails(int OrderDetailsID)
        {
            string sql = "SELECT * FROM [OrderDetails] WHERE [OrderDetailsID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderDetailsID);

            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // AddOrderDetails
        public void AddOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice)
        {
            string sql = "INSERT INTO [OrderDetails] ([OrderID], [ProductID], [Quantity], [UnitPrice]) VALUES (?, ?, ?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderID);
            cmd.Parameters.AddWithValue("?", ProductID);
            cmd.Parameters.AddWithValue("?", Quantity);
            cmd.Parameters.AddWithValue("?", UnitPrice);

            db.DoCommandInjection(cmd);
        }

        // DeleteOrderDetails
        public void DeleteOrderDetails(int OrderDetailsID)
        {
            string sql = "DELETE FROM [OrderDetails] WHERE [OrderDetailsID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderDetailsID);

            db.DoCommandInjection(cmd);
        }

        // UpdateOrderDetails
        public void UpdateOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice, int OrderDetailsID)
        {
            string sql = "UPDATE [OrderDetails] SET [OrderID] = ?, [ProductID] = ?, [Quantity] = ?, [UnitPrice] = ? WHERE [OrderDetailsID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderID);
            cmd.Parameters.AddWithValue("?", ProductID);
            cmd.Parameters.AddWithValue("?", Quantity);
            cmd.Parameters.AddWithValue("?", UnitPrice);
            cmd.Parameters.AddWithValue("?", OrderDetailsID);

            db.DoCommandInjection(cmd);
        }

        // showAllOrderDetails
        public List<OrderDetails> showAllOrderDetails()
        {
            string sql = "SELECT * FROM [OrderDetails]";
            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);
            List<OrderDetails> list = new List<OrderDetails>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new OrderDetails
                {
                    MyOrderDetailsID = Convert.ToInt32(row["OrderDetailsID"]),
                    MyOrderID = Convert.ToInt32(row["OrderID"]),
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyQuantity = Convert.ToInt32(row["Quantity"]),
                    MyUnitPrice = Convert.ToDecimal(row["UnitPrice"])
                });
            }

            return list;
        }

        // showOrderDetails
        public OrderDetails showOrderDetails(int OrderDetailsID)
        {
            string sql = "SELECT * FROM [OrderDetails] WHERE [OrderDetailsID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderDetailsID);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new OrderDetails
            {
                MyOrderDetailsID = Convert.ToInt32(row["OrderDetailsID"]),
                MyOrderID = Convert.ToInt32(row["OrderID"]),
                MyProductID = Convert.ToInt32(row["ProductID"]),
                MyQuantity = Convert.ToInt32(row["Quantity"]),
                MyUnitPrice = Convert.ToDecimal(row["UnitPrice"])
            };
        }
    }
}
