using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class ProductDB
    {
        public ProductDB() { }
        public DBfunction db = new DBfunction();

        // isExistProduct
        public bool isExistProduct(int ProductID)
        {
            string sql = "SELECT * FROM [Product] WHERE [ProductID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ProductID);

            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // GetNextProductID
        public int GetNextProductID(int CategoryID)
        {
            int baseNumber = CategoryID * 10000;
            int nextID = baseNumber + 1;

            string sql = "SELECT MAX(ProductID) FROM [Product] WHERE [CategoryID] = ? AND ProductID >= ? AND ProductID < ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryID);
            cmd.Parameters.AddWithValue("?", baseNumber);
            cmd.Parameters.AddWithValue("?", baseNumber + 10000);

            DataSet ds = db.SelectcommandInjection(cmd);
            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                int maxID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (maxID >= baseNumber)
                    nextID = maxID + 1;
            }

            return nextID;
        }

        // AddProduct
        public void AddProduct(int ProductID, string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, string Picture)
        {
            string sql = "INSERT INTO [Product] ([ProductID], [ProductName], [CategoryID], [BrandID], [Price], [StockQuantity], [Description], [Picture]) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ProductID);
            cmd.Parameters.AddWithValue("?", ProductName);
            cmd.Parameters.AddWithValue("?", CategoryID);
            cmd.Parameters.AddWithValue("?", BrandID);
            cmd.Parameters.AddWithValue("?", Price);
            cmd.Parameters.AddWithValue("?", StockQuantity);
            cmd.Parameters.AddWithValue("?", Description);
            cmd.Parameters.AddWithValue("?", Picture);

            db.DoCommandInjection(cmd);
        }

        // DeleteProduct
        public void DeleteProduct(int ProductID)
        {
            string sql = "DELETE FROM [Product] WHERE [ProductID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ProductID);

            db.DoCommandInjection(cmd);
        }

        // UpdateProduct
        public void UpdateProduct(string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, string Picture, int ProductID)
        {
            string sql = "UPDATE [Product] SET [ProductName] = ?, [CategoryID] = ?, [BrandID] = ?, [Price] = ?, [StockQuantity] = ?, [Description] = ?, [Picture] = ? WHERE [ProductID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ProductName);
            cmd.Parameters.AddWithValue("?", CategoryID);
            cmd.Parameters.AddWithValue("?", BrandID);
            cmd.Parameters.AddWithValue("?", Price);
            cmd.Parameters.AddWithValue("?", StockQuantity);
            cmd.Parameters.AddWithValue("?", Description);
            cmd.Parameters.AddWithValue("?", Picture);
            cmd.Parameters.AddWithValue("?", ProductID);

            db.DoCommandInjection(cmd);
        }

        // showAllProduct
        public List<Product> showAllProduct()
        {
            string sql = "SELECT * FROM [Product]";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }

            return list;
        }

        // showProduct
        public Product showProduct(int ProductID)
        {
            string sql = "SELECT * FROM [Product] WHERE [ProductID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ProductID);
            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0) return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new Product
            {
                MyProductID = Convert.ToInt32(row["ProductID"]),
                MyProductName = row["ProductName"].ToString(),
                MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                MyBrandID = Convert.ToInt32(row["BrandID"]),
                MyPrice = Convert.ToDecimal(row["Price"]),
                MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                MyDescription = row["Description"].ToString(),
                MyPicture = row["Picture"].ToString()
            };
        }

        // showAllProductsInnerJoin
        public List<Product> showAllProductsInnerJoin()
        {
            string sql =
                "SELECT Product.ProductID, Product.ProductName, Category.CategoryName, Brand.BrandName, " +
                "Product.Price, Product.Description, Product.StockQuantity, Product.Picture " +
                "FROM (Product INNER JOIN Category ON Product.CategoryID = Category.CategoryID) " +
                "INNER JOIN Brand ON Product.BrandID = Brand.BrandID";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }

            return list;
        }

        // showAllProductByCategoryID
        public List<Product> showAllProductByCategoryID(int CategoryID)
        {
            string sql = "SELECT * FROM [Product] WHERE [CategoryID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CategoryID);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // showAllProductByBrandID
        public List<Product> showAllProductByBrandID(int BrandID)
        {
            string sql = "SELECT * FROM [Product] WHERE [BrandID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", BrandID);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // showAllProductsPriceDesc
        public List<Product> showAllProductsPriceDesc()
        {
            string sql = "SELECT * FROM [Product] ORDER BY [Price] DESC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // showAllProductsPriceAsc
        public List<Product> showAllProductsPriceAsc()
        {
            string sql = "SELECT * FROM [Product] ORDER BY [Price] ASC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // SearchProducts
        public List<Product> SearchProducts(string SearchTerm)
        {
            string sql = "SELECT * FROM [Product] WHERE [ProductName] LIKE ? OR [Description] LIKE ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", "%" + SearchTerm + "%");
            cmd.Parameters.AddWithValue("?", "%" + SearchTerm + "%");
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // FilterProductsByPriceRange
        public List<Product> FilterProductsByPriceRange(decimal MinPrice, decimal MaxPrice)
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] BETWEEN ? AND ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", MinPrice);
            cmd.Parameters.AddWithValue("?", MaxPrice);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // GetProductsUnder50
        public List<Product> GetProductsUnder50()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] <= 50 ORDER BY [Price] ASC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // GetProductsBetween51And100
        public List<Product> GetProductsBetween51And100()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 50 AND [Price] <= 100 ORDER BY [Price] ASC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // GetProductsBetween101And200
        public List<Product> GetProductsBetween101And200()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 100 AND [Price] <= 200 ORDER BY [Price] ASC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // GetProductsBetween201And500
        public List<Product> GetProductsBetween201And500()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 200 AND [Price] <= 500 ORDER BY [Price] ASC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }

        // GetProductsAbove500
        public List<Product> GetProductsAbove500()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 500 ORDER BY [Price] ASC";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Product> list = new List<Product>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Product
                {
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyProductName = row["ProductName"].ToString(),
                    MyCategoryID = Convert.ToInt32(row["CategoryID"]),
                    MyBrandID = Convert.ToInt32(row["BrandID"]),
                    MyPrice = Convert.ToDecimal(row["Price"]),
                    MyStockQuantity = Convert.ToInt32(row["StockQuantity"]),
                    MyDescription = row["Description"].ToString(),
                    MyPicture = row["Picture"].ToString()
                });
            }
            return list;
        }
    }
}
