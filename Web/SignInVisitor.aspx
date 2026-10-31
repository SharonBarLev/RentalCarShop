<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageVisitor.master" AutoEventWireup="true" CodeFile="SignInVisitor.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="signin-container">
        <div class="signin-form">
            <h2 class="signin-title">Welcome Back!</h2>
            <asp:Label ID="lblInfo" runat="server" CssClass="info-label"></asp:Label>
            
            <div class="form-group">
                <label for="txtusername" class="form-label">Username</label>
                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" Placeholder="Enter your username" />
            </div>
            
            <div class="form-group password-group">
                <label for="txtpass" class="form-label">Password</label>
                <asp:TextBox ID="txtpass" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Enter your password" />
                <i id="togglePassword" class="fas fa-eye toggle-password" title="Show Password"></i>
            </div>
            
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Sign In" CssClass="btn-signin" />

            <div class="create-account-section">
                <asp:Button ID="btnCreateAccount" runat="server" Text="Create an Account" CssClass="btn-create-account" OnClientClick="return transitionToRegister();" />
            </div>
        </div>
    </div>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const togglePassword = document.querySelector('#togglePassword');
            const password = document.querySelector('#<%= txtpass.ClientID %>');

            togglePassword.addEventListener('click', function () {
                const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
                password.setAttribute('type', type);
                this.classList.toggle('fa-eye-slash');
                this.classList.toggle('fa-eye');
                this.setAttribute('title', type === 'password' ? 'Show Password' : 'Hide Password');
                this.classList.toggle('rotate-icon');
                password.classList.toggle('highlight-password');
            });

            const formElements = document.querySelectorAll('.form-group, .btn-signin, .btn-create-account');
            formElements.forEach((element, index) => {
                element.style.animationDelay = `${index * 0.2}s`;
                element.classList.add('fade-in');
            });
        });

        function transitionToRegister() {
            const container = document.querySelector('.signin-container');
            container.classList.add('transition-out');
            setTimeout(function () {
                window.location.href = 'RegisterVisitor.aspx';
            }, 1000);
            return false;
        }

        window.addEventListener('pageshow', function (e) {
            const container = document.querySelector('.signin-container');
            if (container) {
                container.classList.remove('transition-out');
                container.style.opacity = '1';
                container.style.transform = 'scale(1)';
            }
        });
    </script>

    <style>
        .signin-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 20px;
            background-color: transparent;
        }

        .signin-form {
            width: 100%;
            max-width: 450px;
            background: linear-gradient(135deg, #23232b 0%, #2d2d38 100%);
            padding: 50px 40px;
            border-radius: 20px;
            box-shadow: 0 10px 40px rgba(124, 58, 237, 0.3);
            border: 1px solid rgba(168, 85, 247, 0.2);
        }

        .signin-title {
            text-align: center;
            font-weight: bold;
            margin-bottom: 40px;
            background: linear-gradient(45deg, #a855f7, #7c3aed);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            background-clip: text;
            font-size: 32px;
        }

        .info-label {
            display: block;
            text-align: center;
            margin-bottom: 20px;
            font-size: 14px;
            color: #f87171;
            background-color: rgba(248, 113, 113, 0.1);
            padding: 10px;
            border-radius: 8px;
        }

        .form-group {
            margin-bottom: 25px;
        }

        .form-label {
            font-weight: 600;
            color: #e9d5ff;
            display: block;
            margin-bottom: 8px;
            font-size: 14px;
            letter-spacing: 0.5px;
        }

        .form-control {
            width: 100%;
            padding: 14px 16px;
            border-radius: 12px;
            border: 2px solid rgba(124, 58, 237, 0.3);
            background-color: rgba(28, 28, 34, 0.6);
            color: #f1f1f5;
            font-size: 16px;
            transition: all 0.3s ease;
        }

        .form-control::placeholder {
            color: #9ca3af;
        }

        .form-control:focus {
            outline: none;
            border-color: #7c3aed;
            box-shadow: 0 0 0 4px rgba(124, 58, 237, 0.15);
            transform: translateY(-2px);
            background-color: rgba(28, 28, 34, 0.8);
        }

        .password-group {
            position: relative;
        }

        .toggle-password {
            position: absolute;
            right: 15px;
            top: 43px;
            cursor: pointer;
            color: #a855f7;
            transition: transform 0.3s ease, color 0.3s ease;
            font-size: 18px;
        }

        .toggle-password:hover {
            color: #c4b5fd;
        }

        .toggle-password.rotate-icon {
            transform: rotate(180deg);
            color: #7c3aed;
        }

        .highlight-password {
            background-color: rgba(124, 58, 237, 0.2);
            border-color: #7c3aed;
            transition: background-color 0.3s ease;
        }

        .btn-signin, .btn-create-account {
            width: 100%;
            padding: 16px;
            border: none;
            border-radius: 12px;
            font-weight: bold;
            font-size: 16px;
            transition: all 0.3s ease;
            cursor: pointer;
            color: #ffffff;
            letter-spacing: 0.5px;
        }

        .btn-signin {
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            margin-top: 10px;
            box-shadow: 0 4px 15px rgba(124, 58, 237, 0.4);
            margin-bottom: 25px; 
        }

        .btn-signin:hover {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(124, 58, 237, 0.6);
            opacity: 0.95;
        }

        .create-account-section {
            text-align: center;
            padding-top: 20px;
            border-top: 1px solid rgba(124, 58, 237, 0.2);
        }

        .btn-create-account {
            background: transparent;
            border: 2px solid #7c3aed;
            color: #c4b5fd;
            padding: 12px 24px;
            font-size: 15px;
            margin-top: 10px;
        }

        .btn-create-account:hover {
            background: rgba(124, 58, 237, 0.2);
            border-color: #a855f7;
            color: #ffffff;
            transform: translateY(-2px);
        }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .fade-in {
            animation: fadeInUp 0.6s ease-out forwards;
            opacity: 0;
        }

        .transition-out {
            animation: fadeOut 0.5s ease-out forwards;
        }

        @keyframes fadeOut {
            from {
                opacity: 1;
                transform: scale(1);
            }
            to {
                opacity: 0;
                transform: scale(0.95);
            }
        }

        @media (max-width: 480px) {
            .signin-form {
                padding: 35px 25px;
            }
            
            .signin-title {
                font-size: 26px;
            }
        }
    </style>

</asp:Content>