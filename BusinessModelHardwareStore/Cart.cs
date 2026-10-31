using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Cart
    public class Cart
    {
        // ProductID
        private int ProductID;
        public int MyProductID { get { return this.ProductID; } set { this.ProductID = value; } }

        // Username
        private string Username;
        public string MyUsername { get { return this.Username; } set { this.Username = value; } }

        // Price
        private decimal Price;
        public decimal MyPrice { get { return this.Price; } set { this.Price = value; } }

        // Picture
        private string Picture;
        public string MyPicture { get { return this.Picture; } set { this.Picture = value; } }

        // Quantity
        private int Quantity;
        public int MyQuantity { get { return this.Quantity; } set { this.Quantity = value; } }
    }
}

