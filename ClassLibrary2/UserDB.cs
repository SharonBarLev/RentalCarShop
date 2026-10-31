using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class UserDB
    {
        public UserDB() { }

        public DBfunction db = new DBfunction();

        // isExistUser
        public bool isExistUser(string Username)
        {
            string sql = "SELECT * FROM [User] WHERE [Username] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }


        // AddUser
        public void AddUser(string Username, string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate)
        {
            string sql = @"INSERT INTO [User] 
                          ([Username], [Password], [PhoneNumber], [Email], [Admin], [JoinDate]) 
                          VALUES (?, ?, ?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", Password);
            cmd.Parameters.AddWithValue("?", PhoneNumber);
            cmd.Parameters.AddWithValue("?", Email);
            cmd.Parameters.AddWithValue("?", Admin);
            cmd.Parameters.Add("?", OleDbType.Date).Value = JoinDate;

            db.DoCommandInjection(cmd);
        }


        // DeleteUser
        public void DeleteUser(string Username)
        {
            string sql = "DELETE FROM [User] WHERE [Username] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", Username);

            db.DoCommandInjection(cmd);
        }


        // UpdateUser
        public void UpdateUser(string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate, string Username)
        {
            string sql = @"UPDATE [User] 
                          SET [Password] = ?, 
                              [PhoneNumber] = ?, 
                              [Email] = ?, 
                              [Admin] = ?, 
                              [JoinDate] = ? 
                          WHERE [Username] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", Password);
            cmd.Parameters.AddWithValue("?", PhoneNumber);
            cmd.Parameters.AddWithValue("?", Email);
            cmd.Parameters.AddWithValue("?", Admin);
            cmd.Parameters.AddWithValue("?", JoinDate);
            cmd.Parameters.AddWithValue("?", Username);

            db.DoCommandInjection(cmd);
        }


        // showAllUsers
        public List<User> showAllUsers()
        {
            string sql = "SELECT * FROM [User] ";

            OleDbCommand cmd = new OleDbCommand(sql);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<User> users = new List<User>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                users.Add(new User
                {
                    MyUsername = row["Username"].ToString(),
                    MyPassword = row["Password"].ToString(),
                    MyPhoneNumber = row["PhoneNumber"].ToString(),
                    MyEmail = row["Email"].ToString(),
                    MyAdmin = Convert.ToBoolean(row["Admin"]),
                    MyJoinDate = Convert.ToDateTime(row["JoinDate"])
                });
            }

            return users;
        }


        // showUser
        public User showUser(string Username)
        {
            string sql = "SELECT * FROM [User] WHERE [Username] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", Username);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

                       
            return new User
            {
                MyUsername = row["Username"].ToString(),
                MyPassword = row["Password"].ToString(),
                MyPhoneNumber = row["PhoneNumber"].ToString(),
                MyEmail = row["Email"].ToString(),
                MyAdmin = Convert.ToBoolean(row["Admin"]),
                MyJoinDate = Convert.ToDateTime(row["JoinDate"])
            };
        }


        // IsExistUserAndPass
        public bool IsExistUserAndPass(string Username, string Password)
        {
            string sql = "SELECT * FROM [User] WHERE [Username] = ? AND [Password] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", Password);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }


        // IsUserAdmin
        public bool IsUserAdmin(string username)
        {
            string sql = "SELECT [Admin] FROM [User] WHERE [Username] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", username);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return false;

            return Convert.ToBoolean(ds.Tables[0].Rows[0]["Admin"]);
        }


        // ShowUserByEmail
        public User ShowUserByEmail(string Email)
        {
            string sql = "SELECT * FROM [User] WHERE [Email] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);

            cmd.Parameters.AddWithValue("?", Email);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new User
            {
                MyUsername = row["Username"].ToString(),
                MyPassword = row["Password"].ToString(),
                MyPhoneNumber = row["PhoneNumber"].ToString(),
                MyEmail = row["Email"].ToString(),
                MyAdmin = Convert.ToBoolean(row["Admin"]),
                MyJoinDate = Convert.ToDateTime(row["JoinDate"])
            };
        }
    }
}
