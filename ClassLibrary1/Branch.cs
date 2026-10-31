using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Branch
    public class Branch
    {
        // Id
        private int Id;
        public int MyId { get { return this.Id; } set { this.Id = value; } }

        // Name
        private string Name;
        public string MyName { get { return this.Name; } set { this.Name = value; } }

        // City
        private string City;
        public string MyCity { get { return this.City; } set { this.City = value; } }

        // Address
        private string Address;
        public string MyAddress { get { return this.Address; } set { this.Address = value; } }

        // Phone
        private string Phone;
        public string MyPhone { get { return this.Phone; } set { this.Phone = value; } }
    }
}
