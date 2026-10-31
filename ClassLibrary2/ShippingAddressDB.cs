using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class ShippingAddressDB
    {
        public ShippingAddressDB() { }
        public DBfunction db = new DBfunction();

        // IsExistShippingAddress
        public bool IsExistShippingAddress(string Username)
        {
            string sql = "SELECT * FROM [ShippingAddress] WHERE [Username] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // AddShippingAddress
        public void AddShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            string sql = "INSERT INTO [ShippingAddress] ([Username], [CountryRegion], [City], [StreetAddress], [Floor], [ZipCode], [AdditionalInstructions]) " +
                         "VALUES (?, ?, ?, ?, ?, ?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", CountryRegion);
            cmd.Parameters.AddWithValue("?", City);
            cmd.Parameters.AddWithValue("?", StreetAddress);
            cmd.Parameters.AddWithValue("?", Floor);
            cmd.Parameters.AddWithValue("?", ZipCode);
            cmd.Parameters.AddWithValue("?", AdditionalInstructions);
            db.DoCommandInjection(cmd);
        }

        // UpdateShippingAddress
        public void UpdateShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            string sql = "UPDATE [ShippingAddress] SET [CountryRegion] = ?, [City] = ?, [StreetAddress] = ?, [Floor] = ?, [ZipCode] = ?, [AdditionalInstructions] = ? " +
                         "WHERE [Username] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CountryRegion);
            cmd.Parameters.AddWithValue("?", City);
            cmd.Parameters.AddWithValue("?", StreetAddress);
            cmd.Parameters.AddWithValue("?", Floor);
            cmd.Parameters.AddWithValue("?", ZipCode);
            cmd.Parameters.AddWithValue("?", AdditionalInstructions);
            cmd.Parameters.AddWithValue("?", Username);
            db.DoCommandInjection(cmd);
        }

        // DeleteShippingAddress
        public void DeleteShippingAddress(string Username)
        {
            string sql = "DELETE FROM [ShippingAddress] WHERE [Username] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            db.DoCommandInjection(cmd);
        }

        // ShowAllShippingAddress
        public List<ShippingAddress> ShowAllShippingAddress()
        {
            string sql = "SELECT * FROM [ShippingAddress]";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<ShippingAddress> list = new List<ShippingAddress>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new ShippingAddress
                {
                    MyUsername = row["Username"].ToString(),
                    MyCountryRegion = row["CountryRegion"].ToString(),
                    MyCity = row["City"].ToString(),
                    MyStreetAddress = row["StreetAddress"].ToString(),
                    MyFloor = row["Floor"].ToString(),
                    MyZipCode = row["ZipCode"].ToString(),
                    MyAdditionalInstructions = row["AdditionalInstructions"].ToString()
                });
            }
            return list;
        }

        // ShowShippingAddress
        public ShippingAddress ShowShippingAddress(string Username)
        {
            string sql = "SELECT * FROM [ShippingAddress] WHERE [Username] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0) return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new ShippingAddress
            {
                MyUsername = row["Username"].ToString(),
                MyCountryRegion = row["CountryRegion"].ToString(),
                MyCity = row["City"].ToString(),
                MyStreetAddress = row["StreetAddress"].ToString(),
                MyFloor = row["Floor"].ToString(),
                MyZipCode = row["ZipCode"].ToString(),
                MyAdditionalInstructions = row["AdditionalInstructions"].ToString()
            };
        }
    }
}
