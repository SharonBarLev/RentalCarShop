using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModelCarShop
{
    // Contact
    public class Contact
    {
        // ContactID
        private int ContactID;
        public int MyContactID { get { return this.ContactID; } set { this.ContactID = value; } }

        // Username
        private string Username;
        public string MyUsername { get { return this.Username; } set { this.Username = value; } }

        // FullName
        private string FullName;
        public string MyFullName { get { return this.FullName; } set { this.FullName = value; } }

        // Email
        private string Email;
        public string MyEmail { get { return this.Email; } set { this.Email = value; } }

        // Subject
        private string Subject;
        public string MySubject { get { return this.Subject; } set { this.Subject = value; } }

        // Message
        private string Message;
        public string MyMessage { get { return this.Message; } set { this.Message = value; } }

        // DateSent
        private DateTime DateSent;
        public DateTime MyDateSent { get { return this.DateSent; } set { this.DateSent = value; } }
    }
}

