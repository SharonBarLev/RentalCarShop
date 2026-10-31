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
    public class BrandDB
    {
        public BrandDB() { }
        public DBfunction db = new DBfunction();

        // isExistBrand
        public bool isExistBrand(int BrandID)
        {
            string sql = string.Format("select * from [Brand] where [BrandID]={0}", BrandID);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddBrand
        public void AddBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description)
        {
            string sql = string.Format(
                "insert into [Brand] ([BrandName], [CountryOfOrigin], [WebsiteURL], [Description]) values ('{0}', '{1}', '{2}', '{3}')",
                BrandName, CountryOfOrigin, WebsiteURL, Description);
            db.DoCommand(sql);
        }

        // DeleteBrand
        public void DeleteBrand(int BrandID)
        {
            string sql = string.Format("delete from [Brand] where [BrandID]={0}", BrandID);
            db.DoCommand(sql);
        }

        // UpdateBrand
        public void UpdateBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description, int BrandID)
        {
            string sql = string.Format(
                "update [Brand] set [BrandName]='{0}', [CountryOfOrigin]='{1}', [WebsiteURL]='{2}', [Description]='{3}' where [BrandID]='{3}'",
                BrandName, CountryOfOrigin, WebsiteURL, Description, BrandID);
            db.DoCommand(sql);
        }

        // showAllBrand
        public DataSet showAllBrand()
        {
            string sql = "select * from [Brand]";
            return db.Selectcommand(sql);
        }

        // showBrand
        public DataSet showBrand(int BrandID)
        {
            string sql = string.Format("select * from [Brand] where [BrandID]={0}", BrandID);
            return db.Selectcommand(sql);
        }
    }
}

