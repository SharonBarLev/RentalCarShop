using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class FavoriteDB
    {
        public FavoriteDB() { }

        public DBfunction db = new DBfunction();

        // isExistFavorite
        public bool isExistFavorite(int Id)
        {
            string sql = "SELECT * FROM [Favorites] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }

        // AddFavorite
        public void AddFavorite(int UserId, int CarId, DateTime AddedDate)
        {
            string sql = @"INSERT INTO [Favorites] ([UserId], [CarId], [AddedDate]) VALUES (?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);
            cmd.Parameters.AddWithValue("?", CarId);
            cmd.Parameters.AddWithValue("?", AddedDate);

            db.DoCommandInjection(cmd);
        }

        // DeleteFavorite
        public void DeleteFavorite(int Id)
        {
            string sql = "DELETE FROM [Favorites] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // UpdateFavorite
        public void UpdateFavorite(int UserId, int CarId, DateTime AddedDate, int Id)
        {
            string sql = @"UPDATE [Favorites] SET [UserId] = ?, [CarId] = ?, [AddedDate] = ? WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);
            cmd.Parameters.AddWithValue("?", CarId);
            cmd.Parameters.AddWithValue("?", AddedDate);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // showAllFavorites
        public List<Favorite> showAllFavorites()
        {
            string sql = "SELECT * FROM [Favorites]";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Favorite> list = new List<Favorite>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Favorite
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUserId = Convert.ToInt32(row["UserId"]),
                    MyCarId = Convert.ToInt32(row["CarId"]),
                    MyAddedDate = Convert.ToDateTime(row["AddedDate"])
                });
            }

            return list;
        }

        // showFavorite
        public Favorite showFavorite(int Id)
        {
            string sql = "SELECT * FROM [Favorites] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new Favorite
            {
                MyId = Convert.ToInt32(row["Id"]),
                MyUserId = Convert.ToInt32(row["UserId"]),
                MyCarId = Convert.ToInt32(row["CarId"]),
                MyAddedDate = Convert.ToDateTime(row["AddedDate"])
            };
        }

        // showFavoritesByUserId
        public List<Favorite> showFavoritesByUserId(int UserId)
        {
            string sql = "SELECT * FROM [Favorites] WHERE [UserId] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<Favorite> list = new List<Favorite>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Favorite
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUserId = Convert.ToInt32(row["UserId"]),
                    MyCarId = Convert.ToInt32(row["CarId"]),
                    MyAddedDate = Convert.ToDateTime(row["AddedDate"])
                });
            }

            return list;
        }
    }
}
