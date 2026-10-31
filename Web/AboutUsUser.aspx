<%@ Page Title="About ByteForge" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="AboutUsUser.aspx.cs" Inherits="AboutUsUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="about-container">
        <div class="about-card fade-in">
            <h2 class="form-title">The ByteForge Standard</h2>
            
            <div class="about-content">
                <p class="about-text">
                    Established in <span class="highlight">2025 in Nofit</span>, <span class="highlight">ByteForge</span> was created to redefine the hardware experience. We serve everyone—from hardcore gamers and creative professionals to home and business users.
                </p>

                <p class="about-text sub-text">
                    We don’t just sell components; we engineer power. Every part in our catalog is hand-picked for <span class="highlight">uncompromising reliability and elite performance</span>.
                </p>

                <div class="features-list">
                    <div class="feature-item">
                        <i class="fas fa-microchip"></i>
                        <div class="feature-info">
                            <h4>Master-Level Assembly</h4>
                            <p>Premium cable management and optimized airflow are standard in every build we forge.</p>
                        </div>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-layer-group"></i>
                        <div class="feature-info">
                            <h4>The Ultimate Inventory</h4>
                            <p>From CPUs to peripherals, we stock everything you need under one roof with 100% genuine quality.</p>
                        </div>
                    </div>
                    <div class="feature-item">
                        <i class="fas fa-store-alt"></i>
                        <div class="feature-info">
                            <h4>Visit Us in Nofit</h4>
                            <p>Experience our hardware in person at our physical showroom and get expert face-to-face advice.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

    <style>
        .about-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 40px 20px;
            background: transparent;
        }

        .about-card {
            width: 100%;
            max-width: 650px;
            background: linear-gradient(135deg, #23232b 0%, #2d2d38 100%);
            padding: 45px;
            border-radius: 20px;
            box-shadow: 0 10px 40px rgba(124, 58, 237, 0.25);
            border: 1px solid rgba(168, 85, 247, 0.2);
        }

        .form-title {
            text-align: center;
            font-size: 32px;
            font-weight: 800;
            margin-bottom: 25px;
            background: linear-gradient(45deg, #a855f7, #7c3aed);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            text-transform: uppercase;
            letter-spacing: 2px;
        }

        .about-text {
            color: #f1f1f5;
            line-height: 1.6;
            font-size: 16px;
            text-align: center;
            margin-bottom: 15px;
        }

        .sub-text { color: #94a3b8; font-size: 14px; }
        .highlight { color: #c4b5fd; font-weight: bold; }

        .features-list {
            margin-top: 30px;
            background: rgba(15, 15, 20, 0.4);
            border-radius: 15px;
            padding: 25px;
            border: 1px solid rgba(124, 58, 237, 0.1);
        }

        .feature-item { display: flex; align-items: flex-start; gap: 20px; margin-bottom: 25px; }
        .feature-item:last-child { margin-bottom: 0; }
        .feature-item i { color: #a855f7; font-size: 24px; min-width: 40px; text-align: center; padding-top: 5px; }

        .feature-info h4 { color: #e9d5ff; font-size: 17px; margin: 0 0 5px 0; font-weight: 700; }
        .feature-info p { color: #94a3b8; font-size: 14px; margin: 0; line-height: 1.5; }

        @keyframes fadeInUp {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }

        .fade-in { animation: fadeInUp 0.6s ease-out forwards; }
    </style>
</asp:Content>