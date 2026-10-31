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
    public class ShippingAddressDB
    {
        public ShippingAddressDB() { }
        public DBfunction db = new DBfunction();

        public bool IsExistShippingAddress(string Username)
        {
            string sql = string.Format("SELECT * FROM [ShippingAddress] WHERE [Username] = '{0}'", Username);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        public void AddShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            string sql = string.Format(
                "INSERT INTO [ShippingAddress] ([Username], [CountryRegion], [City], [StreetAddress], [Floor], [ZipCode], [AdditionalInstructions]) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                Username, CountryRegion, City, StreetAddress, Floor, ZipCode, AdditionalInstructions);
            db.DoCommand(sql);
        }

        public void UpdateShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            string sql = string.Format(
                "UPDATE [ShippingAddress] SET [CountryRegion] = '{0}', [City] = '{1}', [StreetAddress] = '{2}', [Floor] = '{3}', [ZipCode] = '{4}', [AdditionalInstructions] = '{5}' " +
                "WHERE [Username] = '{6}'",
                CountryRegion, City, StreetAddress, Floor, ZipCode, AdditionalInstructions, Username);
            db.DoCommand(sql);
        }

        public void DeleteShippingAddress(string Username)
        {
            string sql = string.Format("DELETE FROM [ShippingAddress] WHERE [Username] = '{0}'", Username);
            db.DoCommand(sql);
        }
    }
}

