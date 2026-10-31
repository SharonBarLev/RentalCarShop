using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // CarOrder
    public class CarOrder
    {
        // Id
        private int Id;
        public int MyId { get { return this.Id; } set { this.Id = value; } }

        // UserId
        private int UserId;
        public int MyUserId { get { return this.UserId; } set { this.UserId = value; } }

        // CarId
        private int CarId;
        public int MyCarId { get { return this.CarId; } set { this.CarId = value; } }

        // OrderDate
        private DateTime OrderDate;
        public DateTime MyOrderDate { get { return this.OrderDate; } set { this.OrderDate = value; } }

        // StartDate
        private DateTime StartDate;
        public DateTime MyStartDate { get { return this.StartDate; } set { this.StartDate = value; } }

        // EndDate
        private DateTime EndDate;
        public DateTime MyEndDate { get { return this.EndDate; } set { this.EndDate = value; } }

        // TotalPrice
        private decimal TotalPrice;
        public decimal MyTotalPrice { get { return this.TotalPrice; } set { this.TotalPrice = value; } }

        // Status
        private string Status;
        public string MyStatus { get { return this.Status; } set { this.Status = value; } }
    }
}
