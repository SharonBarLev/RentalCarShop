<%@ Page Title="Home - ByteForge" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="HomeUser.aspx.cs" Inherits="HomeUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<style>
    .big-title { 
        font-family: 'Inter', sans-serif; 
        font-weight: 700; 
        text-align: center; 
        color: white; 
    }
    
    .gradient-text { 
        background: linear-gradient(90deg,#4facfe,#00f2fe,#7c3aed); 
        -webkit-background-clip: text; 
        -webkit-text-fill-color: transparent; 
    }

    .section-bg-dark { background-color: #111827; color: white; }
    .section-bg-blue { background-color: #0f172a; color: white; }
    .text-secondary-light { color: #cbd5e1; }
    
    .row-center { 
        display: flex; 
        justify-content: center; 
        flex-wrap: wrap; 
        gap: 1.5rem; 
    }
    
    .card-light { 
        background-color: #2c2f38; 
        padding: 2.5rem; 
        border-radius: 0.75rem; 
        flex: 0 1 320px;
        text-align: center; 
        border: 1px solid rgba(124, 58, 237, 0.1); 
        transition: 0.3s ease-in-out; 
    }
    
    .card-light:hover { 
        transform: translateY(-8px); 
        border-color: #7c3aed; 
        box-shadow: 0 15px 30px rgba(0,0,0,0.3); 
    }

    .info-box {
        background: rgba(255, 255, 255, 0.03);
        border-left: 4px solid #00f2fe;
        padding: 2rem;
        border-radius: 0.5rem;
        text-align: left;
        max-width: 800px;
        margin: 0 auto 2rem auto;
    }

    .step-number {
        font-size: 2.5rem;
        font-weight: 800;
        color: #7c3aed;
        margin-bottom: 0.5rem;
    }

    .user-cta-btn { 
        background: linear-gradient(45deg, #7c3aed, #4facfe); 
        border: none; 
        color: white !important; 
        font-weight: bold; 
        border-radius: 8px; 
        transition: 0.3s; 
    }
    .user-cta-btn:hover { opacity: 0.9; transform: scale(1.05); }
    
    .py-5 { padding-top: 3rem; padding-bottom: 3rem; }
    .mb-5 { margin-bottom: 3rem; }
</style>

<section class="py-5 section-bg-blue">
    <div class="container py-5 text-center">
        <h1 class="big-title gradient-text" style="font-size: 4rem; margin-bottom: 0.5rem;">
            Welcome Back
        </h1>
        
        <h2 class="big-title" style="font-size: 2.5rem; color: white; font-weight: 500;">
            <asp:Literal ID="litUsername" runat="server"></asp:Literal>
        </h2>
        
        <p class="big-title mt-3" style="font-size: 1.4rem; color: white; opacity: 0.8; font-weight: 300;">
            Ready to forge your next masterpiece?
        </p>

        <div class="mt-5">
            <a href="ShopUser.aspx" class="btn user-cta-btn me-3 px-5 py-3">
                <i class="fas fa-shopping-cart me-2"></i>Explore Shop
            </a>
            <a href="CartUser.aspx" class="btn btn-outline-light px-5 py-3">
                <i class="fas fa-credit-card me-2"></i>Order Now
            </a>
        </div>
    </div>
</section>

<section class="py-5 section-bg-dark">
    <div class="container text-center">
        <h2 class="big-title gradient-text mb-4" style="font-size: 2.5rem;">Why Choose ByteForge?</h2>
        <p class="text-secondary-light mb-5" style="max-width: 600px; margin-left: auto; margin-right: auto; font-size: 1.1rem;">
            We provide high-end, battle-tested hardware for gamers, creators, and developers. Every component is handpicked for maximum performance.
        </p>
        
        <div class="row-center">
            <div class="card-light" style="flex: 0 1 280px; padding: 1.5rem;">
                <i class="fas fa-microchip fa-2x mb-3" style="color: #4facfe;"></i>
                <h4 class="text-white">Premium Hardware</h4>
                <p class="text-secondary-light mt-2" style="font-size: 0.9rem;">Only top-tier brands and verified original components.</p>
            </div>
            <div class="card-light" style="flex: 0 1 280px; padding: 1.5rem;">
                <i class="fas fa-shipping-fast fa-2x mb-3" style="color: #00f2fe;"></i>
                <h4 class="text-white">Fast Delivery</h4>
                <p class="text-secondary-light mt-2" style="font-size: 0.9rem;">Secured packaging and quick dispatch right to your doorstep.</p>
            </div>
            <div class="card-light" style="flex: 0 1 280px; padding: 1.5rem;">
                <i class="fas fa-shield-alt fa-2x mb-3" style="color: #7c3aed;"></i>
                <h4 class="text-white">Full Warranty</h4>
                <p class="text-secondary-light mt-2" style="font-size: 0.9rem;">Comprehensive support and hassle-free warranty fulfillment.</p>
            </div>
        </div>
    </div>
</section>

<section class="py-5 section-bg-blue">
    <div class="container text-center">
        <h2 class="big-title gradient-text mb-4" style="font-size: 2.5rem;">Discover Which PC Fits You Best</h2>
        <p class="text-secondary-light mb-5" style="max-width: 600px; margin-left: auto; margin-right: auto; font-size: 1.1rem;">
            Unsure which hardware configuration matches your workflow? Answer a few quick questions, and let our advanced AI model evaluate your perfect powerhouse.
        </p>

        <div class="row-center">
            <div class="card-light" style="background-color: rgba(255,255,255,0.02);">
                <div class="step-number">01</div>
                <h3 class="text-white mb-2">Take the Quiz</h3>
                <p class="text-secondary-light" style="font-size: 0.95rem;">
                    Tell us about your budget, favorite gaming genres, or required rendering/coding software.
                </p>
            </div>

            <div class="card-light" style="background-color: rgba(255,255,255,0.02);">
                <div class="step-number" style="color: #00f2fe;">02</div>
                <h3 class="text-white mb-2">AI Evaluation</h3>
                <p class="text-secondary-light" style="font-size: 0.95rem;">
                    Our localized smart engine processes your criteria against current product catalog features.
                </p>
            </div>

            <div class="card-light" style="background-color: rgba(255,255,255,0.02);">
                <div class="step-number" style="color: #4facfe;">03</div>
                <h3 class="text-white mb-2">Get Your Match</h3>
                <p class="text-secondary-light" style="font-size: 0.95rem;">
                    Instantly receive a customized build recommendation tailored perfectly to your demands.
                </p>
            </div>
        </div>

        <div class="mt-5">
            <a href="ForgePCUser.aspx" class="btn user-cta-btn px-5 py-3">
                <i class="fas fa-brain me-2"></i>Analyze My Setup with AI
            </a>
        </div>
    </div>
</section>

<section class="py-5 section-bg-dark">
    <div class="container text-center">
        <h2 class="big-title gradient-text mb-5" style="font-size: 2.5rem;">Your Quick Access</h2>
        <div class="row-center">

            <div class="card-light">
                <i class="fas fa-headset fa-3x mb-4" style="color: #7c3aed;"></i>
                <h3 class="big-title gradient-text" style="font-size: 1.6rem;">Forge Support</h3>
                <p class="big-title text-secondary-light mt-3" style="font-size: 1rem; line-height: 1.6;">
                    Need help with a build or a technical issue? Our experts are here for you.
                </p>
                <a href="ContactUser.aspx" class="btn btn-outline-light mt-4 px-4 py-2">Contact Us</a>
            </div>

            <div class="card-light">
                <i class="fas fa-laptop-code fa-3x mb-4" style="color: #00f2fe;"></i>
                <h3 class="big-title gradient-text" style="font-size: 1.6rem;">Which PC Do I Need?</h3>
                <p class="big-title text-secondary-light mt-3" style="font-size: 1rem; line-height: 1.6;">
                    Answer a few short questions and we will find the perfect setup tailored specifically for your needs.
                </p>
                <a href="ForgePCUser.aspx" class="btn btn-outline-light mt-4 px-4 py-2">
                    <i class="fas fa-question-circle me-2"></i>Let's Find Out
                </a>
            </div>

        </div>
    </div>
</section>

<section class="py-5 section-bg-blue">
    <div class="container">
        <div class="row-center">
            <div class="stats-card text-center" style="flex: 1; min-width: 200px;">
                <h2 class="big-title gradient-text" style="font-size: 2.5rem;">Elite</h2>
                <p class="big-title text-secondary-light">Membership Status</p>
            </div>
            <div class="stats-card text-center" style="flex: 1; min-width: 200px;">
                <h2 class="big-title gradient-text" style="font-size: 2.5rem;">24/7</h2>
                <p class="big-title text-secondary-light">Expert Support Access</p>
            </div>
            <div class="stats-card text-center" style="flex: 1; min-width: 200px;">
                <h2 class="big-title gradient-text" style="font-size: 2.5rem;">Verified</h2>
                <p class="big-title text-secondary-light">Encrypted Sessions</p>
            </div>
        </div>
    </div>
</section>

<section class="py-5 text-center" style="background: linear-gradient(90deg,#4facfe,#00f2fe,#7c3aed);">
    <div class="container">
        <h2 class="big-title mb-3" style="font-size: 2.5rem; color: white;">The Shop is Updated</h2>
        <p class="big-title mb-4" style="font-size: 1.5rem; color: white;">
            Explore the latest high-performance components available for your next build.
        </p>
        <a href="ShopUser.aspx" class="btn btn-light px-5 py-2 fw-bold" 
           style="color: #000 !important; background-color: white !important; border-radius: 30px; font-size: 1.1rem;">
            Shop Now
        </a>
    </div>
</section>

</asp:Content>