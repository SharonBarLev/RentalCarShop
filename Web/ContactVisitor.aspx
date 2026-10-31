<%@ Page Title="Contact ByteForge" Language="C#" MasterPageFile="~/MasterPageVisitor.master" AutoEventWireup="true" CodeFile="ContactVisitor.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="contact-container">
        <div class="contact-card fade-in">
            <h2 class="form-title">Contact the Forge</h2>
            
            <asp:Label ID="lblStatus" runat="server" CssClass="info-label" Visible="false"></asp:Label>

            <div class="contact-grid">
                <div class="contact-info-sidebar">
                    <h3 class="sidebar-title">Get in Touch</h3>
                    <div class="info-item">
                        <i class="fas fa-location-dot"></i>
                        <span>Nofit, Israel</span>
                    </div>
                </div>

                <div class="contact-form-side">
                    <div class="form-group">
                        <label class="form-label">Full Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Your Name" />
                    </div>

                    <div class="form-group">
                        <label class="form-label">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="example@mail.com" />
                    </div>

                    <div class="form-group">
                        <label class="form-label">Subject</label>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" Placeholder="How can we help?" />
                    </div>

                    <div class="form-group">
                        <label class="form-label">Message</label>
                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" Placeholder="Type your message here..." />
                    </div>

                    <asp:Button ID="btnSend" runat="server" Text="Send Message" CssClass="submit-button" OnClick="btnSend_Click" />
                </div>
            </div>
        </div>
    </div>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

    <style>
        .contact-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 40px 20px;
            background: transparent;
        }

        .contact-card {
            width: 100%;
            max-width: 800px;
            background: linear-gradient(135deg, #23232b 0%, #2d2d38 100%);
            padding: 40px;
            border-radius: 20px;
            box-shadow: 0 10px 40px rgba(124, 58, 237, 0.25);
            border: 1px solid rgba(168, 85, 247, 0.2);
        }

        .form-title {
            text-align: center;
            font-size: 32px;
            font-weight: 800;
            margin-bottom: 35px;
            background: linear-gradient(45deg, #a855f7, #7c3aed);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            text-transform: uppercase;
        }

        .contact-grid {
            display: grid;
            grid-template-columns: 1fr 2fr;
            gap: 40px;
        }

        @media (max-width: 600px) {
            .contact-grid { grid-template-columns: 1fr; }
            .contact-info-sidebar { border-right: none !important; border-bottom: 1px solid rgba(255,255,255,0.1); padding-bottom: 20px; }
        }

        .contact-info-sidebar {
            border-right: 1px solid rgba(255,255,255,0.1);
            padding-right: 20px;
        }

        .sidebar-title { color: #e9d5ff; font-size: 18px; margin-bottom: 20px; }

        .info-item {
            display: flex;
            align-items: center;
            gap: 12px;
            color: #94a3b8;
            margin-bottom: 15px;
            font-size: 14px;
        }

        .info-item i { color: #a855f7; font-size: 18px; width: 25px; }

        .social-links { margin-top: 30px; display: flex; gap: 15px; }
        .social-links i {
            color: #a855f7;
            font-size: 20px;
            cursor: pointer;
            transition: 0.3s;
        }
        .social-links i:hover { color: #00aaff; transform: translateY(-3px); }

        .form-group { margin-bottom: 18px; }

        .form-label {
            font-weight: 600;
            color: #e9d5ff;
            display: block;
            margin-bottom: 8px;
            font-size: 12px;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .form-control {
            width: 100%;
            padding: 12px 16px;
            border-radius: 10px;
            border: 2px solid rgba(124, 58, 237, 0.2);
            background-color: rgba(28, 28, 34, 0.6);
            color: #f1f1f5;
            font-size: 15px;
            transition: all 0.3s ease;
            font-family: inherit;
        }

        .form-control:focus {
            outline: none;
            border-color: #7c3aed;
            background-color: rgba(28, 28, 34, 0.9);
            box-shadow: 0 0 15px rgba(124, 58, 237, 0.2);
        }

        .submit-button {
            width: 100%;
            padding: 14px;
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            border: none;
            border-radius: 10px;
            color: white;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
            box-shadow: 0 4px 15px rgba(124, 58, 237, 0.3);
            transition: 0.3s;
            margin-top: 10px;
            text-transform: uppercase;
        }

        .submit-button:hover {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(124, 58, 237, 0.5);
        }

        .info-label {
            display: block; text-align: center; background: rgba(52, 211, 153, 0.1);
            color: #34d399; padding: 10px; border-radius: 8px; margin-bottom: 20px;
        }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .fade-in { animation: fadeInUp 0.6s ease-out forwards; }
    </style>
</asp:Content>