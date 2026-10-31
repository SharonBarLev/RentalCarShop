using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;

public partial class CartUser : System.Web.UI.Page
{
    private readonly ServiceReference1.Service1Client _ws = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

    public Payment.VisaWebService payment = new Payment.VisaWebService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("SignInVisitor.aspx");
            return;
        }

        if (!IsPostBack)
        {
            RefreshCartView();
        }
    }

    private string GetCurrentUsername()
    {
        return Session["Username"] != null ? Session["Username"].ToString() : "guest";
    }

    private void RefreshCartView()
    {
        string username = GetCurrentUsername();
        var cartItems = _ws.ListCartWithProductDetails(username);

        if (cartItems != null && cartItems.Length > 0)
        {
            divEmptyCart.Visible = false;
            divCartContent.Visible = true;

            rptCart.DataSource = cartItems;
            rptCart.DataBind();

            lblTotalItems.Text = _ws.CountItemsInCart(username).ToString();

            decimal totalCartPrice = 0;
            List<string> outOfStockProducts = new List<string>();

            foreach (var item in cartItems)
            {
                decimal priceToUse = item.MyPrice;

                if (priceToUse == -1)
                {
                    var realProduct = GetProductById(item.MyProductID);
                    if (realProduct != null)
                        priceToUse = Convert.ToDecimal(realProduct.Price);
                }

                totalCartPrice += priceToUse * item.MyQuantity;

                var product = GetProductById(item.MyProductID);
                if (product != null)
                {
                    int availableStock = Convert.ToInt32(product.MyStockQuantity);

                    if (item.MyQuantity > availableStock)
                    {
                        outOfStockProducts.Add(string.Format("<li>{0}</li>", product.MyProductName));
                    }
                }
            }

            lblTotalPrice.Text = totalCartPrice.ToString("N2");

            if (outOfStockProducts.Count > 0)
            {
                pnlStockError.Visible = true;
                litMissingProducts.Text = string.Join("", outOfStockProducts);
                btnCheckout.Enabled = false;
                btnCheckout.CssClass = "btn w-100 btn-secondary py-3 fs-5 mt-2 shadow disabled";
            }
            else
            {
                pnlStockError.Visible = false;
                litMissingProducts.Text = "";
                btnCheckout.Enabled = true;
                btnCheckout.CssClass = "btn w-100 btn-gradient py-3 fs-5 mt-2 shadow";
            }
        }
        else
        {
            divEmptyCart.Visible = true;
            divCartContent.Visible = false;
            rptCart.DataSource = null;
            rptCart.DataBind();

            lblTotalItems.Text = "0";
            lblTotalPrice.Text = "0.00";
            pnlStockError.Visible = false;
        }
    }

    protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string username = GetCurrentUsername();

        @System.Web.UI.WebControls.HiddenField hfProductID = (HiddenField)e.Item.FindControl("hfProductID");
        if (hfProductID == null) return;

        int productIDInt = Convert.ToInt32(hfProductID.Value);
        int currentQuantity = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Increase")
        {
            _ws.UpdateCartQuantity(username, productIDInt, currentQuantity + 1);
        }
        else if (e.CommandName == "Decrease")
        {
            if (currentQuantity > 1)
                _ws.UpdateCartQuantity(username, productIDInt, currentQuantity - 1);
            else
                _ws.DeletePruductFromCart(productIDInt, username);
        }
        else if (e.CommandName == "RemoveItem")
        {
            _ws.DeletePruductFromCart(productIDInt, username);
        }

        RefreshCartView();
    }

    protected void btnClearCart_Click(object sender, EventArgs e)
    {
        string username = GetCurrentUsername();
        _ws.DeleteCart(username);
        RefreshCartView();
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        string username = GetCurrentUsername();
        var cartItems = _ws.ListCartWithProductDetails(username);

        if (cartItems != null && cartItems.Length > 0)
        {
            decimal totalPrice = 0;
            bool hasStockIssue = false;

            foreach (var item in cartItems)
            {
                var product = GetProductById(item.MyProductID);
                if (product != null && item.MyQuantity > Convert.ToInt32(product.MyStockQuantity))
                {
                    hasStockIssue = true;
                    break;
                }

                decimal priceToUse = item.MyPrice;

                if (priceToUse == -1)
                {
                    var realProduct = GetProductById(item.MyProductID);
                    if (realProduct != null)
                        priceToUse = Convert.ToDecimal(realProduct.Price);
                }

                totalPrice += priceToUse * item.MyQuantity;
            }

            if (hasStockIssue)
            {
                RefreshCartView();
                return;
            }

            Session["TotalOrderPrice"] = Convert.ToDouble(totalPrice);
            Response.Redirect("CheckoutUser.aspx");
        }
        else
        {
            RefreshCartView();
        }
    }

    private dynamic GetProductById(object productIDObj)
    {
        if (productIDObj == null) return null;

        int productID = Convert.ToInt32(productIDObj);
        var allProducts = _ws.showAllProduct();

        if (allProducts != null)
        {
            return allProducts.FirstOrDefault(p => p.MyProductID == productID);
        }

        return null;
    }

    public string GetProductName(object productIDObj)
    {
        var product = GetProductById(productIDObj);
        return product != null ? product.MyProductName : "Unknown Product";
    }

    public string GetCartProductImageUrl(object productIDObj)
    {
        var product = GetProductById(productIDObj);

        if (product == null || product.MyPicture == null ||
            string.IsNullOrEmpty(product.MyPicture.ToString()))
        {
            return "img/no-image.png";
        }

        string base64String = product.MyPicture.ToString();

        if (base64String.StartsWith("data:image"))
            return base64String;

        return "data:image/png;base64," + base64String;
    }
}