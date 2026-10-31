<%@ Page Title="ByteForge Cart" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="CartUser.aspx.cs" Inherits="CartUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container my-5">
        <h2 class="gradient-text fw-bold mb-4">Your Shopping Cart</h2>

        <div id="divEmptyCart" runat="server" class="text-center py-5 my-5" visible="false">
            <i class="bi bi-cart-x text-secondary mb-3" style="font-size: 4rem;"></i>
            <h3 class="text-white fw-bold">Your cart is empty</h3>
            <p class="text-muted fs-5 mb-4">Looks like you haven't added any hardware to your forge yet.</p>
            <a href="ShopUser.aspx" class="btn btn-gradient px-4 py-2">Go Shopping</a>
        </div>

        <div id="divCartContent" runat="server" class="row g-4">
            <div class="col-lg-8">
                <div class="table-responsive p-3 rounded" style="background: #23232b; border: 1px solid rgba(0, 170, 255, 0.2);">
                    <table class="table table-dark align-middle mb-0 border-0">
                        <thead>
                            <tr class="text-secondary border-bottom border-secondary">
                                <th scope="col" style="width: 45%;">Product</th>
                                <th scope="col" class="text-center" style="width: 15%;">Price</th>
                                <th scope="col" class="text-center" style="width: 20%;">Quantity</th>
                                <th scope="col" class="text-center" style="width: 20%;">Total</th>
                                <th scope="col" style="width: 5%;"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                                <ItemTemplate>
                                    <tr class="border-bottom border-dark">
                                        <td class="py-3">
                                            <div class="d-flex align-items-center">
                                                <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("MyProductID") %>' />
                                    
                                                <div class="me-3 bg-dark d-flex align-items-center justify-content-center rounded" style="width: 80px; height: 80px; border: 1px solid rgba(255,255,255,0.1); flex-shrink: 0;">
                                                    <img src='<%# GetCartProductImageUrl(Eval("MyProductID")) %>' class="img-fluid rounded" style="max-height: 72px; max-width: 72px; object-fit: contain;" alt='Product Image' />
                                                </div>

                                                <span class="text-white fw-semibold fs-6"><%# GetProductName(Eval("MyProductID")) %></span>
                                            </div>
                                        </td>
                                        <td class="text-center text-white py-3">
                                            <span style="direction: ltr; display: inline-block;">₪<%# Convert.ToDecimal(Eval("MyPrice")).ToString("N2") %></span>
                                        </td>
                                        <td class="text-center py-3">
                                            <div class="d-flex justify-content-center align-items-center">
                                                <asp:LinkButton ID="btnMinus" runat="server" CommandName="Decrease" CommandArgument='<%# Eval("MyQuantity") %>' CssClass="btn btn-sm btn-outline-secondary py-0 px-2 text-white border-secondary">-</asp:LinkButton>
                                                <span class="mx-3 text-white fw-bold"><%# Eval("MyQuantity") %></span>
                                                <asp:LinkButton ID="btnPlus" runat="server" CommandName="Increase" CommandArgument='<%# Eval("MyQuantity") %>' CssClass="btn btn-sm btn-outline-secondary py-0 px-2 text-white border-secondary">+</asp:LinkButton>
                                            </div>
                                        </td>
                                        <td class="text-center py-3">
                                            <span class="gradient-text fw-bold text-decoration-none d-block w-100" style="direction: ltr;">
                                                ₪<%# (Convert.ToDecimal(Eval("MyPrice")) * Convert.ToInt32(Eval("MyQuantity"))).ToString("N2") %>
                                            </span>
                                        </td>
                                        <td class="text-end py-3">
                                            <asp:LinkButton ID="btnRemove" runat="server" CommandName="RemoveItem" CssClass="text-danger fs-5" OnClientClick="return confirm('Remove this item from your cart?');">
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <div class="mt-3 text-start">
                    <asp:LinkButton ID="btnClearCart" runat="server" OnClick="btnClearCart_Click" CssClass="btn btn-sm btn-outline-danger" OnClientClick="return confirm('Are you sure you want to clear your entire cart?');">
                        <i class="bi bi-trash3 me-1"></i> Clear Cart
                    </asp:LinkButton>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="p-4 rounded text-white h-100" style="background: #23232b; border: 1px solid #00aaff;">
                    <h4 class="fw-bold mb-4 border-bottom border-secondary pb-2" style="color: #ffffff !important;">Order Summary</h4>
                    
                    <div class="d-flex justify-content-between mb-3 fs-5">
                        <span class="text-secondary">Total Items:</span>
                        <asp:Label ID="lblTotalItems" runat="server" Text="0" CssClass="fw-bold" style="color: #ffffff;"></asp:Label>
                    </div>
                    
                    <div class="d-flex justify-content-between mb-4 fs-4 border-top border-dark pt-3">
                        <span class="fw-bold" style="color: #ffffff;">Total Price:</span>
                        <div style="direction: ltr; display: inline-block;">
                            <span class="gradient-text fw-bold">₪</span>
                            <asp:Label ID="lblTotalPrice" runat="server" Text="0.00" CssClass="gradient-text fw-bold"></asp:Label>
                        </div>
                    </div>

                    <asp:Panel ID="pnlStockError" runat="server" Visible="false" CssClass="alert alert-danger border-danger bg-dark text-danger my-3 p-3 rounded">
                        <h6 class="fw-bold mb-2"><i class="bi bi-exclamation-triangle-fill me-2"></i>Out of Stock Items:</h6>
                        <p class="small mb-2">We don't have enough stock for the following items:</p>
                        <ul class="mb-2 ps-3 small text-white fw-semibold">
                            <asp:Literal ID="litMissingProducts" runat="server"></asp:Literal>
                        </ul>
                        <p class="small mb-0 text-muted">Please remove or reduce them from your cart to proceed with the order.</p>
                    </asp:Panel>

                    <asp:Button ID="btnCheckout" runat="server" Text="Proceed to Checkout" OnClick="btnCheckout_Click" CssClass="btn w-100 btn-gradient py-3 fs-5 mt-2 shadow" />
                    
                    <div class="text-center mt-3">
                        <a href="ShopUser.aspx" class="text-secondary small text-decoration-none"><i class="bi bi-arrow-left me-1"></i> Continue Shopping</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .gradient-text {
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            display: inline-block;
            text-decoration: none !important;
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
            color: white !important; 
        }
        .table-dark { 
            --bs-table-bg: #23232b; 
        }
    </style>
</asp:Content>