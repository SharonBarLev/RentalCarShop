using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class CarOrderDB
    {
        public CarOrderDB() { }

        public DBfunction db = new DBfunction();

        // isExistCarOrder
        public bool isExistCarOrder(int Id)
        {
            string sql = "SELECT * FROM [Orders] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }

        // AddCarOrder
        public void AddCarOrder(int UserId, int CarId, DateTime OrderDate, DateTime StartDate, DateTime EndDate, decimal TotalPrice, string Status)
        {
            string sql = @"INSERT INTO [Orders] ([UserId], [CarId], [OrderDate], [StartDate], [EndDate], [TotalPrice], [Status]) VALUES (?, ?, ?, ?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);
            cmd.Parameters.AddWithValue("?", CarId);
            cmd.Parameters.AddWithValue("?", OrderDate);
            cmd.Parameters.AddWithValue("?", StartDate);
            cmd.Parameters.AddWithValue("?", EndDate);
            cmd.Parameters.AddWithValue("?", TotalPrice);
            cmd.Parameters.AddWithValue("?", Status);

            db.DoCommandInjection(cmd);
        }

        // DeleteCarOrder
        public void DeleteCarOrder(int Id)
        {
            string sql = "DELETE FROM [Orders] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // UpdateCarOrder
        public void UpdateCarOrder(int UserId, int CarId, DateTime OrderDate, DateTime StartDate, DateTime EndDate, decimal TotalPrice, string Status, int Id)
        {
            string sql = @"UPDATE [Orders] SET [UserId] = ?, [CarId] = ?, [OrderDate] = ?, [StartDate] = ?, [EndDate] = ?, [TotalPrice] = ?, [Status] = ? WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);
            cmd.Parameters.AddWithValue("?", CarId);
            cmd.Parameters.AddWithValue("?", OrderDate);
            cmd.Parameters.AddWithValue("?", StartDate);
            cmd.Parameters.AddWithValue("?", EndDate);
            cmd.Parameters.AddWithValue("?", TotalPrice);
            cmd.Parameters.AddWithValue("?", Status);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // showAllCarOrders
        public List<CarOrder> showAllCarOrders()
        {
            string sql = "SELECT * FROM [Orders]";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<CarOrder> list = new List<CarOrder>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new CarOrder
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUserId = row["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserId"]),
                    MyCarId = row["CarId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CarId"]),
                    MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                    MyStartDate = Convert.ToDateTime(row["StartDate"]),
                    MyEndDate = Convert.ToDateTime(row["EndDate"]),
                    MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                    MyStatus = row["Status"].ToString()
                });
            }

            return list;
        }

        // showCarOrder
        public CarOrder showCarOrder(int Id)
        {
            string sql = "SELECT * FROM [Orders] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new CarOrder
            {
                MyId = Convert.ToInt32(row["Id"]),
                MyUserId = row["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserId"]),
                MyCarId = row["CarId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CarId"]),
                MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                MyStartDate = Convert.ToDateTime(row["StartDate"]),
                MyEndDate = Convert.ToDateTime(row["EndDate"]),
                MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                MyStatus = row["Status"].ToString()
            };
        }

        // showCarOrdersByUserId
        public List<CarOrder> showCarOrdersByUserId(int UserId)
        {
            string sql = "SELECT * FROM [Orders] WHERE [UserId] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<CarOrder> list = new List<CarOrder>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new CarOrder
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUserId = row["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UserId"]),
                    MyCarId = row["CarId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CarId"]),
                    MyOrderDate = Convert.ToDateTime(row["OrderDate"]),
                    MyStartDate = Convert.ToDateTime(row["StartDate"]),
                    MyEndDate = Convert.ToDateTime(row["EndDate"]),
                    MyTotalPrice = Convert.ToDecimal(row["TotalPrice"]),
                    MyStatus = row["Status"].ToString()
                });
            }

            return list;
        }
    }
}
