using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ViewModelCarShop;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        // Brand
        public bool isExistBrand(int BrandID)
        {
            BrandDB b = new BrandDB();
            return b.isExistBrand(BrandID);
        }
        public void AddBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description)
        {
            BrandDB b = new BrandDB();
            b.AddBrand(BrandName, CountryOfOrigin, WebsiteURL, Description);
        }
        public void DeleteBrand(int BrandID)
        {
            BrandDB b = new BrandDB();
            b.DeleteBrand(BrandID);
        }
        public void UpdateBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description, int BrandID)
        {
            BrandDB b = new BrandDB();
            b.UpdateBrand(BrandName, CountryOfOrigin, WebsiteURL, Description, BrandID);
        }
        public DataSet showAllBrand()
        {
            BrandDB b = new BrandDB();
            return b.showAllBrand();
        }
        public DataSet showBrand(int BrandID)
        {
            BrandDB b = new BrandDB();
            return b.showBrand(BrandID);
        }

        // Cart
        public bool isExistCart(int ProductID, string Username)
        {
            CartDB c = new CartDB();
            return c.isExistCart(ProductID, Username);
        }
        public void AddCart(int ProductID, string Username, decimal Price, string Picture, int Quantity)
        {
            CartDB c = new CartDB();
            c.AddCart(ProductID, Username, Price, Picture, Quantity);
        }
        public void DeleteCart(string Username)
        {
            CartDB c = new CartDB();
            c.DeleteCart(Username);
        }
        public void UpdateCart(int Quantity, string ProductID, string Username)
        {
            CartDB c = new CartDB();
            c.UpdateCart(Quantity, ProductID, Username);
        }
        public DataSet showAllCart()
        {
            CartDB c = new CartDB();
            return c.showAllCart();
        }
        public DataSet showCart(int ProductID, string Username)
        {
            CartDB c = new CartDB();
            return c.showCart(ProductID, Username);
        }
        public void DeletePruductFromCart(string ProductID, string Username)
        {
            CartDB c = new CartDB();
            c.DeletePruductFromCart(ProductID, Username);
        }
        public bool isExistProductInCart(int ProductID, string Username)
        {
            CartDB c = new CartDB();
            return c.isExistProductInCart(ProductID, Username);
        }
        public void CopyData(string Username, decimal Price)
        {
            CartDB c = new CartDB();
            c.CopyData(Username, Price);
        }
        public string OrderInfo(string Username, decimal Price)
        {
            CartDB c = new CartDB();
            return c.OrderInfo(Username, Price);
        }
        public bool UpdateCartQuantity(string Username, string ProductID, int newQuantity)
        {
            CartDB c = new CartDB();
            return c.UpdateCartQuantity(Username, ProductID, newQuantity);
        }
        public decimal GetCartTotalPrice(string Username)
        {
            CartDB c = new CartDB();
            return c.GetCartTotalPrice(Username);
        }
        public int CountItemsInCart(string Username)
        {
            CartDB c = new CartDB();
            return c.CountItemsInCart(Username);
        }
        public DataSet ListCartWithProductDetails(string Username)
        {
            CartDB c = new CartDB();
            return c.ListCartWithProductDetails(Username);
        }

    }
}

