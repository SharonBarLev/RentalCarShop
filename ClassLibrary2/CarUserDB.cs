using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class CarUserDB
    {
        public CarUserDB() { }

        public DBfunction db = new DBfunction();

        // isExistCarUser
        public bool isExistCarUser(int Id)
        {
            string sql = "SELECT * FROM [Users] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }

        // AddCarUser
        public void AddCarUser(string Username, string Password, string FirstName, string LastName, string Email, string Phone, bool IsAdmin)
        {
            string sql = @"INSERT INTO [Users] ([Username], [Password], [FirstName], [LastName], [Email], [Phone], [IsAdmin]) VALUES (?, ?, ?, ?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", Password);
            cmd.Parameters.AddWithValue("?", FirstName);
            cmd.Parameters.AddWithValue("?", LastName);
            cmd.Parameters.AddWithValue("?", Email);
            cmd.Parameters.AddWithValue("?", Phone);
            cmd.Parameters.AddWithValue("?", IsAdmin);

            db.DoCommandInjection(cmd);
        }

        // DeleteCarUser
        public void DeleteCarUser(int Id)
        {
            string sql = "DELETE FROM [Users] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // UpdateCarUser
        public void UpdateCarUser(string Username, string Password, string FirstName, string LastName, string Email, string Phone, bool IsAdmin, int Id)
        {
            string sql = @"UPDATE [Users] SET [Username] = ?, [Password] = ?, [FirstName] = ?, [LastName] = ?, [Email] = ?, [Phone] = ?, [IsAdmin] = ? WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", Password);
            cmd.Parameters.AddWithValue("?", FirstName);
            cmd.Parameters.AddWithValue("?", LastName);
            cmd.Parameters.AddWithValue("?", Email);
            cmd.Parameters.AddWithValue("?", Phone);
            cmd.Parameters.AddWithValue("?", IsAdmin);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // showAllCarUsers
        public List<CarUser> showAllCarUsers()
        {
            string sql = "SELECT * FROM [Users]";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<CarUser> list = new List<CarUser>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new CarUser
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUsername = row["Username"].ToString(),
                    MyPassword = row["Password"].ToString(),
                    MyFirstName = row["FirstName"].ToString(),
                    MyLastName = row["LastName"].ToString(),
                    MyEmail = row["Email"].ToString(),
                    MyPhone = row["Phone"].ToString(),
                    MyIsAdmin = row["IsAdmin"] != DBNull.Value && Convert.ToBoolean(row["IsAdmin"])
                });
            }

            return list;
        }

        // showCarUser
        public CarUser showCarUser(int Id)
        {
            string sql = "SELECT * FROM [Users] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new CarUser
            {
                MyId = Convert.ToInt32(row["Id"]),
                MyUsername = row["Username"].ToString(),
                MyPassword = row["Password"].ToString(),
                MyFirstName = row["FirstName"].ToString(),
                MyLastName = row["LastName"].ToString(),
                MyEmail = row["Email"].ToString(),
                MyPhone = row["Phone"].ToString(),
                MyIsAdmin = row["IsAdmin"] != DBNull.Value && Convert.ToBoolean(row["IsAdmin"])
            };
        }
    }
}
