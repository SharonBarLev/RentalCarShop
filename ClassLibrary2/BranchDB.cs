using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class BranchDB
    {
        public BranchDB() { }

        public DBfunction db = new DBfunction();

        // isExistBranch
        public bool isExistBranch(int Id)
        {
            string sql = "SELECT * FROM [Branches] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }

        // AddBranch
        public void AddBranch(string Name, string City, string Address, string Phone)
        {
            string sql = @"INSERT INTO [Branches] ([Name], [City], [Address], [Phone]) VALUES (?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Name);
            cmd.Parameters.AddWithValue("?", City);
            cmd.Parameters.AddWithValue("?", Address);
            cmd.Parameters.AddWithValue("?", Phone);

            db.DoCommandInjection(cmd);
        }

        // DeleteBranch
        public void DeleteBranch(int Id)
        {
            string sql = "DELETE FROM [Branches] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // UpdateBranch
        public void UpdateBranch(string Name, string City, string Address, string Phone, int Id)
        {
            string sql = @"UPDATE [Branches] SET [Name] = ?, [City] = ?, [Address] = ?, [Phone] = ? WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Name);
            cmd.Parameters.AddWithValue("?", City);
            cmd.Parameters.AddWithValue("?", Address);
            cmd.Parameters.AddWithValue("?", Phone);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // showAllBranches
        public List<Branch> showAllBranches()
        {
            string sql = "SELECT * FROM [Branches]";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Branch> list = new List<Branch>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Branch
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyName = row["Name"].ToString(),
                    MyCity = row["City"].ToString(),
                    MyAddress = row["Address"].ToString(),
                    MyPhone = row["Phone"].ToString()
                });
            }

            return list;
        }

        // showBranch
        public Branch showBranch(int Id)
        {
            string sql = "SELECT * FROM [Branches] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new Branch
            {
                MyId = Convert.ToInt32(row["Id"]),
                MyName = row["Name"].ToString(),
                MyCity = row["City"].ToString(),
                MyAddress = row["Address"].ToString(),
                MyPhone = row["Phone"].ToString()
            };
        }
    }
}
