using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class BrandDB
    {
        public BrandDB() { }

        public DBfunction db = new DBfunction();


        // isExistBrand
        public bool isExistBrand(int BrandID)
        {
            string sql = "SELECT * FROM [Brand] WHERE [BrandID] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", BrandID);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }


        // AddBrand
        public void AddBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description)
        {
            string sql = @"INSERT INTO [Brand] 
                          ([BrandName], [CountryOfOrigin], [WebsiteURL], [Description]) 
                          VALUES (?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", BrandName);
            cmd.Parameters.AddWithValue("?", CountryOfOrigin);
            cmd.Parameters.AddWithValue("?", WebsiteURL);
            cmd.Parameters.AddWithValue("?", Description);

            db.DoCommandInjection(cmd);
        }


        // DeleteBrand
        public void DeleteBrand(int BrandID)
        {
            string sql = "DELETE FROM [Brand] WHERE [BrandID] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", BrandID);

            db.DoCommandInjection(cmd);
        }


        // UpdateBrand
        public void UpdateBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description, int BrandID)
        {
            string sql = @"UPDATE [Brand] 
                          SET [BrandName] = ?, 
                              [CountryOfOrigin] = ?, 
                              [WebsiteURL] = ?, 
                              [Description] = ? 
                          WHERE [BrandID] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", BrandName);
            cmd.Parameters.AddWithValue("?", CountryOfOrigin);
            cmd.Parameters.AddWithValue("?", WebsiteURL);
            cmd.Parameters.AddWithValue("?", Description);
            cmd.Parameters.AddWithValue("?", BrandID);

            db.DoCommandInjection(cmd);
        }


        // showAllBrand
        public List<Brand> showAllBrand()
        {
            string sql = "SELECT * FROM [Brand]";

            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<Brand> brands = new List<Brand>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                brands.Add(new Brand
                {
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyBrandName = row["BrandName"].ToString(),
                    MyCountryOfOrigin = row["CountryOfOrigin"].ToString(),
                    MyWebsiteURL = row["WebsiteURL"].ToString(),
                    MyDescription = row["Description"].ToString()
                });
            }

            return brands;
        }


        // showBrand
        public Brand showBrand(int BrandID)
        {
            string sql = "SELECT * FROM [Brand] WHERE [BrandID] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", BrandID);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new Brand
            {
                MyBrandID = Convert.ToInt32(row["BrandID"]),
                MyBrandName = row["BrandName"].ToString(),
                MyCountryOfOrigin = row["CountryOfOrigin"].ToString(),
                MyWebsiteURL = row["WebsiteURL"].ToString(),
                MyDescription = row["Description"].ToString()
            };
        }
    }
}
