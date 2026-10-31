using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // CarUser
    public class CarUser
    {
        // Id
        private int Id;
        public int MyId { get { return this.Id; } set { this.Id = value; } }

        // Username
        private string Username;
        public string MyUsername { get { return this.Username; } set { this.Username = value; } }

        // Password
        private string Password;
        public string MyPassword { get { return this.Password; } set { this.Password = value; } }

        // FirstName
        private string FirstName;
        public string MyFirstName { get { return this.FirstName; } set { this.FirstName = value; } }

        // LastName
        private string LastName;
        public string MyLastName { get { return this.LastName; } set { this.LastName = value; } }

        // Email
        private string Email;
        public string MyEmail { get { return this.Email; } set { this.Email = value; } }

        // Phone
        private string Phone;
        public string MyPhone { get { return this.Phone; } set { this.Phone = value; } }

        // IsAdmin
        private bool IsAdmin;
        public bool MyIsAdmin { get { return this.IsAdmin; } set { this.IsAdmin = value; } }
    }
}
