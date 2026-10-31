<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageVisitor.master" AutoEventWireup="true" CodeFile="RegisterVisitor.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="register-container">
        <div class="register-form">
            <h2 class="register-title">Create Account</h2>
            <asp:Label ID="lblInfo" runat="server" CssClass="info-label" Visible="false"></asp:Label>

            <div class="form-group">
                <label for="txtusername" class="form-label">Username</label>
                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" Placeholder="Choose a username" />
            </div>

            <div class="form-group password-group">
                <label for="txtPass" class="form-label">Password</label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Create a strong password" />
                <i id="togglePassword" class="fas fa-eye toggle-password" title="Show Password"></i>
            </div>

            <div class="password-strength-wrapper">
                <div class="password-strength-bar">
                    <div id="strengthBar" class="strength-fill"></div>
                </div>
                <span id="strengthText" class="strength-text"></span>
            </div>

            <div class="form-group">
                <label for="txtEmail" class="form-label">Email Address</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" Placeholder="Enter your email" />
            </div>

            <div class="form-group">
                <label for="txtPhoneNumber" class="form-label">Phone Number</label>
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" Placeholder="Enter your phone number" />
            </div>

            <div class="password-requirements">
                <p class="req-title"><i class="fas fa-shield-alt"></i> Password Requirements</p>
                <ul>
                    <li id="req-length"><i class="fas fa-circle req-icon"></i> At least 8 characters</li>
                    <li id="req-criteria"><i class="fas fa-circle req-icon"></i> At least 3 of: uppercase, lowercase, number, special character (!@#$%^&*)</li>
                    <li id="req-repeat"><i class="fas fa-circle req-icon"></i> No 3 or more repeated characters</li>
                </ul>
            </div>

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Register" CssClass="btn-register" />

            <div class="signin-link-section">
                <asp:Button ID="btnSignIn" runat="server" Text="Already have an account? Sign In" CssClass="btn-goto-signin" OnClientClick="return transitionToSignIn();" />
            </div>
        </div>
    </div>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const togglePassword = document.querySelector('#togglePassword');
            const passwordField = document.querySelector('#<%= txtPass.ClientID %>');

            togglePassword.addEventListener('click', function () {
                const type = passwordField.getAttribute('type') === 'password' ? 'text' : 'password';
                passwordField.setAttribute('type', type);
                this.classList.toggle('fa-eye-slash');
                this.classList.toggle('fa-eye');
                this.setAttribute('title', type === 'password' ? 'Show Password' : 'Hide Password');
                this.classList.toggle('rotate-icon');
                passwordField.classList.toggle('highlight-password');
            });

            passwordField.addEventListener('input', function () {
                const val = this.value;
                let score = 0;

                const hasLower = /[a-z]/.test(val);
                const hasUpper = /[A-Z]/.test(val);
                const hasDigit = /[0-9]/.test(val);
                const hasSpecial = /[!@#$%^&*]/.test(val);
                const hasLength = val.length >= 8;
                const noRepeat = !(/(.)\1{2,}/.test(val));
                const criteriaCount = [hasLower, hasUpper, hasDigit, hasSpecial].filter(Boolean).length;

                setReq('req-length', hasLength);
                setReq('req-criteria', criteriaCount >= 3);
                setReq('req-repeat', noRepeat && val.length > 0);

                if (hasLength) score++;
                if (criteriaCount >= 2) score++;
                if (criteriaCount >= 3) score++;
                if (hasSpecial) score++;
                if (noRepeat && val.length > 0) score++;

                const bar = document.getElementById('strengthBar');
                const text = document.getElementById('strengthText');

                if (val.length === 0) {
                    bar.style.width = '0%';
                    bar.className = 'strength-fill';
                    text.textContent = '';
                } else if (score <= 1) {
                    bar.style.width = '25%';
                    bar.className = 'strength-fill weak';
                    text.textContent = 'Weak';
                    text.style.color = '#f87171';
                } else if (score <= 2) {
                    bar.style.width = '50%';
                    bar.className = 'strength-fill fair';
                    text.textContent = 'Fair';
                    text.style.color = '#fbbf24';
                } else if (score <= 3) {
                    bar.style.width = '75%';
                    bar.className = 'strength-fill good';
                    text.textContent = 'Good';
                    text.style.color = '#34d399';
                } else {
                    bar.style.width = '100%';
                    bar.className = 'strength-fill strong';
                    text.textContent = 'Strong';
                    text.style.color = '#a855f7';
                }
            });

            function setReq(id, met) {
                const el = document.getElementById(id);
                const icon = el.querySelector('.req-icon');
                if (met) {
                    el.classList.add('met');
                    icon.className = 'fas fa-check-circle req-icon';
                } else {
                    el.classList.remove('met');
                    icon.className = 'fas fa-circle req-icon';
                }
            }

            const formElements = document.querySelectorAll('.form-group, .btn-register, .btn-goto-signin, .password-requirements');
            formElements.forEach((element, index) => {
                element.style.animationDelay = `${index * 0.15}s`;
                element.classList.add('fade-in');
            });
        });

        function transitionToSignIn() {
            const container = document.querySelector('.register-container');
            container.classList.add('transition-out');
            setTimeout(function () {
                window.location.href = 'SignInVisitor.aspx';
            }, 500);
            return false;
        }

        window.addEventListener('pageshow', function (e) {
            const container = document.querySelector('.register-container');
            if (container) {
                container.classList.remove('transition-out');
                container.style.opacity = '1';
                container.style.transform = 'scale(1)';
            }
        });
    </script>

    <style>
        .register-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            padding: 20px;
            background-color: transparent;
        }

        .register-form {
            width: 100%;
            max-width: 450px;
            background: linear-gradient(135deg, #23232b 0%, #2d2d38 100%);
            padding: 50px 40px;
            border-radius: 20px;
            box-shadow: 0 10px 40px rgba(124, 58, 237, 0.3);
            border: 1px solid rgba(168, 85, 247, 0.2);
        }

        .register-title {
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
            box-sizing: border-box;
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
            margin-bottom: 10px;
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

        .password-strength-wrapper {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 20px;
        }

        .password-strength-bar {
            flex: 1;
            height: 6px;
            background-color: rgba(124, 58, 237, 0.15);
            border-radius: 10px;
            overflow: hidden;
        }

        .strength-fill {
            height: 100%;
            width: 0%;
            border-radius: 10px;
            transition: width 0.4s ease, background-color 0.4s ease;
        }

        .strength-fill.weak   { background-color: #f87171; }
        .strength-fill.fair   { background-color: #fbbf24; }
        .strength-fill.good   { background-color: #34d399; }
        .strength-fill.strong { background: linear-gradient(45deg, #a855f7, #7c3aed); }

        .strength-text {
            font-size: 12px;
            font-weight: 600;
            min-width: 45px;
            text-align: right;
        }

        .password-requirements {
            background-color: rgba(124, 58, 237, 0.07);
            border: 1px solid rgba(124, 58, 237, 0.2);
            border-radius: 12px;
            padding: 14px 18px;
            margin-bottom: 25px;
        }

        .req-title {
            color: #c4b5fd;
            font-size: 13px;
            font-weight: 600;
            margin: 0 0 10px 0;
        }

        .req-title .fas {
            margin-right: 6px;
            color: #a855f7;
        }

        .password-requirements ul {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .password-requirements li {
            color: #9ca3af;
            font-size: 12px;
            padding: 3px 0;
            transition: color 0.3s ease;
        }

        .password-requirements li.met {
            color: #34d399;
        }

        .req-icon {
            margin-right: 7px;
            font-size: 10px;
            color: #6b7280;
            transition: color 0.3s ease;
        }

        .password-requirements li.met .req-icon {
            color: #34d399;
        }

        .btn-register, .btn-goto-signin {
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

        .btn-register {
            background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
            margin-top: 5px;
            box-shadow: 0 4px 15px rgba(124, 58, 237, 0.4);
        }

        .btn-register:hover {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(124, 58, 237, 0.6);
            opacity: 0.95;
        }

        .signin-link-section {
            text-align: center;
            padding-top: 20px;
            margin-top: 10px;
            border-top: 1px solid rgba(124, 58, 237, 0.2);
        }

        .btn-goto-signin {
            background: transparent;
            border: 2px solid #7c3aed;
            color: #c4b5fd;
            padding: 12px 24px;
            font-size: 15px;
            margin-top: 10px;
        }

        .btn-goto-signin:hover {
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
            .register-form {
                padding: 35px 25px;
            }

            .register-title {
                font-size: 26px;
            }
        }
    </style>

</asp:Content>
