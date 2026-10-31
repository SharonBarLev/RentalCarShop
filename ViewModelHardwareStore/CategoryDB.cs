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
    public class CategoryDB
    {
        public CategoryDB() { }
        public DBfunction db = new DBfunction();

        // isExistCategory
        public bool isExistCategory(int CategoryID)
        {
            string sql = string.Format("select * from [Category] where [CategoryID]={0}", CategoryID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddCategory
        public void AddCategory(string CategoryName, string Description)
        {
            string sql = string.Format(
                "insert into [Category] ([CategoryName], [Description]) values ('{0}','{1}')",
                CategoryName, Description);
            db.DoCommand(sql);
        }

        // DeleteCategory
        public void DeleteCategory(int CategoryID)
        {
            string sql = string.Format("delete from [Category] where [CategoryID]={0}", CategoryID);
            db.DoCommand(sql);
        }

        // UpdateCategory
        public void UpdateCategory(string CategoryName, string Description, int CategoryID)
        {
            string sql = string.Format(
                "update [Category] set [CategoryName]='{0}', [Description]='{1}' where [CategoryID]={2}",
                CategoryName, Description, CategoryID);
            db.DoCommand(sql);
        }

        // showAllCategory
        public DataSet showAllCategory()
        {
            string sql = "select * from [Category]";
            return db.Selectcommand(sql);
        }

        // showCategory
        public DataSet showCategory(int CategoryID)
        {
            string sql = string.Format("select * from [Category] where [CategoryID]={0}", CategoryID);
            return db.Selectcommand(sql);
        }
    }
}

