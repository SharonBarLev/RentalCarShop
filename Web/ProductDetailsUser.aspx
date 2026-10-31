<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="ProductDetailsUser.aspx.cs" Inherits="ProductDetailsUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container py-5">
        
        <div class="row g-5 p-4 rounded shadow-lg mb-5" style="background: #23232b; border: 1px solid rgba(0, 170, 255, 0.15);">
            <div class="col-md-6 d-flex flex-column">
                <div class="text-center mb-3">
                    <a href="ShopUser.aspx" class="btn btn-gradient px-4 py-2 fw-semibold rounded-pill shadow-sm">
                        <i class="bi bi-arrow-left me-2"></i> Back to Shop
                    </a>
                </div>
                <div class="text-center d-flex align-items-center justify-content-center flex-grow-1" style="background: #1e1e24; border-radius: 8px; min-height: 380px; padding: 20px;">
                    <asp:Image ID="imgProduct" runat="server" CssClass="img-fluid rounded" style="max-height: 380px; width: 100%; object-fit: contain;" />
                </div>
            </div>

            <div class="col-md-6 d-flex flex-column justify-content-between">
                <div class="pt-3">
                    <h1 id="lblProductName" runat="server" class="text-white fw-bold mb-2 mt-0"></h1>
                    <div class="d-flex gap-2 mb-3">
                        <span class="badge bg-dark text-info border border-info px-3 py-2">
                            Category: <asp:Literal ID="litCategory" runat="server"></asp:Literal>
                        </span>
                        <span class="badge bg-dark text-purple border border-purple px-3 py-2">
                            Brand: <asp:Literal ID="litBrand" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <hr class="border-secondary my-3" />
                    <h4 class="text-white small text-uppercase fw-bold mb-2">Description</h4>
                    <p id="lblDescription" runat="server" class="gradient-text fs-5 lh-base mb-4 fw-medium" style="display: block; width: 100%;"></p>
                </div>
                <div>
                    <div class="d-flex align-items-center justify-content-between bg-dark p-3 rounded mb-4" style="border: 1px solid rgba(255,255,255,0.05);">
                        <span class="text-white fs-5">Price:</span>
                        <span id="lblPrice" runat="server" class="text-white fw-bold fs-2"></span>
                    </div>
                    <asp:LinkButton ID="btnAddToCart" runat="server" OnClick="btnAddToCart_Click" CssClass="btn btn-gradient w-100 py-3 fw-bold fs-5 rounded shadow">
                        <i class="bi bi-cart-plus-fill me-2"></i> Add to Shopping Cart
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div class="row g-4">
            
            <div class="col-lg-5">
                <div class="p-4 rounded shadow-lg h-100" style="background: #23232b; border: 1px solid rgba(0, 170, 255, 0.15);">
                    <h3 id="lblFormTitle" runat="server" class="text-white fw-bold mb-3">Add a Review</h3>
                    <p id="lblFormSubtitle" runat="server" class="text-muted small mb-4">Share your thoughts about this product with other users.</p>
                    
                    <div class="mb-3">
                        <label class="text-white-50 small fw-semibold mb-2 d-block">Your Rating</label>
                        <asp:DropDownList ID="ddlRating" runat="server" CssClass="form-select bg-dark text-white border-secondary">
                            <asp:ListItem Value="5">⭐⭐⭐⭐⭐ (5/5)</asp:ListItem>
                            <asp:ListItem Value="4">⭐⭐⭐⭐ (4/5)</asp:ListItem>
                            <asp:ListItem Value="3">⭐⭐⭐ (3/5)</asp:ListItem>
                            <asp:ListItem Value="2">⭐⭐ (2/5)</asp:ListItem>
                            <asp:ListItem Value="1">⭐ (1/5)</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="mb-4">
                        <label class="text-white-50 small fw-semibold mb-2 d-block">Review Content</label>
                        <asp:TextBox ID="txtReviewContent" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control bg-dark text-white border-secondary" placeholder="Write your review here..."></asp:TextBox>
                    </div>

                    <asp:Button ID="btnSubmitReview" runat="server" OnClick="btnSubmitReview_Click" Text="Submit Review" CssClass="btn btn-gradient w-100 py-2 fw-bold" />
                    <asp:Button ID="btnCancelEdit" runat="server" OnClick="btnCancelEdit_Click" Text="Cancel Editing" CssClass="btn btn-outline-secondary w-100 mt-2 py-2 small" Visible="false" />
                </div>
            </div>

            <div class="col-lg-7">
                <div class="p-4 rounded shadow-lg h-100" style="background: #23232b; border: 1px solid rgba(0, 170, 255, 0.15); max-height: 550px; overflow-y: auto;">
                    <h3 class="text-white fw-bold mb-4">Product Reviews</h3>
                    
                    <asp:Repeater ID="rptReviews" runat="server" OnItemDataBound="rptReviews_ItemDataBound" OnItemCommand="rptReviews_ItemCommand">
                        <ItemTemplate>
                            <div class="p-3 mb-3 rounded position-relative" style="background: #1e1e24; border-left: 4px solid #00aaff;">
                                <div class="d-flex justify-content-between align-items-start mb-2">
                                    <div>
                                        <span class="text-info fw-bold me-2"><%# Eval("MyUsername") %></span>
                                        <span class="text-warning small">
                                            <%# new string('⭐', Convert.ToInt32(Eval("MyRating"))) %>
                                        </span>
                                    </div>
                                    
                                    <div class="btn-group btn-group-sm">
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditReview" CommandArgument='<%# Eval("MyReviewID") %>' CssClass="btn btn-outline-info border-0 py-0 px-2" Visible="false">
                                            <i class="bi bi-pencil-square"></i> Edit
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteReview" CommandArgument='<%# Eval("MyReviewID") %>' CssClass="btn btn-outline-danger border-0 py-0 px-2" OnClientClick="return confirm('Are you sure you want to delete this review?');" Visible="false">
                                            <i class="bi bi-trash-fill"></i> Delete
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <p class="text-white-50 m-0 text-break font-monospace small"><%# Eval("MyRev") %></p>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblNoReviews" runat="server" Text="No reviews yet for this product. Be the first to write one!" CssClass="text-muted d-block text-center py-4" Visible='<%# rptReviews.Items.Count == 0 %>'></asp:Label>
                        </FooterTemplate>
                    </asp:Repeater>
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
        }
        .btn-gradient {
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            color: white !important;
            border: none;
            transition: opacity 0.2s ease, transform 0.1s ease;
        }
        .btn-gradient:hover { 
            opacity: 0.9; 
            transform: translateY(-2px);
            box-shadow: 0 4px 15px rgba(0, 170, 255, 0.3);
        }
        .border-purple { border-color: #9900ff !important; }
        .text-purple { color: #9900ff !important; }
        .form-select, .form-control { border-color: rgba(255,255,255,0.1) !important; }
        .form-select:focus, .form-control:focus { background-color: #17171c !important; box-shadow: 0 0 0 0.25rem rgba(0, 170, 255, 0.25); color: #fff; }
    </style>
</asp:Content>