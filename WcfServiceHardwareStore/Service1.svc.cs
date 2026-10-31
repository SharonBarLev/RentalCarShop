using BusinessModelCarShop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ViewModelCarShop;


namespace WcfServiceCarShop
{
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
        public List<Brand> showAllBrand()
        {
            BrandDB b = new BrandDB();
            return b.showAllBrand();
        }
        public Brand showBrand(int BrandID)
        {
            BrandDB b = new BrandDB();
            return b.showBrand(BrandID);
        }

        // Cart
        public bool isExistCart(string Username)
        {
            CartDB c = new CartDB();
            return c.isExistCart(Username);
        }
        public void AddCart(int ProductID, string Username, int Quantity)
        {
            CartDB c = new CartDB();
            c.AddCart(ProductID, Username, Quantity);
        }
        public void DeleteCart(string Username)
        {
            CartDB c = new CartDB();
            c.DeleteCart(Username);
        }
        public List<Cart> showAllCart()
        {
            CartDB c = new CartDB();
            return c.showAllCart();
        }
        public List<Cart> showCart(string Username)
        {
            CartDB c = new CartDB();
            return c.showCart(Username);
        }
        public void DeletePruductFromCart(int ProductID, string Username)
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
        public bool UpdateCartQuantity(string Username, int ProductID, int newQuantity)
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
        public List<Cart> ListCartWithProductDetails(string Username)
        {
            CartDB c = new CartDB();
            return c.ListCartWithProductDetails(Username);
        }
        public decimal GetProductTotalPriceInCart(string Username, int ProductID)
        {
            CartDB c = new CartDB();
            return c.GetProductTotalPriceInCart(Username, ProductID);
        }
        public bool UpdateCartPrice(string Username, int ProductID, decimal NewPrice)
        {
            CartDB c = new CartDB();
            return c.UpdateCartPrice(Username, ProductID, NewPrice);
        }
        public Cart showCartWithoutUsername(string Username)
        {
            CartDB c = new CartDB();
            return c.showCartWithoutUsername(Username);
        }
        public Cart showProductInCart(int ProductID, string Username)
        {
            CartDB c = new CartDB();
            return c.showProductInCart(ProductID, Username);
        }
        public void IncreaseQuantityByOne(string Username, int ProductID)
        {
            CartDB c = new CartDB();
            c.IncreaseQuantityByOne(Username, ProductID);
        }
        public void DecreaseQuantityByOne(string Username, int ProductID)
        {
            CartDB c = new CartDB();
            c.DecreaseQuantityByOne(Username, ProductID);
        }

        // Category
        public bool isExistCategory(int CategoryID)
        {
            CategoryDB c = new CategoryDB();
            return c.isExistCategory(CategoryID);
        }
        public void AddCategory(string CategoryName, string Description)
        {
            CategoryDB c = new CategoryDB();
            c.AddCategory(CategoryName, Description);
        }
        public void DeleteCategory(int CategoryID)
        {
            CategoryDB c = new CategoryDB();
            c.DeleteCategory(CategoryID);
        }
        public void UpdateCategory(string CategoryName, string Description, int CategoryID)
        {
            CategoryDB c = new CategoryDB();
            c.UpdateCategory(CategoryName, Description, CategoryID);
        }
        public List<Category> showAllCategory()
        {
            CategoryDB c = new CategoryDB();
            return c.showAllCategory();
        }
        public Category showCategory(int CategoryID)
        {
            CategoryDB c = new CategoryDB();
            return c.showCategory(CategoryID);
        }
        public void AddProductFromCategory(int CategoryID)
        {
            CategoryDB c = new CategoryDB();
            c.AddProductFromCategory(CategoryID);
        }

        // Contact
        public bool isExistContact(int ContactID)
        {
            ContactDB c = new ContactDB();
            return c.isExistContact(ContactID);
        }

        public void AddContact(string Username, string FullName, string Email, string Subject, string Message, DateTime DateSent)
        {
            ContactDB c = new ContactDB();
            c.AddContact(Username, FullName, Email, Subject, Message, DateSent);
        }

        public void UpdateContactStatus(int ContactID, bool IsTreated)
        {
            ContactDB c = new ContactDB();
            c.UpdateContactStatus(ContactID, IsTreated);
        }

        public void DeleteContact(int ContactID)
        {
            ContactDB c = new ContactDB();
            c.DeleteContact(ContactID);
        }

        public List<Contact> showAllContact()
        {
            ContactDB c = new ContactDB();
            return c.showAllContact();
        }

        public Contact ShowContact(int ContactID)
        {
            ContactDB c = new ContactDB();
            return c.ShowContact(ContactID);
        }

        public List<Contact> GetContactsByUser(string Username)
        {
            ContactDB c = new ContactDB();
            return c.GetContactsByUser(Username);
        }

        // Order
        public bool isExistOrder(int OrderID)
        {
            OrderDB o = new OrderDB();
            return o.isExistOrder(OrderID);
        }
        public void AddOrder(string Username, DateTime OrderDate, decimal TotalPrice, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            OrderDB o = new OrderDB();
            o.AddOrder(Username, OrderDate, TotalPrice, CountryRegion, City, StreetAddress, Floor, ZipCode, AdditionalInstructions);
        }
        public void DeleteOrder(int OrderID)
        {
            OrderDB o = new OrderDB();
            o.DeleteOrder(OrderID);
        }
        public void UpdateOrder(string Username, DateTime OrderDate, decimal TotalPrice, string Address, string AdditionalInstructions, bool Delivered, int OrderID)
        {
            OrderDB o = new OrderDB();
            o.UpdateOrder(Username, OrderDate, TotalPrice, Address, AdditionalInstructions, Delivered, OrderID);
        }
        public List<Order> showAllOrders()
        {
            OrderDB o = new OrderDB();
            return o.showAllOrders();
        }
        public Order showOrder(int OrderID)
        {
            OrderDB o = new OrderDB();
            return o.showOrder(OrderID);
        }
        public void UpdateOrderStatus(int OrderID, bool Delivered)
        {
            OrderDB o = new OrderDB();
            o.UpdateOrderStatus(OrderID, Delivered);
        }
        public List<Order> showPendingOrders()
        {
            OrderDB o = new OrderDB();
            return o.showPendingOrders();
        }
        public List<Order> showOrdersByUsername(string Username)
        {
            OrderDB o = new OrderDB();
            return o.showOrdersByUsername(Username);
        }

        // OrderDetails
        public bool isExistOrderDetails(int OrderDetailsID)
        {
            OrderDetailsDB o = new OrderDetailsDB();
            return o.isExistOrderDetails(OrderDetailsID);
        }
        public void AddOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice)
        {
            OrderDetailsDB o = new OrderDetailsDB();
            o.AddOrderDetails(OrderID, ProductID, Quantity, UnitPrice);
        }
        public void DeleteOrderDetails(int OrderDetailsID)
        {
            OrderDetailsDB o = new OrderDetailsDB();
            o.DeleteOrderDetails(OrderDetailsID);
        }
        public void UpdateOrderDetails(int OrderID, int ProductID, int Quantity, decimal UnitPrice, int OrderDetailsID)
        {
            OrderDetailsDB o = new OrderDetailsDB();
            o.UpdateOrderDetails(OrderID, ProductID, Quantity, UnitPrice, OrderDetailsID);
        }
        public List<OrderDetails> showAllOrderDetails()
        {
            OrderDetailsDB o = new OrderDetailsDB();
            return o.showAllOrderDetails();
        }
        public OrderDetails showOrderDetails(int OrderDetailsID)
        {
            OrderDetailsDB o = new OrderDetailsDB();
            return o.showOrderDetails(OrderDetailsID);
        }

        // Product
        public bool isExistProduct(int ProductID)
        {
            ProductDB p = new ProductDB();
            return p.isExistProduct(ProductID);
        }
        public int GetNextProductID(int CategoryID)
        {
            ProductDB p = new ProductDB();
            return p.GetNextProductID(CategoryID);
        }
        public void AddProduct(int ProductID, string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, string Picture)
        {
            ProductDB p = new ProductDB();
            p.AddProduct(ProductID, ProductName, CategoryID, BrandID, Price, StockQuantity, Description, Picture);
        }
        public void DeleteProduct(int ProductID)
        {
            ProductDB p = new ProductDB();
            p.DeleteProduct(ProductID);
        }
        public void UpdateProduct(string ProductName, int CategoryID, int BrandID, decimal Price, int StockQuantity, string Description, string Picture, int ProductID)
        {
            ProductDB p = new ProductDB();
            p.UpdateProduct(ProductName, CategoryID, BrandID, Price, StockQuantity, Description, Picture, ProductID);
        }
        public List<Product> showAllProduct()
        {
            ProductDB p = new ProductDB();
            return p.showAllProduct();
        }
        public Product showProduct(int ProductID)
        {
            ProductDB p = new ProductDB();
            return p.showProduct(ProductID);
        }
        public List<Product> showAllProductsInnerJoin()
        {
            ProductDB p = new ProductDB();
            return p.showAllProductsInnerJoin();
        }
        public List<Product> showAllProductByCategoryID(int CategoryID)
        {
            ProductDB p = new ProductDB();
            return p.showAllProductByCategoryID(CategoryID);
        }
        public List<Product> showAllProductByBrandID(int BrandID)
        {
            ProductDB p = new ProductDB();
            return p.showAllProductByBrandID(BrandID);
        }
        public List<Product> showAllProductsPriceDesc()
        {
            ProductDB p = new ProductDB();
            return p.showAllProductsPriceDesc();
        }
        public List<Product> showAllProductsPriceAsc()
        {
            ProductDB p = new ProductDB();
            return p.showAllProductsPriceAsc();
        }
        public List<Product> SearchProducts(string SearchTerm)
        {
            ProductDB p = new ProductDB();
            return p.SearchProducts(SearchTerm);
        }
        public List<Product> FilterProductsByPriceRange(decimal MinPrice, decimal MaxPrice)
        {
            ProductDB p = new ProductDB();
            return p.FilterProductsByPriceRange(MinPrice, MaxPrice);
        }
        public List<Product> GetProductsUnder50()
        {
            ProductDB p = new ProductDB();
            return p.GetProductsUnder50();
        }
        public List<Product> GetProductsBetween51And100()
        {
            ProductDB p = new ProductDB();
            return p.GetProductsBetween51And100();
        }
        public List<Product> GetProductsBetween101And200()
        {
            ProductDB p = new ProductDB();
            return p.GetProductsBetween101And200();
        }
        public List<Product> GetProductsBetween201And500()
        {
            ProductDB p = new ProductDB();
            return p.GetProductsBetween201And500();
        }
        public List<Product> GetProductsAbove500()
        {
            ProductDB p = new ProductDB();
            return p.GetProductsAbove500();
        }

        // Review
        public bool isExistReview(int ReviewID)
        {
            ReviewDB r = new ReviewDB();
            return r.isExistReview(ReviewID);
        }
        public void AddReview(string Username, int ProductID, string Rev, int Rating)
        {
            ReviewDB r = new ReviewDB();
            r.AddReview(Username, ProductID, Rev, Rating);
        }
        public void DeleteReview(int ReviewID)
        {
            ReviewDB r = new ReviewDB();
            r.DeleteReview(ReviewID);
        }
        public void UpdateReview(string Username, int ProductID, string Rev, int Rating, int ReviewID)
        {
            ReviewDB r = new ReviewDB();
            r.UpdateReview(Username, ProductID, Rev, Rating, ReviewID);
        }
        public List<Review> ShowAllReview()
        {
            ReviewDB r = new ReviewDB();
            return r.ShowAllReview();
        }
        public Review ShowReview(int ReviewID)
        {
            ReviewDB r = new ReviewDB();
            return r.ShowReview(ReviewID);
        }
        public List<Review> ShowAllUserReview(string Username)
        {
            ReviewDB r = new ReviewDB();
            return r.ShowAllUserReview(Username);
        }
        public List<Review> ShowAllProductReview(int ProductID)
        {
            ReviewDB r = new ReviewDB();
            return r.ShowAllProductReview(ProductID);
        }

        // ShippingAddress
        public bool isExistShippingAddress(string Username)
        {
            ShippingAddressDB db = new ShippingAddressDB();
            return db.IsExistShippingAddress(Username);
        }
        public void AddShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            ShippingAddressDB db = new ShippingAddressDB();
            db.AddShippingAddress(Username, CountryRegion, City, StreetAddress, Floor, ZipCode, AdditionalInstructions);
        }
        public void UpdateShippingAddress(string Username, string CountryRegion, string City, string StreetAddress, string Floor, string ZipCode, string AdditionalInstructions)
        {
            ShippingAddressDB db = new ShippingAddressDB();
            db.UpdateShippingAddress(Username, CountryRegion, City, StreetAddress, Floor, ZipCode, AdditionalInstructions);
        }
        public void DeleteShippingAddress(string Username)
        {
            ShippingAddressDB db = new ShippingAddressDB();
            db.DeleteShippingAddress(Username);
        }
        public List<ShippingAddress> ShowAllShippingAddress()
        {
            ShippingAddressDB db = new ShippingAddressDB();
            return db.ShowAllShippingAddress();
        }
        public ShippingAddress ShowShippingAddress(string Username)
        {
            ShippingAddressDB db = new ShippingAddressDB();
            return db.ShowShippingAddress(Username);
        }

        // User
        public bool isExistUser(string Username)
        {
            UserDB u = new UserDB();
            return u.isExistUser(Username);
        }
        public void AddUser(string Username, string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate)
        {
            UserDB u = new UserDB();
            u.AddUser(Username, Password, PhoneNumber, Email, Admin, JoinDate);
        }
        public void DeleteUser(string Username)
        {
            UserDB u = new UserDB();
            u.DeleteUser(Username);
        }
        public void UpdateUser(string Password, string PhoneNumber, string Email, bool Admin, DateTime JoinDate, string Username)
        {
            UserDB u = new UserDB();
            u.UpdateUser(Password, PhoneNumber, Email, Admin, JoinDate, Username);
        }
        public List<User> showAllUsers()
        {
            UserDB u = new UserDB();
            return u.showAllUsers();
        }
        public User showUser(string Username)
        {
            UserDB u = new UserDB();
            return u.showUser(Username);
        }
        public bool IsExistUserAndPass(string Username, string Password)
        {
            UserDB u = new UserDB();
            return u.IsExistUserAndPass(Username, Password);
        }
        public bool IsUserAdmin(string username)
        {
            UserDB u = new UserDB();
            return u.IsUserAdmin(username);
        }
        public User ShowUserByEmail(string Email)
        {
            UserDB u = new UserDB();
            return u.ShowUserByEmail(Email);
        }

        // CarUser
        public bool isExistCarUser(int Id)
        {
            CarUserDB d = new CarUserDB();
            return d.isExistCarUser(Id);
        }
        public void AddCarUser(string Username, string Password, string FirstName, string LastName, string Email, string Phone, bool IsAdmin)
        {
            CarUserDB d = new CarUserDB();
            d.AddCarUser(Username, Password, FirstName, LastName, Email, Phone, IsAdmin);
        }
        public void DeleteCarUser(int Id)
        {
            CarUserDB d = new CarUserDB();
            d.DeleteCarUser(Id);
        }
        public void UpdateCarUser(string Username, string Password, string FirstName, string LastName, string Email, string Phone, bool IsAdmin, int Id)
        {
            CarUserDB d = new CarUserDB();
            d.UpdateCarUser(Username, Password, FirstName, LastName, Email, Phone, IsAdmin, Id);
        }
        public List<CarUser> showAllCarUsers()
        {
            CarUserDB d = new CarUserDB();
            return d.showAllCarUsers();
        }
        public CarUser showCarUser(int Id)
        {
            CarUserDB d = new CarUserDB();
            return d.showCarUser(Id);
        }

        // Branch
        public bool isExistBranch(int Id)
        {
            BranchDB d = new BranchDB();
            return d.isExistBranch(Id);
        }
        public void AddBranch(string Name, string City, string Address, string Phone)
        {
            BranchDB d = new BranchDB();
            d.AddBranch(Name, City, Address, Phone);
        }
        public void DeleteBranch(int Id)
        {
            BranchDB d = new BranchDB();
            d.DeleteBranch(Id);
        }
        public void UpdateBranch(string Name, string City, string Address, string Phone, int Id)
        {
            BranchDB d = new BranchDB();
            d.UpdateBranch(Name, City, Address, Phone, Id);
        }
        public List<Branch> showAllBranches()
        {
            BranchDB d = new BranchDB();
            return d.showAllBranches();
        }
        public Branch showBranch(int Id)
        {
            BranchDB d = new BranchDB();
            return d.showBranch(Id);
        }

        // Car
        public bool isExistCar(int Id)
        {
            CarDB d = new CarDB();
            return d.isExistCar(Id);
        }
        public void AddCar(string Manufacturer, string Model, int Year, string Category, decimal Price, int Stock, string ImageUrl, string Description, int BranchId, int Seats, string FuelType, int RangeKm, string Transmission, int LuggageCapacity)
        {
            CarDB d = new CarDB();
            d.AddCar(Manufacturer, Model, Year, Category, Price, Stock, ImageUrl, Description, BranchId, Seats, FuelType, RangeKm, Transmission, LuggageCapacity);
        }
        public void DeleteCar(int Id)
        {
            CarDB d = new CarDB();
            d.DeleteCar(Id);
        }
        public void UpdateCar(string Manufacturer, string Model, int Year, string Category, decimal Price, int Stock, string ImageUrl, string Description, int BranchId, int Seats, string FuelType, int RangeKm, string Transmission, int LuggageCapacity, int Id)
        {
            CarDB d = new CarDB();
            d.UpdateCar(Manufacturer, Model, Year, Category, Price, Stock, ImageUrl, Description, BranchId, Seats, FuelType, RangeKm, Transmission, LuggageCapacity, Id);
        }
        public List<Car> showAllCars()
        {
            CarDB d = new CarDB();
            return d.showAllCars();
        }
        public Car showCar(int Id)
        {
            CarDB d = new CarDB();
            return d.showCar(Id);
        }
        public List<Car> showCarsByBranchId(int BranchId)
        {
            CarDB d = new CarDB();
            return d.showCarsByBranchId(BranchId);
        }

        // CarOrder
        public bool isExistCarOrder(int Id)
        {
            CarOrderDB d = new CarOrderDB();
            return d.isExistCarOrder(Id);
        }
        public void AddCarOrder(int UserId, int CarId, DateTime OrderDate, DateTime StartDate, DateTime EndDate, decimal TotalPrice, string Status)
        {
            CarOrderDB d = new CarOrderDB();
            d.AddCarOrder(UserId, CarId, OrderDate, StartDate, EndDate, TotalPrice, Status);
        }
        public void DeleteCarOrder(int Id)
        {
            CarOrderDB d = new CarOrderDB();
            d.DeleteCarOrder(Id);
        }
        public void UpdateCarOrder(int UserId, int CarId, DateTime OrderDate, DateTime StartDate, DateTime EndDate, decimal TotalPrice, string Status, int Id)
        {
            CarOrderDB d = new CarOrderDB();
            d.UpdateCarOrder(UserId, CarId, OrderDate, StartDate, EndDate, TotalPrice, Status, Id);
        }
        public List<CarOrder> showAllCarOrders()
        {
            CarOrderDB d = new CarOrderDB();
            return d.showAllCarOrders();
        }
        public CarOrder showCarOrder(int Id)
        {
            CarOrderDB d = new CarOrderDB();
            return d.showCarOrder(Id);
        }
        public List<CarOrder> showCarOrdersByUserId(int UserId)
        {
            CarOrderDB d = new CarOrderDB();
            return d.showCarOrdersByUserId(UserId);
        }

        // Favorite
        public bool isExistFavorite(int Id)
        {
            FavoriteDB d = new FavoriteDB();
            return d.isExistFavorite(Id);
        }
        public void AddFavorite(int UserId, int CarId, DateTime AddedDate)
        {
            FavoriteDB d = new FavoriteDB();
            d.AddFavorite(UserId, CarId, AddedDate);
        }
        public void DeleteFavorite(int Id)
        {
            FavoriteDB d = new FavoriteDB();
            d.DeleteFavorite(Id);
        }
        public void UpdateFavorite(int UserId, int CarId, DateTime AddedDate, int Id)
        {
            FavoriteDB d = new FavoriteDB();
            d.UpdateFavorite(UserId, CarId, AddedDate, Id);
        }
        public List<Favorite> showAllFavorites()
        {
            FavoriteDB d = new FavoriteDB();
            return d.showAllFavorites();
        }
        public Favorite showFavorite(int Id)
        {
            FavoriteDB d = new FavoriteDB();
            return d.showFavorite(Id);
        }
        public List<Favorite> showFavoritesByUserId(int UserId)
        {
            FavoriteDB d = new FavoriteDB();
            return d.showFavoritesByUserId(UserId);
        }

        // CarReview
        public bool isExistCarReview(int Id)
        {
            CarReviewDB d = new CarReviewDB();
            return d.isExistCarReview(Id);
        }
        public void AddCarReview(int UserId, int CarId, int OrderId, int Rating, string Comment, DateTime ReviewDate)
        {
            CarReviewDB d = new CarReviewDB();
            d.AddCarReview(UserId, CarId, OrderId, Rating, Comment, ReviewDate);
        }
        public void DeleteCarReview(int Id)
        {
            CarReviewDB d = new CarReviewDB();
            d.DeleteCarReview(Id);
        }
        public void UpdateCarReview(int UserId, int CarId, int OrderId, int Rating, string Comment, DateTime ReviewDate, int Id)
        {
            CarReviewDB d = new CarReviewDB();
            d.UpdateCarReview(UserId, CarId, OrderId, Rating, Comment, ReviewDate, Id);
        }
        public List<CarReview> showAllCarReviews()
        {
            CarReviewDB d = new CarReviewDB();
            return d.showAllCarReviews();
        }
        public CarReview showCarReview(int Id)
        {
            CarReviewDB d = new CarReviewDB();
            return d.showCarReview(Id);
        }
        public List<CarReview> showCarReviewsByCarId(int CarId)
        {
            CarReviewDB d = new CarReviewDB();
            return d.showCarReviewsByCarId(CarId);
        }
    }
}

