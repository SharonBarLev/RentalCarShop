using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Category
    public class Category
    {
        // CategoryID
        private int CategoryID;
        public int MyCategoryID { get { return this.CategoryID; } set { this.CategoryID = value; } }

        // CategoryName
        private string CategoryName;
        public string MyCategoryName { get { return this.CategoryName; } set { this.CategoryName = value; } }

        // Description
        private string Description;
        public string MyDescription { get { return this.Description; } set { this.Description = value; } }
    }
}

