using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Review
    public class Review
    {
        // ReviewID
        private int ReviewID;
        public int MyReviewID { get { return this.ReviewID; } set { this.ReviewID = value; } }

        // Username
        private string Username;
        public string MyUsername { get { return this.Username; } set { this.Username = value; } }

        // ProductID
        private int ProductID;
        public int MyProductID { get { return this.ProductID; } set { this.ProductID = value; } }

        // Rev
        private string Rev;
        public string MyRev { get { return this.Rev; } set { this.Rev = value; } }

        // Rating
        private int Rating;
        public int MyRating { get { return this.Rating; } set { this.Rating = value; } }
    }
}

