using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;

public partial class CheckoutUser : System.Web.UI.Page
{
    public Payment.VisaWebService creditWS = new Payment.VisaWebService();
    private readonly ServiceReference1.Service1Client _shopWS = new ServiceReference1.Service1Client("BasicHttpBinding_IService1");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("SignInVisitor.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadOrderTotal();
            LoadUserShippingAddress();
        }
    }

    private void LoadOrderTotal()
    {
        string username = Session["Username"].ToString();

        var cartItems = _shopWS.ListCartWithProductDetails(username);
        var allProducts = _shopWS.showAllProduct();

        if (cartItems != null && cartItems.Length > 0 && allProducts != null)
        {
            var combinedItems = from cart in cartItems
                                join prod in allProducts on cart.MyProductID equals prod.MyProductID
                                select new
                                {
                                    MyProductName = prod.MyProductName,
                                    MyQuantity = cart.MyQuantity,
                                    MyPrice = prod.MyPrice
                                };

            rptSummaryItems.DataSource = combinedItems.ToList();
            rptSummaryItems.DataBind();

            double total = cartItems.Sum(item => (double)item.MyPrice * item.MyQuantity);
            lblOrderTotal.Text = total.ToString("N2");
            Session["TotalOrderPrice"] = total;
        }
        else
        {
            Response.Redirect("CartUser.aspx");
        }
    }

    private void LoadUserShippingAddress()
    {
        string username = Session["Username"].ToString();

        var addressInfo = _shopWS.ShowShippingAddress(username);

        if (addressInfo != null)
        {
            txtCountry.Text = addressInfo.MyCountryRegion;
            txtCity.Text = addressInfo.MyCity;
            txtStreet.Text = addressInfo.MyStreetAddress;
            txtFloor.Text = addressInfo.MyFloor;
            txtZip.Text = addressInfo.MyZipCode;
            txtNotes.Text = addressInfo.MyAdditionalInstructions;

            Session["OldCountry"] = addressInfo.MyCountryRegion;
            Session["OldCity"] = addressInfo.MyCity;
            Session["OldStreet"] = addressInfo.MyStreetAddress;
            Session["OldFloor"] = addressInfo.MyFloor;
            Session["OldZip"] = addressInfo.MyZipCode;
            Session["OldNotes"] = addressInfo.MyAdditionalInstructions;
            Session["HasAddressRecord"] = true;
        }
        else
        {
            Session["OldCountry"] = "";
            Session["OldCity"] = "";
            Session["OldStreet"] = "";
            Session["OldFloor"] = "";
            Session["OldZip"] = "";
            Session["OldNotes"] = "";
            Session["HasAddressRecord"] = false;
        }
    }

    protected void btnPay_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;
        string username = Session["Username"].ToString();

        string currentCountry = txtCountry.Text.Trim();
        string currentCity = txtCity.Text.Trim();
        string currentStreet = txtStreet.Text.Trim();
        string currentFloor = txtFloor.Text.Trim();
        string currentZip = txtZip.Text.Trim();
        string currentNotes = txtNotes.Text.Trim();

        bool isAddressChanged = currentCountry != Session["OldCountry"].ToString() ||
                                currentCity != Session["OldCity"].ToString() ||
                                currentStreet != Session["OldStreet"].ToString() ||
                                currentFloor != Session["OldFloor"].ToString() ||
                                currentZip != Session["OldZip"].ToString() ||
                                currentNotes != Session["OldNotes"].ToString();

        try
        {
            if (isAddressChanged && Convert.ToBoolean(Session["HasAddressRecord"]))
            {
                var allOrders = _shopWS.showAllOrders();
                if (allOrders != null && allOrders.Length > 0)
                {
                    string currentUsername = username.Trim().ToLower();
                    foreach (var o in allOrders)
                    {
                        if (o.MyUsername != null && o.MyUsername.Trim().ToLower() == currentUsername && o.MyDelivered == false)
                        {
                            lblErrorMessage.Text = "You cannot change your shipping address while you have an active order in delivery.";
                            lblErrorMessage.Visible = true;
                            return;
                        }
                    }
                }
            }

            string holderName = txtHolderName.Text.Trim();
            string cardNum = txtCardNum.Text.Trim();
            string securityCode = txtCVV.Text.Trim();
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            int year = Convert.ToInt32(ddlYear.SelectedValue);

            if (Session["TotalOrderPrice"] == null)
            {
                Response.Redirect("CartUser.aspx");
                return;
            }

            double moneyToPay = Convert.ToDouble(Session["TotalOrderPrice"]);

            if (!creditWS.isCardExist(cardNum, holderName, securityCode, month, year) || !creditWS.isValidMoney(cardNum, moneyToPay))
            {
                lblErrorMessage.Text = "Payment declined: Credit card details are incorrect or insufficient funds.";
                lblErrorMessage.Visible = true;
                return;
            }

            var cartItems = _shopWS.ListCartWithProductDetails(username);
            if (cartItems == null || cartItems.Length == 0) return;

            if (!Convert.ToBoolean(Session["HasAddressRecord"]))
            {
                _shopWS.AddShippingAddress(username, currentCountry, currentCity, currentStreet, currentFloor, currentZip, currentNotes);
            }
            else if (isAddressChanged)
            {
                _shopWS.UpdateShippingAddress(username, currentCountry, currentCity, currentStreet, currentFloor, currentZip, currentNotes);
            }

            creditWS.UpdateTotal(cardNum, moneyToPay);

            decimal totalPriceDecimal = Convert.ToDecimal(moneyToPay);
            string formattedDateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime finalDate = Convert.ToDateTime(formattedDateStr);

            _shopWS.AddOrder(username, finalDate, totalPriceDecimal, currentCountry, currentCity, currentStreet, currentFloor, currentZip, currentNotes);

            int newOrderID = -1;
            var updatedOrders = _shopWS.showAllOrders();

            if (updatedOrders != null && updatedOrders.Length > 0)
            {
                string currentUsername = username.ToString().Trim().ToLower();
                foreach (var o in updatedOrders)
                {
                    if (o.MyUsername != null && o.MyUsername.ToString().Trim().ToLower() == currentUsername)
                    {
                        if (o.MyOrderID > newOrderID)
                        {
                            newOrderID = o.MyOrderID;
                        }
                    }
                }
            }

            if (newOrderID != -1)
            {
                foreach (var item in cartItems)
                {
                    int prodID = Convert.ToInt32(item.MyProductID);
                    int qty = Convert.ToInt32(item.MyQuantity);
                    decimal finalUnitPrice = Convert.ToDecimal(Convert.ToDouble(item.MyPrice));

                    _shopWS.AddOrderDetails(newOrderID, prodID, qty, finalUnitPrice);
                }
            }

            var allProducts = _shopWS.showAllProduct();

            _shopWS.DeleteCart(username);

            if (newOrderID != -1 && allProducts != null)
            {
                string cardLastFour = cardNum.Length >= 4 ? cardNum.Substring(cardNum.Length - 4) : cardNum;

                GenerateAndDownloadPDF(newOrderID, username, moneyToPay, cartItems, allProducts,
                    currentCountry, currentCity, currentStreet, currentFloor, currentZip, currentNotes,
                    cardLastFour);
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "An error occurred during processing: " + ex.Message;
            lblErrorMessage.Visible = true;
        }
    }

    private void GenerateAndDownloadPDF(int orderID, string username, double totalPrice,
                                        BusinessModelCarShop.Cart[] cartItems,
                                        BusinessModelCarShop.Product[] allProducts,
                                        string country, string city, string street, string floor, string zip, string notes,
                                        string cardLastFour)
    {
        var orderDetails = from cart in cartItems
                           join prod in allProducts on cart.MyProductID equals prod.MyProductID
                           select new
                           {
                               ProductName = prod.MyProductName,
                               Quantity = cart.MyQuantity,
                               UnitPrice = prod.MyPrice,
                               TotalPrice = (double)prod.MyPrice * cart.MyQuantity
                           };

        StringBuilder html = new StringBuilder();

        html.Append("<!DOCTYPE html>");
        html.Append("<html>");
        html.Append("<head>");
        html.Append("<meta charset='utf-8' />");
        html.Append("<style>");

        html.Append("@import url('https://fonts.googleapis.com/css2?family=DM+Sans:wght@400;500;600&family=DM+Mono:wght@400;500&display=swap');");
        html.Append("* { box-sizing: border-box; margin: 0; padding: 0; }");
        html.Append("body { font-family: 'DM Sans', 'Segoe UI', sans-serif; background: #ffffff; color: #1a1a1a; font-size: 13px; line-height: 1.6; }");

        html.Append(".page { max-width: 740px; margin: 0 auto; padding: 48px 52px; }");

        html.Append(".header { display: flex; justify-content: space-between; align-items: flex-start; padding-bottom: 32px; border-bottom: 2px solid #1a1a1a; margin-bottom: 36px; }");
        html.Append(".brand-name { font-size: 26px; font-weight: 600; letter-spacing: -0.5px; color: #1a1a1a; }");
        html.Append(".brand-name em { font-style: normal; color: #0062ff; }");
        html.Append(".invoice-meta { text-align: right; }");
        html.Append(".invoice-label { font-size: 11px; font-weight: 500; letter-spacing: 1.5px; text-transform: uppercase; color: #888; margin-bottom: 6px; }");
        html.Append(".invoice-number { font-family: 'DM Mono', monospace; font-size: 20px; font-weight: 500; color: #1a1a1a; }");
        html.Append(".invoice-date { font-size: 12px; color: #666; margin-top: 4px; }");

        html.Append(".status-strip { background: #f0f7ff; border: 1px solid #d0e4ff; border-radius: 6px; padding: 12px 18px; display: flex; align-items: center; gap: 10px; margin-bottom: 36px; }");
        html.Append(".status-dot { width: 8px; height: 8px; background: #0062ff; border-radius: 50%; flex-shrink: 0; }");
        html.Append(".status-text { font-size: 12px; color: #0040a8; font-weight: 500; }");

        html.Append(".info-grid { display: flex; gap: 24px; margin-bottom: 36px; }");
        html.Append(".info-block { flex: 1; background: #f8f8f8; border-radius: 8px; padding: 18px 20px; }");
        html.Append(".info-block-title { font-size: 10px; font-weight: 600; letter-spacing: 1.4px; text-transform: uppercase; color: #999; margin-bottom: 12px; }");
        html.Append(".info-row { display: flex; justify-content: space-between; padding: 4px 0; border-bottom: 1px solid #ececec; }");
        html.Append(".info-row:last-child { border-bottom: none; }");
        html.Append(".info-key { color: #666; font-size: 12px; }");
        html.Append(".info-val { color: #1a1a1a; font-size: 12px; font-weight: 500; text-align: right; max-width: 55%; }");

        html.Append(".section-title { font-size: 10px; font-weight: 600; letter-spacing: 1.4px; text-transform: uppercase; color: #999; margin-bottom: 10px; }");
        html.Append("table { width: 100%; border-collapse: collapse; }");
        html.Append("thead tr { border-bottom: 2px solid #1a1a1a; }");
        html.Append("th { padding: 8px 10px; font-size: 11px; font-weight: 600; letter-spacing: 0.5px; color: #555; text-align: left; }");
        html.Append("th.right { text-align: right; }");
        html.Append("td { padding: 11px 10px; font-size: 13px; color: #1a1a1a; border-bottom: 1px solid #efefef; vertical-align: middle; }");
        html.Append("td.right { text-align: right; }");
        html.Append("td.mono { font-family: 'DM Mono', monospace; font-size: 12px; }");
        html.Append("tbody tr:last-child td { border-bottom: none; }");

        html.Append(".total-section { margin-top: 0; border-top: 2px solid #1a1a1a; padding-top: 14px; display: flex; justify-content: flex-end; }");
        html.Append(".total-box { text-align: right; }");
        html.Append(".total-label { font-size: 11px; font-weight: 600; letter-spacing: 1px; text-transform: uppercase; color: #888; margin-bottom: 4px; }");
        html.Append(".total-amount { font-family: 'DM Mono', monospace; font-size: 26px; font-weight: 500; color: #0062ff; letter-spacing: -0.5px; }");

        html.Append(".footer { margin-top: 52px; padding-top: 20px; border-top: 1px solid #e8e8e8; display: flex; justify-content: space-between; align-items: flex-end; }");
        html.Append(".footer-brand { font-size: 12px; color: #bbb; }");
        html.Append(".footer-support { font-size: 11px; color: #aaa; text-align: right; }");
        html.Append(".footer-support a { color: #0062ff; text-decoration: none; }");

        html.Append("</style>");
        html.Append("</head>");
        html.Append("<body>");
        html.Append("<div class='page'>");

        // Header
        html.Append("<div class='header'>");
        html.Append("  <div class='brand-name'><em>Byte</em>Forge</div>");
        html.Append("  <div class='invoice-meta'>");
        html.Append("    <div class='invoice-label'>Invoice</div>");
        html.Append("    <div class='invoice-number'>#" + orderID.ToString("D6") + "</div>");
        html.Append("    <div class='invoice-date'>" + DateTime.Now.ToString("MMMM dd, yyyy") + "</div>");
        html.Append("  </div>");
        html.Append("</div>");

        // Status strip
        html.Append("<div class='status-strip'>");
        html.Append("  <div class='status-dot'></div>");
        html.Append("  <div class='status-text'>Payment confirmed &mdash; your order is being prepared for dispatch.</div>");
        html.Append("</div>");

        // Info grid
        html.Append("<div class='info-grid'>");

        // Shipping block
        html.Append("<div class='info-block'>");
        html.Append("  <div class='info-block-title'>Shipping Address</div>");
        html.Append("  <div class='info-row'><span class='info-key'>Recipient</span><span class='info-val'>" + username + "</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Country</span><span class='info-val'>" + country + "</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>City</span><span class='info-val'>" + city + "</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Street</span><span class='info-val'>" + street + (string.IsNullOrEmpty(floor) ? "" : ", Fl. " + floor) + "</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Zip Code</span><span class='info-val'>" + zip + "</span></div>");
        if (!string.IsNullOrEmpty(notes))
            html.Append("  <div class='info-row'><span class='info-key'>Notes</span><span class='info-val'>" + notes + "</span></div>");
        html.Append("</div>");

        // Payment block
        html.Append("<div class='info-block'>");
        html.Append("  <div class='info-block-title'>Payment Details</div>");
        html.Append("  <div class='info-row'><span class='info-key'>Date &amp; Time</span><span class='info-val'>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Method</span><span class='info-val'>Credit Card</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Card</span><span class='info-val' style='font-family:\"DM Mono\",monospace;'>&bull;&bull;&bull;&bull; &bull;&bull;&bull;&bull; &bull;&bull;&bull;&bull; " + cardLastFour + "</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Status</span><span class='info-val' style='color:#0062ff;font-weight:600;'>Paid</span></div>");
        html.Append("  <div class='info-row'><span class='info-key'>Order ID</span><span class='info-val' style='font-family:\"DM Mono\",monospace;'>#" + orderID.ToString("D6") + "</span></div>");
        html.Append("</div>");

        html.Append("</div>"); // end info-grid

        // Items table
        html.Append("<div class='section-title'>Order Items</div>");
        html.Append("<table>");
        html.Append("<thead><tr>");
        html.Append("  <th>Description</th>");
        html.Append("  <th class='right'>Qty</th>");
        html.Append("  <th class='right'>Unit Price</th>");
        html.Append("  <th class='right'>Line Total</th>");
        html.Append("</tr></thead>");
        html.Append("<tbody>");

        foreach (var item in orderDetails)
        {
            html.Append("<tr>");
            html.Append("  <td>" + item.ProductName + "</td>");
            html.Append("  <td class='right mono'>" + item.Quantity + "</td>");
            html.Append("  <td class='right mono'>&#8362;" + Convert.ToDouble(item.UnitPrice).ToString("N2") + "</td>");
            html.Append("  <td class='right mono'>&#8362;" + item.TotalPrice.ToString("N2") + "</td>");
            html.Append("</tr>");
        }

        html.Append("</tbody>");
        html.Append("</table>");

        // Grand total
        html.Append("<div class='total-section'>");
        html.Append("  <div class='total-box'>");
        html.Append("    <div class='total-label'>Grand Total</div>");
        html.Append("    <div class='total-amount'>&#8362;" + totalPrice.ToString("N2") + "</div>");
        html.Append("  </div>");
        html.Append("</div>");

        // Footer
        html.Append("<div class='footer'>");
        html.Append("  <div class='footer-brand'>&copy; " + DateTime.Now.Year + " ByteForge Marketplace</div>");
        html.Append("  <div class='footer-support'>Questions? <a href='https://www.byteforge.com/contact'>Contact us on our website</a></div>");
        html.Append("</div>");

        html.Append("</div>"); // end .page
        html.Append("</body></html>");

        string tempHtmlPath = Path.Combine(Path.GetTempPath(), "order_" + orderID + ".html");
        string tempPdfPath = Path.Combine(Path.GetTempPath(), "order_" + orderID + ".pdf");

        File.WriteAllText(tempHtmlPath, html.ToString(), System.Text.Encoding.UTF8);

        string wkhtmlPath = Server.MapPath("~/Rotativa/wkhtmltopdf.exe");

        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = wkhtmlPath,
            Arguments = "--page-size A4 --margin-top 10 --margin-bottom 10 --margin-left 10 --margin-right 10 \"" + tempHtmlPath + "\" \"" + tempPdfPath + "\"",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true
        };

        using (var proc = System.Diagnostics.Process.Start(psi))
        {
            proc.WaitForExit();
        }

        byte[] pdfBytes = File.ReadAllBytes(tempPdfPath);

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=ForgeByteOrder.pdf");
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.BinaryWrite(pdfBytes);
        Response.Flush();

        File.Delete(tempHtmlPath);
        File.Delete(tempPdfPath);

        Response.End();
    }
}
