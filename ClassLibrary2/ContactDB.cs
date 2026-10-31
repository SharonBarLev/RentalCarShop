using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class ContactDB
    {
        public ContactDB() { }
        public DBfunction db = new DBfunction();

        // isExistContact
        public bool isExistContact(int ContactID)
        {
            string sql = "SELECT * FROM [Contact] WHERE [ContactID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ContactID);

            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // AddContact - עודכן: מוסיף כברירת מחדל false (לא טופל) בעת יצירת פנייה חדשה
        public void AddContact(string Username, string FullName, string Email, string Subject, string Message, DateTime DateSent)
        {
            string sql = "INSERT INTO [Contact] ([Username], [FullName], [Email], [Subject], [Message], [DateSent], [Treated]) " +
                         "VALUES (?, ?, ?, ?, ?, ?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", FullName);
            cmd.Parameters.AddWithValue("?", Email);
            cmd.Parameters.AddWithValue("?", Subject);
            cmd.Parameters.AddWithValue("?", Message);
            cmd.Parameters.AddWithValue("?", DateSent);
            cmd.Parameters.AddWithValue("?", false); // פנייה חדשה מוגדרת אוטומטית כלא טופלה

            db.DoCommandInjection(cmd);
        }

        // NEW: UpdateContactStatus - פונקציה חדשה שמאפשרת למנהל לשנות את סטטוס הטיפול (למשל לסמן כטופל)
        public void UpdateContactStatus(int ContactID, bool IsTreated)
        {
            string sql = "UPDATE [Contact] SET [Treated] = ? WHERE [ContactID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", IsTreated);
            cmd.Parameters.AddWithValue("?", ContactID);

            db.DoCommandInjection(cmd);
        }

        // DeleteContact
        public void DeleteContact(int ContactID)
        {
            string sql = "DELETE FROM [Contact] WHERE [ContactID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ContactID);

            db.DoCommandInjection(cmd);
        }

        // showAllContact - עודכן: קורא את השדה מהשורה בבסיס הנתונים
        public List<Contact> showAllContact()
        {
            string sql = "SELECT * FROM [Contact]";
            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<Contact> list = new List<Contact>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Contact
                {
                    MyContactID = Convert.ToInt32(row["ContactID"]),
                    MyUsername = row["Username"].ToString(),
                    MyFullName = row["FullName"].ToString(),
                    MyEmail = row["Email"].ToString(),
                    MySubject = row["Subject"].ToString(),
                    MyMessage = row["Message"].ToString(),
                    MyDateSent = Convert.ToDateTime(row["DateSent"]),
                    MyTreated = Convert.ToBoolean(row["Treated"]) // מיפוי השדה החדש
                });
            }
            return list;
        }

        // ShowContact - עודכן: קורא את השדה מהשורה בבסיס הנתונים
        public Contact ShowContact(int ContactID)
        {
            string sql = "SELECT * FROM [Contact] WHERE [ContactID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ContactID);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new Contact
            {
                MyContactID = Convert.ToInt32(row["ContactID"]),
                MyUsername = row["Username"].ToString(),
                MyFullName = row["FullName"].ToString(),
                MyEmail = row["Email"].ToString(),
                MySubject = row["Subject"].ToString(),
                MyMessage = row["Message"].ToString(),
                MyDateSent = Convert.ToDateTime(row["DateSent"]),
                MyTreated = Convert.ToBoolean(row["Treated"]) // מיפוי השדה החדש
            };
        }

        // GetContactsByUser - עודכן: קורא את השדה מהשורה בבסיס הנתונים
        public List<Contact> GetContactsByUser(string Username)
        {
            string sql = "SELECT * FROM [Contact] WHERE [Username] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<Contact> list = new List<Contact>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Contact
                {
                    MyContactID = Convert.ToInt32(row["ContactID"]),
                    MyUsername = row["Username"].ToString(),
                    MyFullName = row["FullName"].ToString(),
                    MyEmail = row["Email"].ToString(),
                    MySubject = row["Subject"].ToString(),
                    MyMessage = row["Message"].ToString(),
                    MyDateSent = Convert.ToDateTime(row["DateSent"]),
                    MyTreated = Convert.ToBoolean(row["Treated"]) // מיפוי השדה החדש
                });
            }
            return list;
        }
    }
}
