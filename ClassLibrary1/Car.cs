using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Car
    public class Car
    {
        // Id
        private int Id;
        public int MyId { get { return this.Id; } set { this.Id = value; } }

        // Manufacturer
        private string Manufacturer;
        public string MyManufacturer { get { return this.Manufacturer; } set { this.Manufacturer = value; } }

        // Model
        private string Model;
        public string MyModel { get { return this.Model; } set { this.Model = value; } }

        // Year
        private int Year;
        public int MyYear { get { return this.Year; } set { this.Year = value; } }

        // Category
        private string Category;
        public string MyCategory { get { return this.Category; } set { this.Category = value; } }

        // Price
        private decimal Price;
        public decimal MyPrice { get { return this.Price; } set { this.Price = value; } }

        // Stock
        private int Stock;
        public int MyStock { get { return this.Stock; } set { this.Stock = value; } }

        // ImageUrl
        private string ImageUrl;
        public string MyImageUrl { get { return this.ImageUrl; } set { this.ImageUrl = value; } }

        // Description
        private string Description;
        public string MyDescription { get { return this.Description; } set { this.Description = value; } }

        // BranchId
        private int BranchId;
        public int MyBranchId { get { return this.BranchId; } set { this.BranchId = value; } }

        // Seats
        private int Seats;
        public int MySeats { get { return this.Seats; } set { this.Seats = value; } }

        // FuelType
        private string FuelType;
        public string MyFuelType { get { return this.FuelType; } set { this.FuelType = value; } }

        // RangeKm
        private int RangeKm;
        public int MyRangeKm { get { return this.RangeKm; } set { this.RangeKm = value; } }

        // Transmission
        private string Transmission;
        public string MyTransmission { get { return this.Transmission; } set { this.Transmission = value; } }

        // LuggageCapacity
        private int LuggageCapacity;
        public int MyLuggageCapacity { get { return this.LuggageCapacity; } set { this.LuggageCapacity = value; } }
    }
}
