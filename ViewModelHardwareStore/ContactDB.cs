using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace ViewModelCarShop
{
    public class ContactDB
    {
        public ContactDB() { }
        public DBfunction db = new DBfunction();

        // isExistContact
        public bool isExistContact(int ContactID)
        {
            string sql = string.Format("select * from [Contact] where [ContactID]={0}", ContactID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddContact
        public void AddContact(string Username, string FullName, string Email, string Subject, string Message, DateTime DateSent)
        {
            string sql = string.Format(
                "insert into [Contact] ([Username], [FullName], [Email], [Subject], [Message], [DateSent]) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                Username, FullName, Email, Subject, Message, DateSent);
            db.DoCommand(sql);
        }

        // DeleteContact
        public void DeleteContact(int ContactID)
        {
            string sql = string.Format("delete from [Contact] where [ContactID]={0}", ContactID);
            db.DoCommand(sql);
        }

        // showAllContact
        public DataSet showAllContact()
        {
            string sql = "select * from [Contact]";
            return db.Selectcommand(sql);
        }

        // ShowContact 
        public DataSet ShowContact(int ContactID)
        {
            string sql = string.Format("select * from [Contact] where [ContactID]={0}", ContactID);
            return db.Selectcommand(sql);
        }

        // GetContactsByUser
        public DataSet GetContactsByUser(string Username)
        {
            string sql = string.Format("select * from [Contact] where [Username]='{0}'", Username);
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }
    }
}

