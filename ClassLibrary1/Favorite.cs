using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Favorite
    public class Favorite
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

        // AddedDate
        private DateTime AddedDate;
        public DateTime MyAddedDate { get { return this.AddedDate; } set { this.AddedDate = value; } }
    }
}
