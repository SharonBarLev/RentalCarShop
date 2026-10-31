using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // User
    public class User
    {
        // Username
        private string Username;
        public string MyUsername { get { return this.Username;  } set { this.Username = value; } }

        // Password
        private string Password;
        public string MyPassword { get { return this.Password; } set { this.Password = value; } }

        // PhoneNumber
        private string PhoneNumber;
        public string MyPhoneNumber { get { return this.PhoneNumber; } set { this.PhoneNumber = value; } }

        // Email
        private string Email;
        public string MyEmail { get { return this.Email; } set { this.Email = value; } }

        // Admin
        private bool Admin;
        public bool MyAdmin { get { return this.Admin; } set { this.Admin = value; } }

        // JoinDate
        private DateTime JoinDate;
        public DateTime MyJoinDate { get { return this.JoinDate; } set { this.JoinDate = value; } }
    }
}

