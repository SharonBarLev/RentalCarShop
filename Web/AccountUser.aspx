<%@ Page Title="ByteForge - My Account" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="AccountUser.aspx.cs" Inherits="AccountUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .account-card {
            background: #23232b;
            border: 1px solid #7c3aed;
            box-shadow: 0 4px 20px rgba(124, 58, 237, 0.15);
            border-radius: 0.75rem;
        }
        .section-title {
            color: #00aaff;
            font-weight: 700;
            border-bottom: 2px solid #3f3f46;
            padding-bottom: 0.5rem;
        }
        .form-control-dark {
            background-color: #18181b !important;
            border: 1px solid #3f3f46 !important;
            color: #ffffff !important;
            transition: border-color 0.2s ease, box-shadow 0.2s ease;
        }
        .form-control-dark:focus {
            border-color: #00aaff !important;
            box-shadow: 0 0 8px rgba(0, 170, 255, 0.25) !important;
        }
        .gradient-btn-forge {
            background: linear-gradient(135deg, #7c3aed 0%, #00aaff 100%);
            border: none;
            color: white;
            font-weight: 600;
            transition: opacity 0.2s;
        }
        .gradient-btn-forge:hover {
            opacity: 0.9;
            color: white;
        }
        .table-dark-custom {
            background-color: #18181b;
            color: #ffffff;
        }
        .table-dark-custom th {
            background-color: #2b2b36;
            color: #00aaff;
            border-color: #3f3f46;
        }
        .table-dark-custom td {
            border-color: #3f3f46;
            vertical-align: middle;
        }
        
        .welcome-text {
            color: #a1a1aa; 
        }
        .welcome-username {
            color: #ffffff !important; 
        }

        .custom-back-btn {
            background-color: #18181b !important;
            color: #ffffff !important;
            border: 0.8px solid rgb(9, 153, 244) !important;
            font-weight: 600 !important;
            transition: all 0.2s ease-in-out !important;
        }
        .custom-back-btn:hover {
            background-color: rgb(9, 153, 244) !important;
            color: #ffffff !important;
            box-shadow: 0 0 8px rgba(9, 153, 244, 0.4) !important;
        }
    </style>

    <div class="container py-5">
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="text-white fw-bold"><i class="fas fa-user-cog me-2 text-primary"></i>My Account Settings</h2>
                <h5 class="mt-2">
                    <span class="welcome-text">Hello </span>
                    <asp:Label ID="lblWelcomeUser" runat="server" CssClass="welcome-username fw-bold"></asp:Label>
                </h5>
            </div>
        </div>

        <asp:Panel ID="pnlAlert" runat="server" Visible="false" CssClass="alert alert-dismissible fade show" role="alert">
            <asp:Label ID="lblAlertMessage" runat="server"></asp:Label>
            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="alert" aria-label="Close"></button>
        </asp:Panel>

        <div class="row g-4">
            <div class="col-lg-5">
                <div class="account-card p-4 mb-4">
                    <h4 class="section-title mb-4"><i class="fas fa-id-card me-2"></i>Update Personal Details</h4>
                    
                    <div class="mb-3">
                        <label class="form-label text-white-50">Username (Cannot be changed)</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-dark" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label text-white">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-dark" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label text-white">Phone Number</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control form-control-dark"></asp:TextBox>
                    </div>
                    <div class="mb-4">
                        <label class="form-label text-warning"><i class="fas fa-lock me-1"></i>Enter current password to save changes</label>
                        <asp:TextBox ID="txtConfirmPassForDetails" runat="server" CssClass="form-control form-control-dark" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnUpdateDetails" runat="server" Text="Save Changes" CssClass="btn gradient-btn-forge w-100 py-2" OnClick="btnUpdateDetails_Click" />
                </div>

                <div class="account-card p-4">
                    <h4 class="section-title mb-4"><i class="fas fa-key me-2"></i>Change Password</h4>
                    
                    <div class="mb-3">
                        <label class="form-label text-white">Current Password</label>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control form-control-dark" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label text-white">New Password</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control form-control-dark" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="mb-4">
                        <label class="form-label text-white">Confirm New Password</label>
                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="form-control form-control-dark" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnChangePassword" runat="server" Text="Update Password" CssClass="btn btn-outline-info w-100 py-2 fw-bold" OnClick="btnChangePassword_Click" />
                </div>
            </div>

            <div class="col-lg-7">
                <div class="account-card p-4 h-100">
                    <asp:MultiView ID="mvOrders" runat="server" ActiveViewIndex="0">
                        
                        <asp:View ID="viewOrderList" runat="server">
                            <h4 class="section-title mb-4"><i class="fas fa-shopping-bag me-2"></i>My Order History</h4>
                            
                            <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-dark-custom table-hover align-middle rounded text-center">
                                            <thead>
                                                <tr>
                                                    <th>Date</th>
                                                    <th>Total Price</th>
                                                    <th>Status</th>
                                                    <th>Actions</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                            <tr>
                                                <td><%# Convert.ToDateTime(Eval("MyOrderDate")).ToString("dd/MM/yyyy") %></td>
                                                <td class="text-success fw-bold"><%# Eval("MyTotalPrice") %> ₪</td>
                                                <td>
                                                    <%# Convert.ToBoolean(Eval("MyDelivered")) ? 
                                                        "<span class='badge bg-success'><i class='fas fa-check-circle me-1'></i>Delivered</span>" : 
                                                        "<span class='badge bg-warning text-dark'><i class='fas fa-truck me-1'></i>In Transit</span>" %>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDetails" runat="server" CssClass="btn btn-sm btn-outline-primary px-3" 
                                                        CommandName="ViewDetails" CommandArgument='<%# Eval("MyOrderID") %>'>
                                                        <i class="fas fa-search-plus me-1"></i>Details
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                            </tbody>
                                        </table>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            
                            <asp:Panel ID="pnlNoOrders" runat="server" Visible="false" CssClass="text-center py-5">
                                <i class="fas fa-box-open fa-3x text-muted mb-3"></i>
                                <h5 class="text-white-50">You haven't placed any orders yet.</h5>
                            </asp:Panel>
                        </asp:View>

                        <asp:View ID="viewOrderDetails" runat="server">
                            <div class="d-flex justify-content-between align-items-center mb-4 border-bottom pb-2">
                                <h4 class="text-white mb-0"><i class="fas fa-receipt text-info me-2"></i>Order Invoice <asp:Label ID="lblDetailOrderID" runat="server" ForeColor="#00aaff"></asp:Label></h4>
                                <asp:Button ID="btnBackToOrders" runat="server" Text="Back to History" CssClass="btn btn-sm custom-back-btn px-3" OnClick="btnBackToOrders_Click" />
                            </div>

                            <div class="p-3 mb-4 rounded" style="background-color: #18181b; border-left: 4px solid #00aaff;">
                                <p class="text-white mb-1"><strong>Shipping Address:</strong> <asp:Label ID="lblDetailAddress" runat="server"></asp:Label></p>
                                <p class="text-white mb-0"><strong>Order Notes:</strong> <asp:Label ID="lblDetailNotes" runat="server" CssClass="text-muted"></asp:Label></p>
                            </div>

                            <asp:Repeater ID="rptOrderItems" runat="server">
                                <HeaderTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-dark-custom text-center">
                                            <thead>
                                                <tr>
                                                    <th>Product Name</th>
                                                    <th>Quantity</th>
                                                    <th>Unit Price</th>
                                                    <th>Subtotal</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                            <tr>
                                                <td class="text-start ps-3 fw-bold text-white"><%# Eval("MyProductName") %></td>
                                                <td class="fw-bold text-info"><%# Eval("MyQuantity") %></td>
                                                <td><%# Eval("MyUnitPrice") %> ₪</td>
                                                <td class="text-success fw-bold"><%# Convert.ToDecimal(Eval("MyQuantity")) * Convert.ToDecimal(Eval("MyUnitPrice")) %> ₪</td>
                                            </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                            </tbody>
                                        </table>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                            
                            <div class="text-end mt-3 pe-2">
                                <h5 class="text-white">Total Amount Paid: <span class="text-success fw-bold" style="font-size: 1.4rem;"><asp:Label ID="lblDetailTotalSum" runat="server"></asp:Label> ₪</span></h5>
                            </div>
                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>