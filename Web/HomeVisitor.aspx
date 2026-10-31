<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageVisitor.master" AutoEventWireup="true" CodeFile="HomeVisitor.aspx.cs" Inherits="home" %>

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

.gradient-text-buttom {
    background: none;
    -webkit-background-clip: unset;
    -webkit-text-fill-color: white; 
}

.gradient-text-buttom .cta-button-black {
    color: black !important;
    -webkit-text-fill-color: unset !important; 
}

.section-bg-dark {
    background-color: #111827;
    color: white;
}

.section-bg-blue {
    background-color: #0f172a;
    color: white;
}

.text-secondary-light {
    color: #cbd5e1;
}

.card-light {
    background-color: #2c2f38; 
    padding: 1.2rem;
    border-radius: 0.75rem;
    box-shadow: 0 1px 3px rgba(0,0,0,0.1);
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    text-align: center;
    flex: 1 1 22%; 
    margin: 0.5rem;
    min-width: 220px; 
}

.row-center {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
    gap: 1rem;
}

.stats-card {
    background: none;
    flex: 1 1 22%;
    min-width: 180px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    text-align: center;
    margin: 0.5rem;
}

.py-5 { padding-top: 3rem; padding-bottom: 3rem; }
.mb-5 { margin-bottom: 3rem; }
</style>


<section class="py-5 section-bg-blue">
    <div class="container py-5 text-center">
        <h1 class="big-title gradient-text" style="font-size: 3rem;">
            Join the Ultimate PC Hardware Marketplace
        </h1>
        <p class="big-title mb-4" style="font-size: 1.5rem;">
            Access exclusive components and premium builds.
        </p>

        <div class="mt-4">
            <a href="RegisterVisitor.aspx" class="btn gradient-btn me-3 px-4 py-2">
                Register Now
            </a>
            <a href="SignInVisitor.aspx" class="btn btn-outline-light px-4 py-2">
                Sign In
            </a>
        </div>

        <p class="mt-3 big-title text-secondary-light" style="font-size: 1rem;">
            Free registration • No hidden fees • Instant access
        </p>
    </div>
</section>

<section class="py-5 section-bg-dark">
    <div class="container text-center">
        <h2 class="big-title gradient-text mb-5" style="font-size: 2.5rem;">What You Get After Registration</h2>
        <div class="row-center">
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">Browse Full Hardware Catalog</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Explore our complete collection of high-performance components.
                </p>
            </div>
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">Smart Build Assistant</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Build smarter with automatic compatibility checks.
                </p>
            </div>
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">Fast & Secure Orders</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Quick processing and protected shipping.
                </p>
            </div>
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">Secure Payments & Warranty</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Safe transactions and full warranty coverage.
                </p>
            </div>
        </div>
    </div>
</section>

<section class="py-5 text-white text-center section-bg-blue">
    <div class="container">
        <h2 class="big-title gradient-text mb-3" style="font-size: 2rem;">Performance Without Limits</h2>
        <p class="big-title text-secondary-light mb-5" style="font-size: 1.1rem;">
            Built for those who demand more from every frame, every render, every build.
        </p>

        <div class="row-center">
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">⚡ Hardware That Hits Different</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Cutting-edge components engineered for raw speed and stability.
                </p>
            </div>
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">🧠 Smart Compatibility Engine</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    No bottlenecks. No guesswork. Just perfect performance.
                </p>
            </div>
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">🚀 Built for Power Users</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Gamers. Creators. Developers. If performance matters — you're home.
                </p>
            </div>
            <div class="card-light">
                <h5 class="big-title gradient-text" style="font-size: 1.25rem;">🔒 Members-Only Advantage</h5>
                <p class="big-title text-secondary-light mt-2" style="font-size: 1rem;">
                    Exclusive access. Premium gear. Serious upgrades.
                </p>
            </div>
        </div>
    </div>
</section>

<section class="py-5 section-bg-dark">
    <div class="container">
        <div class="row-center">
            <div class="stats-card">
                <h2 class="big-title gradient-text">2,000+</h2>
                <p class="big-title text-secondary-light">Products</p>
            </div>
            <div class="stats-card">
                <h2 class="big-title gradient-text">500+</h2>
                <p class="big-title text-secondary-light">Active Members</p>
            </div>
            <div class="stats-card">
                <h2 class="big-title gradient-text">4.9★</h2>
                <p class="big-title text-secondary-light">Average Rating</p>
            </div>
            <div class="stats-card">
                <h2 class="big-title gradient-text">99%</h2>
                <p class="big-title text-secondary-light">Customer Satisfaction</p>
            </div>
        </div>
    </div>
</section>

<section class="py-5 text-center gradient-text-buttom" style="background: linear-gradient(90deg,#4facfe,#00f2fe,#7c3aed);">
    <div class="container">
        <h2 class="big-title mb-3" style="font-size: 2.5rem; color: white;">Ready to Unlock the Full Store?</h2>
        <p class="big-title mb-4" style="font-size: 1.5rem; color: white;">
            Create your free account and access the complete marketplace.
        </p>
        <a href="RegisterVisitor.aspx" class="btn cta-button-black px-5 py-2 fw-bold" 
           style="color: #000000 !important; background-color: white !important; -webkit-text-fill-color: initial !important;">
            Create Free Account
        </a>
    </div>
</section>

</asp:Content>
