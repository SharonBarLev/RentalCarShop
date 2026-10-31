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
    public class CartDB
    {
        public CartDB() { }
        public DBfunction db = new DBfunction();

        // isExistCart
        public bool isExistCart(int ProductID, string Username)
        {
            string sql = string.Format("select * from [Cart] where [ProductID]={0} and [Username]='{1}'", ProductID, Username);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // AddCart
        public void AddCart(int ProductID, string Username, decimal Price, string Picture, int Quantity)
        {
            string sql = string.Format(
                "insert into [Cart] ([ProductID], [Username], [Price], [Picture], [Quantity]) values ({0}, '{1}', {2}, '{3}', {4})",
                ProductID, Username, Price, Picture, Quantity);
            db.DoCommand(sql);
        }

        // DeleteCart
        public void DeleteCart(string Username)
        {
            string sql = string.Format("DELETE FROM [Cart] WHERE [Username]='{0}'", Username);
            db.DoCommand(sql);
        }


        // UpdateCart
        public void UpdateCart(int Quantity, string ProductID, string Username)
        {
            string sql = string.Format("UPDATE Cart SET quantity={0} WHERE [ProductID]= '{1}' AND [Username]='{2}'", Quantity, ProductID, Username);
            db.DoCommand(sql);
        }

        // showAllCart
        public DataSet showAllCart()
        {
            string sql = "select * from [Cart]";
            return db.Selectcommand(sql);
        }

        // showCart
        public DataSet showCart(int ProductID, string Username)
        {
            string sql = string.Format("select * from [Cart] where [ProductID]={0} and [Username]='{1}'", ProductID, Username);
            return db.Selectcommand(sql);
        }

        // DeletePruductFromCart
        public void DeletePruductFromCart(string ProductID, string Username)
        {
            string queryStr = string.Format("DELETE from tempCart WHERE [ProductID]= '{0}' and [Username]='{1}'", ProductID, Username);
            db.DoCommand(queryStr);
        }

        // isExistProductInCart
        public bool isExistProductInCart(int ProductID, string Username)
        {
            string sql = string.Format("select * from Cart where ProductID='{0}' and [Username]='{1}'", ProductID, Username);
            DataSet ds = db.Selectcommand(sql);
            return ds.Tables[0].Rows.Count >= 1;
        }

        // CopyData
        public void CopyData(string Username, decimal Price)
        {
            DateTime mydate = DateTime.Now;

            string sqlAddress = string.Format("SELECT * FROM [ShippingAddress] WHERE [Username] = '{0}'", Username);
            DataSet dsAddress = db.Selectcommand(sqlAddress);

            string Address = "";
            string AdditionalInstructions = "";

            if (dsAddress.Tables[0].Rows.Count > 0)
            {
                DataRow row = dsAddress.Tables[0].Rows[0];

                string CountryRegion = row["CountryRegion"].ToString();
                string City = row["City"].ToString();
                string StreetAddress = row["StreetAddress"].ToString();
                string Floor = row["Floor"].ToString();
                string ZipCode = row["ZipCode"].ToString();
                AdditionalInstructions = row["AdditionalInstructions"].ToString();

                Address = CountryRegion + ", " + City + ", " + StreetAddress + ", Floor " + Floor + ", " + ZipCode;
            }
            else
            {
                Address = "";
                AdditionalInstructions = "";
            }

            string sql1 = string.Format(
                "INSERT INTO [Order] ([Username], [OrderDate], [TotalPrice], [Address], [AdditionalInstructions]) " +
                "VALUES ('{0}', #{1}#, {2}, '{3}', '{4}')",
                Username, mydate.ToString("yyyy-MM-dd"), Price, Address, AdditionalInstructions);
            db.DoCommand(sql1);

            string sql2 = "SELECT TOP 1 OrderID FROM [Order] ORDER BY OrderID DESC";
            DataSet ds1 = db.Selectcommand(sql2);
            int HeshbonetNo = 0;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                HeshbonetNo = Convert.ToInt32(ds1.Tables[0].Rows[0][0]);
            }

            string sql3 = string.Format("SELECT * FROM [Cart] WHERE [Username] = '{0}'", Username);
            DataSet ds2 = db.Selectcommand(sql3);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                int ProductID = Convert.ToInt32(ds2.Tables[0].Rows[i]["ProductID"]);
                int Quantity = Convert.ToInt32(ds2.Tables[0].Rows[i]["Quantity"]);

                string sql4 = string.Format(
                    "INSERT INTO [OrderDetails] ([OrderID], [ProductID], [Quantity]) VALUES ({0}, {1}, {2})",
                    HeshbonetNo, ProductID, Quantity);
                db.DoCommand(sql4);
            }
        }

        // OrderInfo
        public string OrderInfo(string Username, decimal Price)
        {
            string str = "your shopping are:";
            string sql = "Select * FROM [Cart] where [username]='" + Username + "'";
            DataSet ds = db.Selectcommand(sql);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string serial = ds.Tables[0].Rows[i]["ProductName"].ToString();
                str += "  " + serial;
            }
            return str + "-----> total price=" + Price;
        }

        // UpdateCartQuantity
        public bool UpdateCartQuantity(string Username, string ProductID, int newQuantity)
        {
            string sql = string.Format("UPDATE Cart SET quantity={0} WHERE [ProductID]= '{1}' AND [Username]='{2}'", newQuantity, ProductID, Username);
            db.DoCommand(sql);
            string checkQuery = string.Format("SELECT COUNT(*) FROM Cart WHERE [ProductID]= '{0}' AND [Username]='{1}' AND Quantity={2}", ProductID, Username, newQuantity);
            DataSet ds = db.Selectcommand(checkQuery);
            return ds.Tables[0].Rows.Count > 0;
        }

        // GetCartTotalPrice
        public decimal GetCartTotalPrice(string Username)
        {
            decimal total = 0;
            string sql = string.Format("SELECT SUM(Quantity * Price) FROM Cart WHERE Username = '{0}'", Username);
            DataSet ds = db.Selectcommand(sql);
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                total = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
            }
            return total;
        }

        // CountItemsInCart
        public int CountItemsInCart(string Username)
        {
            int count = 0;
            string sql = string.Format("SELECT SUM(Quantity) FROM Cart WHERE Username = '{0}'", Username);
            DataSet ds = db.Selectcommand(sql);
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            return count;
        }

        // ListCartWithProductDetails
        public DataSet ListCartWithProductDetails(string Username)
        {
            string sql = string.Format(
                "SELECT Cart.ProductID, Product.ProductName, Product.Price, Cart.Quantity " +
                "FROM Cart INNER JOIN Product ON Cart.ProductID = Product.ProductID " +
                "WHERE Cart.Username = '{0}'",
                Username);
            DataSet ds = db.Selectcommand(sql);
            return ds;
        }
    }
}

