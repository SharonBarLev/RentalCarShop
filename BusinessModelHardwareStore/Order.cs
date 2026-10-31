using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Order
    public class Order
    {
        // OrderID
        private int OrderID;
        public int MyOrderID { get { return this.OrderID; } set { this.OrderID = value; } }

        // CustomerID
        private string Usermame;
        public string MyUsermame { get { return this.Usermame; } set { this.Usermame = value; } }

        // OrderDate
        private DateTime OrderDate;
        public DateTime MyOrderDate { get { return this.OrderDate; } set { this.OrderDate = value; } }

        // TotalPrice
        private decimal TotalPrice;
        public decimal MyTotalPrice { get { return this.TotalPrice; } set { this.TotalPrice = value; } }

        // Address
        private string Address;
        public string MyAddress { get { return this.Address; } set { this.Address = value; } }

        // AdditionalInstructions
        private string AdditionalInstructions;
        public string MyAdditionalInstructions { get { return this.AdditionalInstructions; } set { this.AdditionalInstructions = value; } }
    }
}

