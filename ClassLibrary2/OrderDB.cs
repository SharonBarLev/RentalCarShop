using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class OrderDB
    {
        public OrderDB() { }
        public DBfunction db = new DBfunction();

        // isExistOrder
        public bool isExistOrder(int OrderID)
        {
            string sql = "SELECT * FROM [Order] WHERE [OrderID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderID);

            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // AddOrder
        public void AddOrder(string Username, DateTime OrderDate, decimal TotalPrice, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            string Address = $"{CountryRegion}, {City}, {StreetAddress}, {Floor}, {ZipCode}";

            string sql = "INSERT INTO [Order] ([Username], [OrderDate], [TotalPrice], [Address], [AdditionalInstructions], [Delivered]) " +
                         "VALUES (?, ?, ?, ?, ?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", OrderDate);
            cmd.Parameters.AddWithValue("?", TotalPrice);
            cmd.Parameters.AddWithValue("?", Address);
            cmd.Parameters.AddWithValue("?", AdditionalInstructions);
            cmd.Parameters.AddWithValue("?", false);

            db.DoCommandInjection(cmd);
        }

        // DeleteOrder
        public void DeleteOrder(int OrderID)
        {
            string sql = "DELETE FROM [Order] WHERE [OrderID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderID);

            db.DoCommandInjection(cmd);
        }

        // UpdateOrder
        public void UpdateOrder(string Username, DateTime OrderDate, decimal TotalPrice, string Address, string AdditionalInstructions, bool Delivered, int OrderID)
        {
            string sql = "UPDATE [Order] SET [Username]=?, [OrderDate]=?, [TotalPrice]=?, [Address]=?, [AdditionalInstructions]=?, [Delivered]=? WHERE [OrderID]=?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", OrderDate);
            cmd.Parameters.AddWithValue("?", TotalPrice);
            cmd.Parameters.AddWithValue("?", Address);
            cmd.Parameters.AddWithValue("?", AdditionalInstructions);
            cmd.Parameters.AddWithValue("?", Delivered);
            cmd.Parameters.AddWithValue("?", OrderID);

            db.DoCommandInjection(cmd);
        }

        // showAllOrders
        public List<Order> showAllOrders()
        {
            string sql = "SELECT * FROM [Order]";
            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);
            List<Order> list = new List<Order>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Order
                {
                    MyOrderID = Convert.ToInt32(row["OrderID"]),
                    MyUsername = row["Username"].ToString(),
                    MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                    MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                    MyAddress = row["Address"].ToString(),
                    MyAdditionalInstructions = row["AdditionalInstructions"].ToString(),
                    MyDelivered = Convert.ToBoolean(row["Delivered"])
                });
            }

            return list;
        }

        // showOrder
        public Order showOrder(int OrderID)
        {
            string sql = "SELECT * FROM [Order] WHERE [OrderID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", OrderID);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new Order
            {
                MyOrderID = Convert.ToInt32(row["OrderID"]),
                MyUsername = row["Username"].ToString(),
                MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                MyAddress = row["Address"].ToString(),
                MyAdditionalInstructions = row["AdditionalInstructions"].ToString(),
                MyDelivered = Convert.ToBoolean(row["Delivered"])
            };
        }

        // UpdateOrderStatus
        public void UpdateOrderStatus(int OrderID, bool Delivered)
        {
            string sql = "UPDATE [Order] SET [Delivered]=? WHERE [OrderID]=?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Delivered);
            cmd.Parameters.AddWithValue("?", OrderID);

            db.DoCommandInjection(cmd);
        }

        // showPendingOrders
        public List<Order> showPendingOrders()
        {
            string sql = "SELECT * FROM [Order] WHERE [Delivered] = false";
            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);
            List<Order> list = new List<Order>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Order
                {
                    MyOrderID = Convert.ToInt32(row["OrderID"]),
                    MyUsername = row["Username"].ToString(),
                    MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                    MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                    MyAddress = row["Address"].ToString(),
                    MyAdditionalInstructions = row["AdditionalInstructions"].ToString(),
                    MyDelivered = false
                });
            }
            return list;
        }

        // showOrdersByUsername
        public List<Order> showOrdersByUsername(string Username)
        {
            string sql = "SELECT * FROM [Order] WHERE [Username] = ? ORDER BY [OrderDate] DESC";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);

            DataSet ds = db.SelectcommandInjection(cmd);
            List<Order> list = new List<Order>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Order
                {
                    MyOrderID = Convert.ToInt32(row["OrderID"]),
                    MyUsername = row["Username"].ToString(),
                    MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                    MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                    MyAddress = row["Address"].ToString(),
                    MyAdditionalInstructions = row["AdditionalInstructions"].ToString(),
                    MyDelivered = Convert.ToBoolean(row["Delivered"])
                });
            }
            return list;
        }
    }
}
