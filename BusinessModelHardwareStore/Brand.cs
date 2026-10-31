using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Brand
    public class Brand
    {
        // BrandName
        private int BrandID;
        public int MyBrandID { get { return this.BrandID; } set { this.BrandID = value; } }

        // BrandName
        private string BrandName;
        public string MyBrandName { get { return this.BrandName; } set { this.BrandName = value; } }

        // CountryOfOrigin
        private string CountryOfOrigin;
        public string MyCountryOfOrigin { get { return this.CountryOfOrigin; } set { this.CountryOfOrigin = value; } }

        // WebsiteURL
        private string WebsiteURL;
        public string MyWebsiteURL { get { return this.WebsiteURL; } set { this.WebsiteURL = value; } }

        // Description
        private string Description;
        public string MyDescription { get { return this.Description; } set { this.Description = value; } }
    }
}

