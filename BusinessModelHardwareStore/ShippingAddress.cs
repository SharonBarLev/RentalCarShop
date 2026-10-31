using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // ShippingAddress
    public class ShippingAddress
    {
        // Username
        private string Username;
        public string MyUsername { get { return this.Username; } set { this.Username = value; } }

        // CountryRegion
        private string CountryRegion;
        public string MyCountryRegion { get { return this.CountryRegion; } set { this.CountryRegion = value; } }

        // City
        private string City;
        public string MyCity { get { return this.City; } set { this.City = value; } }

        // StreetAddress
        private string StreetAddress;
        public string MyStreetAddress { get { return this.StreetAddress; } set { this.StreetAddress = value; } }

        // Floor
        private string Floor;
        public string MyFloor { get { return this.Floor; } set { this.Floor = value; } }

        // ZipCode
        private string ZipCode;
        public string MyZipCode { get { return this.ZipCode; } set { this.ZipCode = value; } }

        // AdditionalInstructions
        private string AdditionalInstructions;
        public string MyAdditionalInstructions { get { return this.AdditionalInstructions; } set { this.AdditionalInstructions = value; } }
    }
}

