using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Product
    public class Product
    {
        // ProductID
        private int ProductID;
        public int MyProductID { get { return this.ProductID; } set { this.ProductID = value; } }

        // ProductName
        private string ProductName;
        public string MyProductName { get { return this.ProductName; } set { this.ProductName = value; } }

        // CategoryID
        private int CategoryID;
        public int MyCategoryID { get { return this.CategoryID; } set { this.CategoryID = value; } }

        // BrandName
        private int BrandID;
        public int MyBrandID { get { return this.BrandID; } set { this.BrandID = value; } }

        // Price
        private decimal Price;
        public decimal MyPrice { get { return this.Price; } set { this.Price = value; } }

        // StockQuantity
        private int StockQuantity;
        public int MyStockQuantity { get { return this.StockQuantity; } set { this.StockQuantity = value; } }

        // Description
        private string Description;
        public string MyDescription { get { return this.Description; } set { this.Description = value; } }
    }
}

