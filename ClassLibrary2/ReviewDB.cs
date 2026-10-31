using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using BusinessModelCarShop;

namespace ViewModelCarShop
{
    public class ReviewDB
    {
        public ReviewDB() { }
        public DBfunction db = new DBfunction();

        // isExistReview
        public bool isExistReview(int ReviewID)
        {
            string sql = "SELECT * FROM [Review] WHERE [ReviewID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ReviewID);
            DataSet ds = db.SelectcommandInjection(cmd);
            return ds.Tables[0].Rows.Count > 0;
        }

        // AddReview
        public void AddReview(string Username, int ProductID, string Rev, int Rating)
        {
            string sql = "INSERT INTO [Review] ([Username], [ProductID], [Rev], [Rating]) VALUES (?, ?, ?, ?)";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", ProductID);
            cmd.Parameters.AddWithValue("?", Rev);
            cmd.Parameters.AddWithValue("?", Rating);
            db.DoCommandInjection(cmd);
        }

        // DeleteReview
        public void DeleteReview(int ReviewID)
        {
            string sql = "DELETE FROM [Review] WHERE [ReviewID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ReviewID);
            db.DoCommandInjection(cmd);
        }

        // UpdateReview
        public void UpdateReview(string Username, int ProductID, string Rev, int Rating, int ReviewID)
        {
            string sql = "UPDATE [Review] SET [Username] = ?, [ProductID] = ?, [Rev] = ?, [Rating] = ? WHERE [ReviewID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            cmd.Parameters.AddWithValue("?", ProductID);
            cmd.Parameters.AddWithValue("?", Rev);
            cmd.Parameters.AddWithValue("?", Rating);
            cmd.Parameters.AddWithValue("?", ReviewID);
            db.DoCommandInjection(cmd);
        }

        // ShowAllReview
        public List<Review> ShowAllReview()
        {
            string sql = "SELECT * FROM [Review]";
            OleDbCommand cmd = new OleDbCommand(sql);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Review> list = new List<Review>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Review
                {
                    MyReviewID = Convert.ToInt32(row["ReviewID"]),
                    MyUsername = row["Username"].ToString(),
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyRev = row["Rev"].ToString(),
                    MyRating = Convert.ToInt32(row["Rating"])
                });
            }
            return list;
        }

        // ShowReview
        public Review ShowReview(int ReviewID)
        {
            string sql = "SELECT * FROM [Review] WHERE [ReviewID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ReviewID);
            DataSet ds = db.SelectcommandInjection(cmd);

            if (ds.Tables[0].Rows.Count == 0) return null;

            DataRow row = ds.Tables[0].Rows[0];
            return new Review
            {
                MyReviewID = Convert.ToInt32(row["ReviewID"]),
                MyUsername = row["Username"].ToString(),
                MyProductID = Convert.ToInt32(row["ProductID"]),
                MyRev = row["Rev"].ToString(),
                MyRating = Convert.ToInt32(row["Rating"])
            };
        }

        // ShowAllUserReview
        public List<Review> ShowAllUserReview(string Username)
        {
            string sql = "SELECT * FROM [Review] WHERE [Username] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", Username);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Review> list = new List<Review>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Review
                {
                    MyReviewID = Convert.ToInt32(row["ReviewID"]),
                    MyUsername = row["Username"].ToString(),
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyRev = row["Rev"].ToString(),
                    MyRating = Convert.ToInt32(row["Rating"])
                });
            }
            return list;
        }

        // ShowAllProductReview
        public List<Review> ShowAllProductReview(int ProductID)
        {
            string sql = "SELECT * FROM [Review] WHERE [ProductID] = ?";
            OleDbCommand cmd = new OleDbCommand(sql);
            cmd.Parameters.AddWithValue("?", ProductID);
            DataSet ds = db.SelectcommandInjection(cmd);

            List<Review> list = new List<Review>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(new Review
                {
                    MyReviewID = Convert.ToInt32(row["ReviewID"]),
                    MyUsername = row["Username"].ToString(),
                    MyProductID = Convert.ToInt32(row["ProductID"]),
                    MyRev = row["Rev"].ToString(),
                    MyRating = Convert.ToInt32(row["Rating"])
                });
            }
            return list;
        }
    }
}
