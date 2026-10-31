using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // CarReview
    public class CarReview
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

        // OrderId
        private int OrderId;
        public int MyOrderId { get { return this.OrderId; } set { this.OrderId = value; } }

        // Rating
        private int Rating;
        public int MyRating { get { return this.Rating; } set { this.Rating = value; } }

        // Comment
        private string Comment;
        public string MyComment { get { return this.Comment; } set { this.Comment = value; } }

        // ReviewDate
        private DateTime ReviewDate;
        public DateTime MyReviewDate { get { return this.ReviewDate; } set { this.ReviewDate = value; } }
    }
}
