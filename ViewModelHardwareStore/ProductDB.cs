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
    public class ProductDB
    {
        public ProductDB() { }
        public DBfunction db = new DBfunction();

        // isExistProduct
        public bool isExistProduct(int ProductID)
        {
            string sql = string.Format("select * from [Product] where [ProductID]={0}", ProductID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // GetNextProductID
        public int GetNextProductID(int CategoryID)
        {
            int baseNumber = CategoryID * 10000;
            int nextID = baseNumber + 1;

            string sql = string.Format(
                "SELECT MAX(ProductID) FROM Product WHERE CategoryID = {0} AND ProductID >= {1} AND ProductID < {2}",
                CategoryID, baseNumber, baseNumber + 10000);

            DataSet ds = db.Selectcommand(sql);
            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                int maxID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                if (maxID >= baseNumber)
                {
                    nextID = maxID + 1;
                }
            }

            return nextID;
        }

        // AddProduct
        public void AddProduct(int ProductID, string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description)
        {
            string sql = string.Format(
                "insert into [Product] ([ProductID], [ProductName], [CategoryID], [BrandID], [Price], [StockQuantity], [Description]) " +
                "values ({0}, '{1}', {2}, {3}, {4}, {5}, '{6}')",
                ProductID, ProductName, CategoryID, BrandID, Price, StockQuantity, Description);
            db.DoCommand(sql);
        }

        // DeleteProduct
        public void DeleteProduct(int ProductID)
        {
            string sql = string.Format("delete from [Product] where [ProductID]={0}", ProductID);
            db.DoCommand(sql);
        }

        // UpdateProduct
        public void UpdateProduct(string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, int ProductID)
        {
            string sql = string.Format(
                "update [Product] set [ProductName]='{0}', [CategoryID]={1}, [BrandID]={2}, [Price]={3}, [StockQuantity]={4}, [Description]='{5}' where [ProductID]={6}",
                ProductName, CategoryID, BrandID, Price, StockQuantity, Description, ProductID);
            db.DoCommand(sql);
        }

        // showAllProduct
        public DataSet showAllProduct()
        {
            string sql = "select * from [Product]";
            return db.Selectcommand(sql);
        }

        // showProduct
        public DataSet showProduct(int ProductID)
        {
            string sql = string.Format("select * from [Product] where [ProductID]={0}", ProductID);
            return db.Selectcommand(sql);
        }

        // showAllProductsInnerJoin
        public DataSet showAllProductsInnerJoin()
        {
            string sql = string.Format(
                "SELECT Product.ProductID, Product.ProductName, Category.CategoryName, Brand.BrandName, " +
                "Product.Price, Product.Description, Product.StockQuantity, Product.Picture " +
                "FROM Category " +
                "INNER JOIN Product ON Category.CategoryID = Product.CategoryID " +
                "INNER JOIN Brand ON Brand.BrandID = Product.BrandID");

            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // showAllProductByCategoryID
        public DataSet showAllProductByCategoryID(int CategoryID)
        {
            string sql = string.Format("select * from [Product] where [CategoryID]={0}", CategoryID);
            return db.Selectcommand(sql);
        }

        // showAllProductByBrandID
        public DataSet showAllProductByBrandID(int BrandID)
        {
            string sql = string.Format("select * from [Product] where [BrandID]={0}", BrandID);
            return db.Selectcommand(sql);
        }

        // showAllProductsPriceDesc
        public DataSet showAllProductsPriceDesc()
        {
            string sql = "select * from [Product] order by [Price] DESC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // showAllProductsPriceAsc
        public DataSet showAllProductsPriceAsc()
        {
            string sql = "select * from [Product] order by [Price] ASC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // SearchProducts
        public DataSet SearchProducts(string SearchTerm)
        {
            string sql = string.Format("SELECT * FROM [Product] WHERE [ProductName] LIKE '%{0}%' OR [Description] LIKE '%{0}%'", SearchTerm);
            return db.Selectcommand(sql);
        }

        // FilterProductsByPriceRange
        public DataSet FilterProductsByPriceRange(decimal MinPrice, decimal MaxPrice)
        {
            string sql = string.Format("SELECT * FROM [Product] WHERE [Price] BETWEEN {0} AND {1}", MinPrice, MaxPrice);
            return db.Selectcommand(sql);
        }

        // GetProductsUnder50
        public DataSet GetProductsUnder50()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] <= 50 ORDER BY [Price] ASC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // GetProductsBetween51And100
        public DataSet GetProductsBetween51And100()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 50 AND [Price] <= 100 ORDER BY [Price] ASC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // GetProductsBetween101And200
        public DataSet GetProductsBetween101And200()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 100 AND [Price] <= 200 ORDER BY [Price] ASC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // GetProductsBetween201And500
        public DataSet GetProductsBetween201And500()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 200 AND [Price] <= 500 ORDER BY [Price] ASC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }

        // GetProductsAbove500
        public DataSet GetProductsAbove500()
        {
            string sql = "SELECT * FROM [Product] WHERE [Price] > 500 ORDER BY [Price] ASC";
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }
    }
}

