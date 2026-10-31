<%@ Page Title="ByteForge - Checkout" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="CheckoutUser.aspx.cs" Inherits="CheckoutUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container my-5">
        <div class="row g-4 justify-content-center">
            
            <div class="col-lg-7">
                <div class="p-4 rounded shadow" style="background: #23232b; border: 1px solid rgba(0, 170, 255, 0.2);">
                    <h2 class="gradient-text fw-bold mb-4">Secure Checkout</h2>
                    
                    <h4 class="text-white mb-3 border-bottom border-secondary pb-2">1. Shipping Address</h4>
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="text-secondary small fw-bold mb-1">Country / Region</label>
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="txtCountry" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="text-secondary small fw-bold mb-1">City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="text-secondary small fw-bold mb-1">Street Address</label>
                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtStreet" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="text-secondary small fw-bold mb-1">Floor</label>
                            <asp:TextBox ID="txtFloor" runat="server" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFloor" runat="server" ControlToValidate="txtFloor" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="text-secondary small fw-bold mb-1">Zip Code</label>
                            <asp:TextBox ID="txtZip" runat="server" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="mb-4">
                        <label class="text-secondary small fw-bold mb-1">Additional Instructions (Optional)</label>
                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                    </div>

                    <h4 class="text-white mb-3 border-bottom border-secondary pb-2 mt-4">2. Payment Method</h4>

                    <div class="mb-3">
                        <label class="text-secondary small fw-bold mb-1">Card holder Name</label>
                        <asp:TextBox ID="txtHolderName" runat="server" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtHolderName" Display="Dynamic" ForeColor="Red" ErrorMessage="Name is required." class="small"></asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label class="text-secondary small fw-bold mb-1">Card Number</label>
                        <asp:TextBox ID="txtCardNum" runat="server" MaxLength="16" CssClass="form-control bg-dark text-white border-secondary py-2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCard" runat="server" ControlToValidate="txtCardNum" Display="Dynamic" ForeColor="Red" ErrorMessage="Card number is required." class="small"></asp:RequiredFieldValidator>
                    </div>

                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <label class="text-secondary small fw-bold mb-1">Expiration Month</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select bg-dark text-white border-secondary py-2">
                                <asp:ListItem Text="MM" Value=""></asp:ListItem>
                                <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                <asp:ListItem Text="04" Value="4"></asp:ListItem>
                                <asp:ListItem Text="05" Value="5"></asp:ListItem>
                                <asp:ListItem Text="06" Value="6"></asp:ListItem>
                                <asp:ListItem Text="07" Value="7"></asp:ListItem>
                                <asp:ListItem Text="08" Value="8"></asp:ListItem>
                                <asp:ListItem Text="09" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ControlToValidate="ddlMonth" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 mb-3">
                            <label class="text-secondary small fw-bold mb-1">Expiration Year</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select bg-dark text-white border-secondary py-2">
                                <asp:ListItem Text="YYYY" Value=""></asp:ListItem>
                                <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                                <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                                <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                                <asp:ListItem Text="2030" Value="2030"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 mb-3">
                            <label class="text-secondary small fw-bold mb-1">CVV</label>
                            <asp:TextBox ID="txtCVV" runat="server" MaxLength="3" CssClass="form-control bg-dark text-white border-secondary py-2" placeholder="123"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCVV" runat="server" ControlToValidate="txtCVV" Display="Dynamic" ForeColor="Red" ErrorMessage="Required." class="small"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger small d-block mb-3" Visible="false"></asp:Label>

                    <asp:Button ID="btnPay" runat="server" Text="Pay Now" OnClick="btnPay_Click" CssClass="btn w-100 btn-gradient py-3 fs-5 mt-2 shadow" />
                </div>
            </div>

            <div class="col-lg-5">
        <div class="p-4 rounded h-100" style="background: #1a1a1f; border: 1px solid rgba(255, 255, 255, 0.05); overflow-x: hidden;">
            <h4 class="text-white fw-bold mb-4">Order Summary</h4>
        
            <div class="row gx-0 text-secondary small fw-bold mb-2 pb-1 border-bottom border-secondary" style="opacity: 0.6;">
                <div class="col-6 text-start">Product</div>
                <div class="col-2 text-center">Quantity</div>
                <div class="col-4 text-end">Total</div>
            </div>

            <div class="mb-3" style="max-height: 300px; overflow-y: auto; overflow-x: hidden;">
                <asp:Repeater ID="rptSummaryItems" runat="server">
                    <ItemTemplate>
                        <div class="row gx-0 text-white small py-2 align-items-center border-bottom border-dark" style="--bs-border-opacity: .4;">
                            <div class="col-6 text-truncate fw-medium text-start" style="max-width: 100%;">
                                <%# DataBinder.Eval(Container.DataItem, "MyProductName") %>
                            </div>
                            <div class="col-2 text-center text-secondary">
                                <%# DataBinder.Eval(Container.DataItem, "MyQuantity") %>
                            </div>
                            <div class="col-4 text-end fw-semibold">
                                <%# "₪" + (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "MyPrice")) * Convert.ToInt32(DataBinder.Eval(Container.DataItem, "MyQuantity"))).ToString("N2") %>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="d-flex justify-content-between align-items-center mb-4 mt-3">
                <span class="text-secondary fw-medium">Total Amount:</span>
                <div class="fs-4 text-white">
                    <span class="gradient-text fw-bold">₪</span>
                    <asp:Label ID="lblOrderTotal" runat="server" Text="0.00" CssClass="gradient-text fw-bold"></asp:Label>
                </div>
            </div>
            <p class="text-muted small mb-0"><i class="bi bi-shield-lock me-1"></i> Your transaction is encrypted and secured by ByteForge Credit Services.</p>
        </div>
    </div>
            
        </div>
    </div>

    <style>
        .col-lg-5, .col-lg-5 * {
            overflow-x: hidden !important;
        }
        
        .gradient-text {
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            display: inline-block;
        }
        .btn-gradient {
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            color: white !important;
            border: none;
            font-weight: 600;
            transition: opacity 0.2s ease;
        }
        .btn-gradient:hover {
            opacity: 0.9;
        }
        .container .border-secondary {
            border-color: rgb(9, 153, 244) !important;
            border-width: 0.8px !important;
        }

    </style>
</asp:Content>