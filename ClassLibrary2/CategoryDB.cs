using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class CategoryDB
    {
        public CategoryDB() { }
        public DBfunction db = new DBfunction();

        // isExistCategory
        public bool isExistCategory(int CategoryID)
        {
            string sql = "SELECT * FROM [Category] WHERE [CategoryID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryID);

            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // AddCategory
        public void AddCategory(string CategoryName, string Description)
        {
            string sql = "INSERT INTO [Category] ([CategoryName], [Description]) VALUES (?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryName);
            cmd.Parameters.AddWithValue("?", Description);

            db.DoCommandInjection(cmd);
        }

        // DeleteCategory
        public void DeleteCategory(int CategoryID)
        {
            string sql = "DELETE FROM [Category] WHERE [CategoryID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryID);

            db.DoCommandInjection(cmd);
        }

        // UpdateCategory
        public void UpdateCategory(string CategoryName, string Description, int CategoryID)
        {
            string sql = "UPDATE [Category] SET [CategoryName] = ?, [Description] = ? WHERE [CategoryID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryName);
            cmd.Parameters.AddWithValue("?", Description);
            cmd.Parameters.AddWithValue("?", CategoryID);

            db.DoCommandInjection(cmd);
        }

        // showAllCategory
        public List<Category> showAllCategory()
        {
            string sql = "SELECT * FROM [Category]";
            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<Category> list = new List<Category>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Category
                {
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyCategoryName = row["CategoryName"].ToString(),
                    MyDescription = row["Description"].ToString()
                });
            }
            return list;
        }

        // showCategory
        public Category showCategory(int CategoryID)
        {
            string sql = "SELECT * FROM [Category] WHERE [CategoryID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryID);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new Category
            {
                MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                MyCategoryName = row["CategoryName"].ToString(),
                MyDescription = row["Description"].ToString()
            };
        }

        // AddProductFromCategory
        public void AddProductFromCategory(int CategoryID)
        {
            string sql = "INSERT INTO [Product] " +
                         "([ProductID], [ProductName], [CategoryID], [BrandID], [Price], [StockQuantity], [Description], [Picture]) " +
                         "VALUES (?, 'Fake', ?, 2, 0, 0, 'None', 'None')";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", -CategoryID);
            cmd.Parameters.AddWithValue("?", CategoryID);

            db.DoCommandInjection(cmd);
        }
    }
}
