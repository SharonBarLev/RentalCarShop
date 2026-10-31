<%@ Page Title="ByteForge Shop" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="ShopUser.aspx.cs" Inherits="ShopUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid py-4">
        <div class="row">
            
            <div class="col-md-3">
                <div class="filter-sidebar p-3 rounded" style="background: #23232b; border: 1px solid #00aaff;">
                    <h4 class="gradient-text fw-bold mb-4">Filters</h4>
                    
                    <div class="mb-4">
                        <div id="headingPrice" class="d-flex justify-content-between align-items-center mb-2 collapsed" style="cursor: pointer;" data-bs-toggle="collapse" data-bs-target="#priceCollapse">
                            <h5 class="text-white mb-0">Shop By Price</h5>
                            <i class="bi bi-chevron-down text-white small"></i>
                        </div>
                        <div id="priceCollapse" class="collapse mt-3">
                            <asp:CheckBoxList ID="cblPriceFilter" runat="server" 
                                 CssClass="text-white w-100 filter-checkbox-list" 
                                 AutoPostBack="true" 
                                 OnSelectedIndexChanged="ApplyAllFilters">
                                <asp:ListItem Text="Under 50₪" Value="under50" />
                                <asp:ListItem Text="50₪ - 100₪" Value="51-100" />
                                <asp:ListItem Text="100₪ - 200₪" Value="101-200" />
                                <asp:ListItem Text="200₪ - 500₪" Value="201-500" />
                                <asp:ListItem Text="Over 500₪" Value="above500" />
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <hr class="border-secondary" />

                    <div class="mb-4">
                        <div id="headingCategory" class="d-flex justify-content-between align-items-center mb-2 collapsed" style="cursor: pointer;" data-bs-toggle="collapse" data-bs-target="#categoryCollapse">
                            <h5 class="text-white mb-0">Categories</h5>
                            <i class="bi bi-chevron-down text-white small"></i>
                        </div>
                        <div id="categoryCollapse" class="collapse mt-3">
                            <asp:CheckBoxList ID="cblCategories" runat="server" 
                                 CssClass="text-white w-100 filter-checkbox-list" 
                                 AutoPostBack="true" 
                                 OnSelectedIndexChanged="ApplyAllFilters">
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <hr class="border-secondary" />

                    <div class="mb-4">
                        <div id="headingBrand" class="d-flex justify-content-between align-items-center mb-2 collapsed" style="cursor: pointer;" data-bs-toggle="collapse" data-bs-target="#brandCollapse">
                            <h5 class="text-white mb-0">Brands</h5>
                            <i class="bi bi-chevron-down text-white small"></i>
                        </div>
                        <div id="brandCollapse" class="collapse mt-3">
                            <asp:CheckBoxList ID="cblBrands" runat="server" 
                                 CssClass="text-white w-100 filter-checkbox-list" 
                                 AutoPostBack="true" 
                                 OnSelectedIndexChanged="ApplyAllFilters">
                            </asp:CheckBoxList>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-md-9">
                
                <div class="row mb-4">
                    <div class="col-md-4 ms-auto"> 
                        <div class="input-group h-100 rounded bg-dark" style="border: none;">
                            <span class="input-group-text bg-dark text-secondary border-0 pe-1" style="font-size: 0.9rem; user-select: none;">Sort By:</span>
                            <asp:DropDownList ID="ddlSort" runat="server" 
                                 CssClass="form-select bg-dark text-white h-100 custom-sort-ddl ps-1" 
                                 AutoPostBack="true" 
                                 OnSelectedIndexChanged="ApplyAllFilters">
                                <asp:ListItem Text="Default" Value="default" />
                                <asp:ListItem Text="Price: Low-High" Value="low-high" />
                                <asp:ListItem Text="Price: High-Low" Value="high-low" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div id="divNoProducts" runat="server" class="text-center py-5 my-5" visible="false">
                    <i class="bi bi-search text-secondary mb-3" style="font-size: 3.5rem;"></i>
                    <h2 class="fw-bold text-white mb-2">Sorry, we don't have that</h2>
                    <p class="text-muted fs-5">Try checking your spelling or adjusting your filters to find what you're looking for.</p>
                </div>

                <div class="row g-4">
                    <asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
                        <ItemTemplate>
                            <div class="col-lg-4 col-md-6">
                                <div class="product-card h-100 p-3 rounded d-flex flex-column" style="background: #23232b; border: 1px solid rgba(0, 170, 255, 0.2); transition: 0.3s;">
                                    
                                    <a href='<%# "ProductDetailsUser.aspx?id=" + Eval("MyProductID") %>' class="text-decoration-none item-link">
                                        <div class="text-center mb-3">
                                            <img src='<%# GetImageUrl(Eval("MyPicture")) %>' class="img-fluid rounded" style="max-height: 200px; width: 100%; object-fit: contain;" alt='<%# Eval("MyProductName") %>' />
                                        </div>
                                        
                                        <h5 class="text-white fw-bold mb-2 product-title-hover"><%# Eval("MyProductName") %></h5>
                                    </a>
                                    
                                    <p class="text-secondary small flex-grow-1"><%# Eval("MyDescription") %></p>
                                    
                                    <div class="d-flex justify-content-between align-items-center mt-3">
                                        <span class="gradient-text fw-bold fs-5">₪<%# Eval("MyPrice") %></span>
                                        
                                        <asp:LinkButton ID="btnAddToCart" runat="server" 
                                            CommandName="AddToCart" 
                                            CommandArgument='<%# Eval("MyProductID") %>' 
                                            CssClass="btn btn-sm btn-gradient py-2 px-3 fw-semibold"
                                            Visible='<%# Convert.ToInt32(Eval("MyStockQuantity")) > 0 %>'>
                                            <i class="bi bi-cart-plus me-1"></i>Add to Cart
                                        </asp:LinkButton>

                                        <asp:Label ID="lblOutOfStock" runat="server" 
                                            CssClass="badge bg-secondary text-wrap py-2 px-3 fw-semibold out-of-stock-badge"
                                            Visible='<%# Convert.ToInt32(Eval("MyStockQuantity")) <= 0 %>'
                                            Text="Out of Stock">
                                        </asp:Label>
                                    </div>

                                </div>
                            </div>
                        </ItemTemplate>
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
            font-weight: 500;
            transition: opacity 0.2s ease, transform 0.1s ease;
        }
        .btn-gradient:hover { 
            opacity: 0.9; 
            color: white !important; 
            transform: translateY(-1px);
        }
        .filter-checkbox-list label { margin-left: 10px; margin-bottom: 8px; cursor: pointer; font-size: 0.95rem; }
        .filter-checkbox-list input { cursor: pointer; background-color: #1a1a2e; border-color: #00aaff; }
        .product-card:hover { 
            transform: translateY(-5px); 
            border-color: #9900ff !important; 
            box-shadow: 0 10px 20px rgba(0, 170, 255, 0.15); 
        }
        .custom-sort-ddl { border: none !important; outline: none !important; box-shadow: none !important; cursor: pointer; }
        .custom-sort-ddl:focus { border: none !important; box-shadow: none !important; background-color: #23232b; }
        .input-group-text { border-radius: 0.375rem 0 0 0.375rem; }
        .custom-sort-ddl { border-radius: 0 0.375rem 0.375rem 0; }
        .product-title-hover { transition: color 0.2s ease; }
        .item-link:hover .product-title-hover { color: #00aaff !important; }
        
        .out-of-stock-badge {
            background-color: #343a40 !important;
            color: #a8a8b3 !important;
            border: 1px solid rgba(255, 255, 255, 0.1);
            font-size: 0.85rem;
            letter-spacing: 0.5px;
            cursor: not-allowed;
            user-select: none;
        }
    </style>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const collapses = ['priceCollapse', 'categoryCollapse', 'brandCollapse'];
            collapses.forEach(id => {
                const state = localStorage.getItem(id);
                const element = document.getElementById(id);
                const header = document.querySelector(`[data-bs-target="#${id}"]`);
                if (state === "shown" && element) {
                    element.classList.add("show");
                    if (header) header.classList.remove("collapsed");
                }
            });
            collapses.forEach(id => {
                const element = document.getElementById(id);
                if (element) {
                    element.addEventListener('shown.bs.collapse', function () { localStorage.setItem(id, "shown"); });
                    element.addEventListener('hidden.bs.collapse', function () { localStorage.setItem(id, "hidden"); });
                }
            });
        });
    </script>
</asp:Content>