using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class CarDB
    {
        public CarDB() { }

        public DBfunction db = new DBfunction();

        // isExistCar
        public bool isExistCar(int Id)
        {
            string sql = "SELECT * FROM [Cars] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }

        // AddCar
        public void AddCar(string Manufacturer, string Model, int Year, string Category, decimal Price, int Stock, string ImageUrl, string Description, int BranchId, int Seats, string FuelType, int RangeKm, string Transmission, int LuggageCapacity)
        {
            string sql = @"INSERT INTO [Cars] ([Manufacturer], [Model], [Year], [Category], [Price], [Stock], [ImageUrl], [Description], [BranchId], [Seats], [FuelType], [RangeKm], [Transmission], [LuggageCapacity]) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Manufacturer);
            cmd.Parameters.AddWithValue("?", Model);
            cmd.Parameters.AddWithValue("?", Year);
            cmd.Parameters.AddWithValue("?", Category);
            cmd.Parameters.AddWithValue("?", Price);
            cmd.Parameters.AddWithValue("?", Stock);
            cmd.Parameters.AddWithValue("?", ImageUrl);
            cmd.Parameters.AddWithValue("?", Description);
            cmd.Parameters.AddWithValue("?", BranchId);
            cmd.Parameters.AddWithValue("?", Seats);
            cmd.Parameters.AddWithValue("?", FuelType);
            cmd.Parameters.AddWithValue("?", RangeKm);
            cmd.Parameters.AddWithValue("?", Transmission);
            cmd.Parameters.AddWithValue("?", LuggageCapacity);

            db.DoCommandInjection(cmd);
        }

        // DeleteCar
        public void DeleteCar(int Id)
        {
            string sql = "DELETE FROM [Cars] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // UpdateCar
        public void UpdateCar(string Manufacturer, string Model, int Year, string Category, decimal Price, int Stock, string ImageUrl, string Description, int BranchId, int Seats, string FuelType, int RangeKm, string Transmission, int LuggageCapacity, int Id)
        {
            string sql = @"UPDATE [Cars] SET [Manufacturer] = ?, [Model] = ?, [Year] = ?, [Category] = ?, [Price] = ?, [Stock] = ?, [ImageUrl] = ?, [Description] = ?, [BranchId] = ?, [Seats] = ?, [FuelType] = ?, [RangeKm] = ?, [Transmission] = ?, [LuggageCapacity] = ? WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Manufacturer);
            cmd.Parameters.AddWithValue("?", Model);
            cmd.Parameters.AddWithValue("?", Year);
            cmd.Parameters.AddWithValue("?", Category);
            cmd.Parameters.AddWithValue("?", Price);
            cmd.Parameters.AddWithValue("?", Stock);
            cmd.Parameters.AddWithValue("?", ImageUrl);
            cmd.Parameters.AddWithValue("?", Description);
            cmd.Parameters.AddWithValue("?", BranchId);
            cmd.Parameters.AddWithValue("?", Seats);
            cmd.Parameters.AddWithValue("?", FuelType);
            cmd.Parameters.AddWithValue("?", RangeKm);
            cmd.Parameters.AddWithValue("?", Transmission);
            cmd.Parameters.AddWithValue("?", LuggageCapacity);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // showAllCars
        public List<Car> showAllCars()
        {
            string sql = "SELECT * FROM [Cars]";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Car> list = new List<Car>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Car
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyManufacturer = row["Manufacturer"].ToString(),
                    MyModel = row["Model"].ToString(),
                    MyYear = row["Year"] == DBNull.Value ? 0 : Convert.ToInt32(row["Year"]),
                    MyCategory = row["Category"].ToString(),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStock = Convert.ToInt32(row["Stock"]),
                    MyImageUrl = row["ImageUrl"].ToString(),
                    MyDescription = row["Description"].ToString(),
                    MyBranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]),
                    MySeats = row["Seats"] == DBNull.Value ? 0 : Convert.ToInt32(row["Seats"]),
                    MyFuelType = row["FuelType"].ToString(),
                    MyRangeKm = row["RangeKm"] == DBNull.Value ? 0 : Convert.ToInt32(row["RangeKm"]),
                    MyTransmission = row["Transmission"].ToString(),
                    MyLuggageCapacity = row["LuggageCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(row["LuggageCapacity"])
                });
            }

            return list;
        }

        // showCar
        public Car showCar(int Id)
        {
            string sql = "SELECT * FROM [Cars] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new Car
            {
                MyId = Convert.ToInt32(row["Id"]),
                MyManufacturer = row["Manufacturer"].ToString(),
                MyModel = row["Model"].ToString(),
                MyYear = row["Year"] == DBNull.Value ? 0 : Convert.ToInt32(row["Year"]),
                MyCategory = row["Category"].ToString(),
                MyPrice = Convert.ToDecimal(row["Price"]),
                MyStock = Convert.ToInt32(row["Stock"]),
                MyImageUrl = row["ImageUrl"].ToString(),
                MyDescription = row["Description"].ToString(),
                MyBranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]),
                MySeats = row["Seats"] == DBNull.Value ? 0 : Convert.ToInt32(row["Seats"]),
                MyFuelType = row["FuelType"].ToString(),
                MyRangeKm = row["RangeKm"] == DBNull.Value ? 0 : Convert.ToInt32(row["RangeKm"]),
                MyTransmission = row["Transmission"].ToString(),
                MyLuggageCapacity = row["LuggageCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(row["LuggageCapacity"])
            };
        }

        // showCarsByBranchId
        public List<Car> showCarsByBranchId(int BranchId)
        {
            string sql = "SELECT * FROM [Cars] WHERE [BranchId] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", BranchId);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<Car> list = new List<Car>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Car
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyManufacturer = row["Manufacturer"].ToString(),
                    MyModel = row["Model"].ToString(),
                    MyYear = row["Year"] == DBNull.Value ? 0 : Convert.ToInt32(row["Year"]),
                    MyCategory = row["Category"].ToString(),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStock = Convert.ToInt32(row["Stock"]),
                    MyImageUrl = row["ImageUrl"].ToString(),
                    MyDescription = row["Description"].ToString(),
                    MyBranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]),
                    MySeats = row["Seats"] == DBNull.Value ? 0 : Convert.ToInt32(row["Seats"]),
                    MyFuelType = row["FuelType"].ToString(),
                    MyRangeKm = row["RangeKm"] == DBNull.Value ? 0 : Convert.ToInt32(row["RangeKm"]),
                    MyTransmission = row["Transmission"].ToString(),
                    MyLuggageCapacity = row["LuggageCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(row["LuggageCapacity"])
                });
            }

            return list;
        }
    }
}
