using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // OrderDetails
    public class OrderDetails
    {
        // OrderDetailID
        private int OrderDetailsID;
        public int MyOrderDetailsID { get { return this.OrderDetailsID; } set { this.OrderDetailsID = value; } }

        // OrderID
        private int OrderID;
        public int MyOrderID { get { return this.OrderID; } set { this.OrderID = value; } }

        // ProductID
        private int ProductID;
        public int MyProductID { get { return this.ProductID; } set { this.ProductID = value; } }

        // Quantity
        private int Quantity;
        public int MyQuantity { get { return this.Quantity; } set { this.Quantity = value; } }

        // UnitPrice
        private decimal UnitPrice;
        public decimal MyUnitPrice { get { return this.UnitPrice; } set { this.UnitPrice = value; } }
    }
}

