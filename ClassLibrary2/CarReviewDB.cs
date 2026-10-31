using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class CarReviewDB
    {
        public CarReviewDB() { }

        public DBfunction db = new DBfunction();

        // isExistCarReview
        public bool isExistCarReview(int Id)
        {
            string sql = "SELECT * FROM [Reviews] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            return ds.Tables[0].Rows.Count > 0;
        }

        // AddCarReview
        public void AddCarReview(int UserId, int CarId, int OrderId, int Rating, string Comment, DateTime ReviewDate)
        {
            string sql = @"INSERT INTO [Reviews] ([UserId], [CarId], [OrderId], [Rating], [Comment], [ReviewDate]) VALUES (?, ?, ?, ?, ?, ?)";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);
            cmd.Parameters.AddWithValue("?", CarId);
            cmd.Parameters.AddWithValue("?", OrderId);
            cmd.Parameters.AddWithValue("?", Rating);
            cmd.Parameters.AddWithValue("?", Comment);
            cmd.Parameters.AddWithValue("?", ReviewDate);

            db.DoCommandInjection(cmd);
        }

        // DeleteCarReview
        public void DeleteCarReview(int Id)
        {
            string sql = "DELETE FROM [Reviews] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // UpdateCarReview
        public void UpdateCarReview(int UserId, int CarId, int OrderId, int Rating, string Comment, DateTime ReviewDate, int Id)
        {
            string sql = @"UPDATE [Reviews] SET [UserId] = ?, [CarId] = ?, [OrderId] = ?, [Rating] = ?, [Comment] = ?, [ReviewDate] = ? WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", UserId);
            cmd.Parameters.AddWithValue("?", CarId);
            cmd.Parameters.AddWithValue("?", OrderId);
            cmd.Parameters.AddWithValue("?", Rating);
            cmd.Parameters.AddWithValue("?", Comment);
            cmd.Parameters.AddWithValue("?", ReviewDate);
            cmd.Parameters.AddWithValue("?", Id);

            db.DoCommandInjection(cmd);
        }

        // showAllCarReviews
        public List<CarReview> showAllCarReviews()
        {
            string sql = "SELECT * FROM [Reviews]";

            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<CarReview> list = new List<CarReview>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new CarReview
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUserId = Convert.ToInt32(row["UserId"]),
                    MyCarId = Convert.ToInt32(row["CarId"]),
                    MyOrderId = Convert.ToInt32(row["OrderId"]),
                    MyRating = Convert.ToInt32(row["Rating"]),
                    MyComment = row["Comment"].ToString(),
                    MyReviewDate = Convert.ToDateTime(row["ReviewDate"])
                });
            }

            return list;
        }

        // showCarReview
        public CarReview showCarReview(int Id)
        {
            string sql = "SELECT * FROM [Reviews] WHERE [Id] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Id);

            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = ds.Tables[0].Rows[0];

            return new CarReview
            {
                MyId = Convert.ToInt32(row["Id"]),
                MyUserId = Convert.ToInt32(row["UserId"]),
                MyCarId = Convert.ToInt32(row["CarId"]),
                MyOrderId = Convert.ToInt32(row["OrderId"]),
                MyRating = Convert.ToInt32(row["Rating"]),
                MyComment = row["Comment"].ToString(),
                MyReviewDate = Convert.ToDateTime(row["ReviewDate"])
            };
        }

        // showCarReviewsByCarId
        public List<CarReview> showCarReviewsByCarId(int CarId)
        {
            string sql = "SELECT * FROM [Reviews] WHERE [CarId] = ?";

            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", CarId);

            DataSet ds = db.SelectcommandInjection(cmd);

            List<CarReview> list = new List<CarReview>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new CarReview
                {
                    MyId = Convert.ToInt32(row["Id"]),
                    MyUserId = Convert.ToInt32(row["UserId"]),
                    MyCarId = Convert.ToInt32(row["CarId"]),
                    MyOrderId = Convert.ToInt32(row["OrderId"]),
                    MyRating = Convert.ToInt32(row["Rating"]),
                    MyComment = row["Comment"].ToString(),
                    MyReviewDate = Convert.ToDateTime(row["ReviewDate"])
                });
            }

            return list;
        }
    }
}
