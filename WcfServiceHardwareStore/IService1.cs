using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using BusinessModelCarShop;

namespace WcfServiceCarShop
{
    
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
        List<Brand> showAllBrand();
        [OperationContract]
        Brand showBrand(int BrandID);

        // Cart
        [OperationContract]
        bool isExistCart(string Username);
        [OperationContract]
        void AddCart(int ProductID, string Username, int Quantity);
        [OperationContract]
        void DeleteCart(string Username);
        [OperationContract]
        List<Cart> showAllCart();
        [OperationContract]
        List<Cart> showCart(string Username);
        [OperationContract]
        void DeletePruductFromCart(int ProductID, string Username);
        [OperationContract]
        bool isExistProductInCart(int ProductID, string Username);
        [OperationContract]
        void CopyData(string Username, decimal Price);
        [OperationContract]
        bool UpdateCartQuantity(string Username, int ProductID, int newQuantity);
        [OperationContract]
        decimal GetCartTotalPrice(string Username);
        [OperationContract]
        int CountItemsInCart(string Username);
        [OperationContract]
        List<Cart> ListCartWithProductDetails(string Username);
        [OperationContract]
        decimal GetProductTotalPriceInCart(string Username, int ProductID);
        [OperationContract]
        bool UpdateCartPrice(string Username, int ProductID, decimal NewPrice);
        [OperationContract]
        Cart showCartWithoutUsername(string Username);
        [OperationContract]
        Cart showProductInCart(int ProductID, string Username);
        [OperationContract]
        void IncreaseQuantityByOne(string Username, int ProductID);
        [OperationContract]
        void DecreaseQuantityByOne(string Username, int ProductID);

        // Category
        [OperationContract]
        bool isExistCategory(int CategoryID);
        [OperationContract]
        void AddCategory(string CategoryName, string Description);
        [OperationContract]
        void DeleteCategory(int CategoryID);
        [OperationContract]
        void UpdateCategory(string CategoryName, string Description, int CategoryID);
        [OperationContract]
        List<Category> showAllCategory();
        [OperationContract]
        Category showCategory(int CategoryID);
        [OperationContract]
        void AddProductFromCategory(int CategoryID);

        // Contact
        [OperationContract]
        bool isExistContact(int ContactID);
        [OperationContract]
        void AddContact(string Username, string FullName, string Email, string Subject, string Message, DateTime DateSent);

        [OperationContract]
        void UpdateContactStatus(int ContactID, bool IsTreated);

        [OperationContract]
        void DeleteContact(int ContactID);

        [OperationContract]
        List<Contact> showAllContact();

        [OperationContract]
        Contact ShowContact(int ContactID);

        [OperationContract]
        List<Contact> GetContactsByUser(string Username);

        // Order
        [OperationContract]
        bool isExistOrder(int OrderID);
        [OperationContract]
        void AddOrder(string Username, DateTime OrderDate, decimal TotalPrice, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions);
        [OperationContract]
        void DeleteOrder(int OrderID);
        [OperationContract]
        void UpdateOrder(string Username, DateTime OrderDate, decimal TotalPrice, string Address, string AdditionalInstructions, bool Delivered, int OrderID);
        [OperationContract]
        List<Order> showAllOrders();
        [OperationContract]
        Order showOrder(int OrderID);
        [OperationContract]
        void UpdateOrderStatus(int OrderID, bool Delivered);
        [OperationContract]
        List<Order> showPendingOrders();
        [OperationContract]
        List<Order> showOrdersByUsername(string Username);

        // OrderDetails
        [OperationContract]
        bool isExistOrderDetails(int OrderDetailsID);
        [OperationContract]
        void AddOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice);
        [OperationContract]
        void DeleteOrderDetails(int OrderDetailsID);
        [OperationContract]
        void UpdateOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice, int OrderDetailsID);
        [OperationContract]
        List<OrderDetails> showAllOrderDetails();
        [OperationContract]
        OrderDetails showOrderDetails(int OrderDetailsID);

        // Product
        [OperationContract]
        bool isExistProduct(int ProductID);
        [OperationContract]
        int GetNextProductID(int CategoryID);
        [OperationContract]
        void AddProduct(int ProductID, string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, string Picture);
        [OperationContract]
        void DeleteProduct(int ProductID);
        [OperationContract]
        void UpdateProduct(string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, string Picture, int ProductID);
        [OperationContract]
        List<Product> showAllProduct();
        [OperationContract]
        Product showProduct(int ProductID);
        [OperationContract]
        List<Product> showAllProductsInnerJoin();
        [OperationContract]
        List<Product> showAllProductByCategoryID(int CategoryID);
        [OperationContract]
        List<Product> showAllProductByBrandID(int BrandID);
        [OperationContract]
        List<Product> showAllProductsPriceDesc();
        [OperationContract]
        List<Product> showAllProductsPriceAsc();
        [OperationContract]
        List<Product> SearchProducts(string SearchTerm);
        [OperationContract]
        List<Product> FilterProductsByPriceRange(decimal MinPrice, decimal MaxPrice);
        [OperationContract]
        List<Product> GetProductsUnder50();
        [OperationContract]
        List<Product> GetProductsBetween51And100();
        [OperationContract]
        List<Product> GetProductsBetween101And200();
        [OperationContract]
        List<Product> GetProductsBetween201And500();
        [OperationContract]
        List<Product> GetProductsAbove500();

        // Review
        [OperationContract]
        bool isExistReview(int ReviewID);
        [OperationContract]
        void AddReview(string Username, int ProductID, string Rev, int Rating);
        [OperationContract]
        void DeleteReview(int ReviewID);
        [OperationContract]
        void UpdateReview(string Username, int ProductID, string Rev, int Rating, int ReviewID);
        [OperationContract]
        List<Review> ShowAllReview();
        [OperationContract]
        Review ShowReview(int ReviewID);
        [OperationContract]
        List<Review> ShowAllUserReview(string Username);
        [OperationContract]
        List<Review> ShowAllProductReview(int ProductID);

        // ShippingAddress
        [OperationContract]
        bool isExistShippingAddress(string Username);
        [OperationContract]
        void AddShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions);
        [OperationContract]
        void UpdateShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions);
        [OperationContract]
        void DeleteShippingAddress(string Username);
        [OperationContract]
        List<ShippingAddress> ShowAllShippingAddress();
        [OperationContract]
        ShippingAddress ShowShippingAddress(string Username);

        // User
        [OperationContract]
        bool isExistUser(string Username);
        [OperationContract]
        void AddUser(string Username, string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate);
        [OperationContract]
        void DeleteUser(string Username);
        [OperationContract]
        void UpdateUser(string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate, string Username);
        [OperationContract]
        List<User> showAllUsers();
        [OperationContract]
        User showUser(string Username);
        [OperationContract]
        bool IsExistUserAndPass(string Username, string Password);
        [OperationContract]
        bool IsUserAdmin(string username);
        [OperationContract]
        User ShowUserByEmail(string Email);

        // CarUser
        [OperationContract]
        bool isExistCarUser(int Id);
        [OperationContract]
        void AddCarUser(string Username, string Password, string FirstName, string LastName, string Email, string Phone, bool IsAdmin);
        [OperationContract]
        void DeleteCarUser(int Id);
        [OperationContract]
        void UpdateCarUser(string Username, string Password, string FirstName, string LastName, string Email, string Phone, bool IsAdmin, int Id);
        [OperationContract]
        List<CarUser> showAllCarUsers();
        [OperationContract]
        CarUser showCarUser(int Id);

        // Branch
        [OperationContract]
        bool isExistBranch(int Id);
        [OperationContract]
        void AddBranch(string Name, string City, string Address, string Phone);
        [OperationContract]
        void DeleteBranch(int Id);
        [OperationContract]
        void UpdateBranch(string Name, string City, string Address, string Phone, int Id);
        [OperationContract]
        List<Branch> showAllBranches();
        [OperationContract]
        Branch showBranch(int Id);

        // Car
        [OperationContract]
        bool isExistCar(int Id);
        [OperationContract]
        void AddCar(string Manufacturer, string Model, int Year, string Category, decimal Price, int Stock, string ImageUrl, string Description, int BranchId, int Seats, string FuelType, int RangeKm, string Transmission, int LuggageCapacity);
        [OperationContract]
        void DeleteCar(int Id);
        [OperationContract]
        void UpdateCar(string Manufacturer, string Model, int Year, string Category, decimal Price, int Stock, string ImageUrl, string Description, int BranchId, int Seats, string FuelType, int RangeKm, string Transmission, int LuggageCapacity, int Id);
        [OperationContract]
        List<Car> showAllCars();
        [OperationContract]
        Car showCar(int Id);
        [OperationContract]
        List<Car> showCarsByBranchId(int BranchId);

        // CarOrder
        [OperationContract]
        bool isExistCarOrder(int Id);
        [OperationContract]
        void AddCarOrder(int UserId, int CarId, DateTime OrderDate, DateTime StartDate, DateTime EndDate, decimal TotalPrice, string Status);
        [OperationContract]
        void DeleteCarOrder(int Id);
        [OperationContract]
        void UpdateCarOrder(int UserId, int CarId, DateTime OrderDate, DateTime StartDate, DateTime EndDate, decimal TotalPrice, string Status, int Id);
        [OperationContract]
        List<CarOrder> showAllCarOrders();
        [OperationContract]
        CarOrder showCarOrder(int Id);
        [OperationContract]
        List<CarOrder> showCarOrdersByUserId(int UserId);

        // Favorite
        [OperationContract]
        bool isExistFavorite(int Id);
        [OperationContract]
        void AddFavorite(int UserId, int CarId, DateTime AddedDate);
        [OperationContract]
        void DeleteFavorite(int Id);
        [OperationContract]
        void UpdateFavorite(int UserId, int CarId, DateTime AddedDate, int Id);
        [OperationContract]
        List<Favorite> showAllFavorites();
        [OperationContract]
        Favorite showFavorite(int Id);
        [OperationContract]
        List<Favorite> showFavoritesByUserId(int UserId);

        // CarReview
        [OperationContract]
        bool isExistCarReview(int Id);
        [OperationContract]
        void AddCarReview(int UserId, int CarId, int OrderId, int Rating, string Comment, DateTime ReviewDate);
        [OperationContract]
        void DeleteCarReview(int Id);
        [OperationContract]
        void UpdateCarReview(int UserId, int CarId, int OrderId, int Rating, string Comment, DateTime ReviewDate, int Id);
        [OperationContract]
        List<CarReview> showAllCarReviews();
        [OperationContract]
        CarReview showCarReview(int Id);
        [OperationContract]
        List<CarReview> showCarReviewsByCarId(int CarId);
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

