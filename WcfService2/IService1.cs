using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here


        // Brand
        [OperationContract]
        bool isExistBrand(int BrandID);
        [OperationContract]
        void AddBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description);
        [OperationContract]
        void DeleteBrand(int BrandID);
        [OperationContract]
        void UpdateBrand(string BrandName, string CountryOfOrigin, string WebsiteURL, string Description, int BrandID);
        [OperationContract]
        DataSet showAllBrand();
        [OperationContract]
        DataSet showBrand(int BrandID);

        // Cart
        [OperationContract]
        bool isExistCart(int ProductID, string Username);
        [OperationContract]
        void AddCart(int ProductID, string Username, decimal Price, string Picture, int Quantity);
        [OperationContract]
        void DeleteCart(string Username);
        [OperationContract]
        void UpdateCart(int Quantity, string ProductID, string Username);
        [OperationContract]
        DataSet showAllCart();
        [OperationContract]
        DataSet showCart(int ProductID, string Username);
        [OperationContract]
        void DeletePruductFromCart(string ProductID, string Username);
        [OperationContract]
        bool isExistProductInCart(int ProductID, string Username);
        [OperationContract]
        void CopyData(string Username, decimal Price);
        [OperationContract]
        string OrderInfo(string Username, decimal Price);
        [OperationContract]
        bool UpdateCartQuantity(string Username, string ProductID, int newQuantity);
        [OperationContract]
        decimal GetCartTotalPrice(string Username);
        [OperationContract]
        int CountItemsInCart(string Username);
        [OperationContract]
        DataSet ListCartWithProductDetails(string Username);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
