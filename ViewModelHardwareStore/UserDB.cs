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
    public class UserDB
    {
        public UserDB() { }
        public DBfunction db = new DBfunction();

        // isExistUser
        public bool isExistUser(string Username)
        {
            string sql = string.Format("select * from [User] where [Username]='{0}'", Username);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddUser
        public void AddUser(string Username, string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate)
        {
            string sql = string.Format(
                "insert into [User] ([Username], [Password], [PhoneNumber], [Email], [Admin], [JoinDate]) " +
                "values ('{0}', '{1}', '{2}', '{3}', {4}, #{5}#)",
                Username, Password, PhoneNumber, Email, Admin, JoinDate.ToString("yyyy-MM-dd"));
            db.DoCommand(sql);
        }

        // DeleteUser
        public void DeleteUser(string Username)
        {
            string sql = string.Format("delete from [User] where [Username]='{0}'", Username);
            db.DoCommand(sql);
        }

        // UpdateUser
        public void UpdateUser(string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate, string Username)
        {
            string sql = string.Format(
                "update [User] set [Password]='{0}', [PhoneNumber]='{1}', [Email]='{2}', [Admin]={3}, [JoinDate]=#{4}# where [Username]='{5}'",
                Password, PhoneNumber, Email, Admin ? 1 : 0, JoinDate.ToString("yyyy-MM-dd"), Username);
            db.DoCommand(sql);
        }

        // showAllUsers
        public DataSet showAllUsers()
        {
            string sql = "select * from [User]";
            return db.Selectcommand(sql);
        }

        // showUser
        public DataSet showUser(string Username)
        {
            string sql = string.Format("select * from [User] where [Username]='{0}'", Username);
            return db.Selectcommand(sql);
        }

        // IsExistUserAndPass
        public bool IsExistUserAndPass(string Username, string Password)
        {
            string sql = string.Format("select * from [user] where [username]='{0}' and [password]='{1}'", Username, Password);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // IsAdmin
        public bool IsUserAdmin(string username)
        {
            string sql = string.Format("select [admin] from [user] where [username]='{0}'", username);
            DataSet ds = db.Selectcommand(sql);

            if (ds.Tables[0].Rows.Count == 1)
            {
                return Convert.ToBoolean(ds.Tables[0].Rows[0]["admin"]);
            }

            return false;
        }
    }
}

