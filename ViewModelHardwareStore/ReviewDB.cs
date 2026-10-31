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
    public class ReviewDB
    {
        public ReviewDB() { }
        public DBfunction db = new DBfunction();

        // isExistReview
        public bool isExistReview(int ReviewID)
        {
            string sql = string.Format("select * from [Review] where [ReviewID]={0}", ReviewID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddReview
        public void AddReview(string Username, int ProductID, string Rev, int Rating)
        {
            string sql = string.Format(
                "insert into [Review] ([Username], [ProductID], [Rev], [Rating]) values ('{0}', {1}, '{2}', {3})",
                Username, ProductID, Rev, Rating);
            db.DoCommand(sql);
        }

        // DeleteReview
        public void DeleteReview(int ReviewID)
        {
            string sql = string.Format("delete from [Review] where [ReviewID]={0}", ReviewID);
            db.DoCommand(sql);
        }

        // UpdateReview
        public void UpdateReview(string Username, int ProductID, string Rev, int Rating, int ReviewID)
        {
            string sql = string.Format(
                "update [Review] set [Username]='{0}', [ProductID]={1}, [Rev]='{2}', [Rating]={3} where [ReviewID]={4}",
                Username, ProductID, Rev, Rating, ReviewID);
            db.DoCommand(sql);
        }

        // ShowAllReview
        public DataSet ShowAllReview()
        {
            string sql = "select * from [Review]";
            return db.Selectcommand(sql);
        }

        // ShowReview
        public DataSet ShowReview(int ReviewID)
        {
            string sql = string.Format("select * from [Review] where [ReviewID]={0}", ReviewID);
            return db.Selectcommand(sql);
        }

        // ShowAllUserReview
        public DataSet ShowAllUserReview(string Username)
        {
            string sql = string.Format("select * from [Review] where [Username]={0}", Username);
            return db.Selectcommand(sql);
        }

        // ShowAllProductReview
        public DataSet ShowAllProductReview(int ProductID)
        {
            string sql = string.Format("select * from [Review] where [ProductID]={0}", ProductID);
            return db.Selectcommand(sql);
        }
    }
}

