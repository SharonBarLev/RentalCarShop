<%@ Page Title="ForgePC - Guided AI Assistant" Language="C#" MasterPageFile="~/MasterPageUser.master" AutoEventWireup="true" CodeFile="ForgePCUser.aspx.cs" Inherits="ForgePCUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
    .question-step { display: none; }
    .question-step.active { display: block; animation: fadeIn 0.4s ease-in-out; }
    
    .custom-rbl table { width: 100%; border-collapse: separate; border-spacing: 15px; }
    .custom-rbl tbody { display: grid; grid-template-columns: 1fr 1fr; gap: 15px; width: 100%; }
    .custom-rbl tr { background: #1f2937; border-radius: 12px; display: block; width: 100%; transition: all 0.3s ease; position: relative; padding: 2px; }
    
    .custom-rbl input[type="radio"] { display: none !important; }
    
    .custom-rbl td { padding: 20px; display: block; width: 100%; height: 100%; cursor: pointer; min-height: 120px; }
    .custom-rbl label { color: white; cursor: pointer; font-size: 1.1rem; font-weight: 600; display: block; width: 100%; margin: 0; }
    .option-desc { display: block; font-size: 0.85rem; color: #9ca3af; margin-top: 5px; font-weight: normal; }

    .custom-rbl tr:hover { background: #2d3748; transform: translateY(-2px); }

    .custom-rbl tr.selected-card {
        background: linear-gradient(45deg, #00aaff 0%, #9900ff 100%);
        box-shadow: 0 0 15px rgba(153, 0, 255, 0.4);
    }
    .custom-rbl tr.selected-card td {
        background: #111827;
        border-radius: 10px;
        margin: 1px;
    }

    .spinner-container { display: none; text-align: center; padding: 60px 0; }
    .custom-spinner { width: 4rem; height: 4rem; border: 0.4em solid rgba(255,255,255,0.1); border-left-color: #7c3aed; border-radius: 50%; animation: spin 1s linear infinite; display: inline-block; }
    
    @keyframes spin { to { transform: rotate(360deg); } }
    @keyframes fadeIn { from { opacity: 0; transform: translateY(10px); } to { opacity: 1; transform: translateY(0); } }
</style>

<div class="container py-5" style="background-color: #0f172a; color: white; min-height: 75vh; border-radius: 12px; margin-top: 30px;">
    
    <asp:Panel ID="pnlQuiz" runat="server">
        <div class="row justify-content-center mb-4" id="progressWrapper" style="display: none;">
            <div class="col-md-9">
                <div class="progress" style="background-color: #1e293b; height: 8px;">
                    <div id="progressBar" class="progress-bar" role="progressbar" style="width: 0%; background: linear-gradient(90deg, #4facfe, #7c3aed);" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100"></div>
                </div>
                <small class="text-muted d-block text-end mt-1" id="progressText">Question 1 of 8</small>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-9">
            
                <div id="quizCard" style="background-color: #111827; padding: 40px; border-radius: 14px; border: 1px solid rgba(124, 58, 237, 0.15);">
                    
                    <div class="question-step active" data-step="0" style="text-align: center; padding: 20px 0;">
                        <i class="fas fa-robot text-info mb-4" style="font-size: 3.5rem; background: linear-gradient(45deg, #4facfe, #7c3aed); -webkit-background-clip: text; -webkit-text-fill-color: transparent;"></i>
                        <h2 class="fw-bold mb-3" style="color: white;">Ready to Forge Your Perfect PC?</h2>
                        <p class="text-muted fs-5 mx-auto mb-4" style="max-width: 500px;">
                            Answer a few short questions about your daily habits, budget, and needs. Our custom AI model will analyze your benchmarks and match you with the ideal hardware setup.
                        </p>                    
                    </div>

                    <div class="question-step" data-step="1">
                        <h3 class="text-info mb-4">1. What will you do most of the time on this computer?</h3>
                        <asp:RadioButtonList ID="rblUsage" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">Play Video Games<span class="option-desc">From casual gaming to high-end realistic visual adventures.</span></asp:ListItem>
                            <asp:ListItem Value="2">Everyday Homework & Internet<span class="option-desc">Watching movies, reading articles, school work, and writing documents.</span></asp:ListItem>
                            <asp:ListItem Value="3">Write Code / Professional Programs<span class="option-desc">Building software, apps, website backends, and technical tasks.</span></asp:ListItem>
                            <asp:ListItem Value="4">Edit Videos & Advanced Design<span class="option-desc">Creating 4K video content, heavy graphics effects, and 3D modeling.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="2">
                        <h3 class="text-info mb-4">2. How much are you planning or willing to invest?</h3>
                        <asp:RadioButtonList ID="rblBudget" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">As cheap as possible<span class="option-desc">Looking for basic computing without breaking the bank.</span></asp:ListItem>
                            <asp:ListItem Value="2">A fair, average budget<span class="option-desc">Getting a reliable setup with a good balance of value.</span></asp:ListItem>
                            <asp:ListItem Value="3">Ready to invest for high performance<span class="option-desc">Wanting high speeds and great quality for serious workloads.</span></asp:ListItem>
                            <asp:ListItem Value="4">Top tier setup / Best money can buy<span class="option-desc">No strict limit, prioritizing maximum power and ultimate hardware.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="3">
                        <h3 class="text-info mb-4">3. How many hours a day will this computer be working hard?</h3>
                        <asp:RadioButtonList ID="rblHours" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">Just 1-2 hours a day<span class="option-desc">Quick casual sessions or short evening tasks.</span></asp:ListItem>
                            <asp:ListItem Value="2">Around 3-6 hours a day<span class="option-desc">Standard daily workflow or typical entertainment routine.</span></asp:ListItem>
                            <asp:ListItem Value="3">Practically all day long (7+ hours)<span class="option-desc">The PC will work non-stop for heavy loads, long sessions, or processing tasks.</span></asp:ListItem>
                            <asp:ListItem Value="4">Runs 24/7 without shutting down<span class="option-desc">Always on, running background automated processes or servers.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="4">
                        <h3 class="text-info mb-4">4. How many files, games, or photos do you plan to save?</h3>
                        <asp:RadioButtonList ID="rblStorage" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">Very few / Mostly online streaming<span class="option-desc">Using cloud storage, web apps, and keeping minimal local files.</span></asp:ListItem>
                            <asp:ListItem Value="2">Average amount<span class="option-desc">A normal mix of basic software, daily documents, and a few games.</span></asp:ListItem>
                            <asp:ListItem Value="3">A lot of heavy files<span class="option-desc">Dozens of massive modern games, raw video recordings, or heavy source codes.</span></asp:ListItem>
                            <asp:ListItem Value="4">Infinite space needed<span class="option-desc">Massive local archive storage for absolute security and huge projects.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="5">
                        <h3 class="text-info mb-4">5. How important are crystal-clear visual details to you?</h3>
                        <asp:RadioButtonList ID="rblGraphics" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">Standard quality is totally fine<span class="option-desc">As long as it's clear and fluid, I don't need insane pixel density.</span></asp:ListItem>
                            <asp:ListItem Value="2">Sharp and modern look<span class="option-desc">Wanting beautiful high-definition colors and great visual fidelity.</span></asp:ListItem>
                            <asp:ListItem Value="3">Elite visual clarity<span class="option-desc">Extremely sharp, cinematic experience with the highest detailing possible.</span></asp:ListItem>
                            <asp:ListItem Value="4">Absolute realism is a must<span class="option-desc">Professional grade output, immersive simulation, and top-tier visuals.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="6">
                        <h3 class="text-info mb-4">6. Do you want to easily swap internal parts yourself in the future?</h3>
                        <asp:RadioButtonList ID="rblUpgrade" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">Yes, I want to upgrade it over time<span class="option-desc">Allowing me to replace individual pieces later to make it faster.</span></asp:ListItem>
                            <asp:ListItem Value="2">No, I want to use it exactly as bought<span class="option-desc">I prefer to buy it ready, use it for years, and buy a new one when it's old.</span></asp:ListItem>
                            <asp:ListItem Value="3">Maybe just add basic storage/RAM later<span class="option-desc">Keeping options open for minor speed enhancements in a few years.</span></asp:ListItem>
                            <asp:ListItem Value="4">I want absolute future-proof core infrastructure<span class="option-desc">A premium base that ensures full compatibility with future releases.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="7">
                        <h3 class="text-info mb-4">7. How many years do you expect this PC to run new software smoothly?</h3>
                        <asp:RadioButtonList ID="rblFutureApps" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">2-3 Years<span class="option-desc">Good for immediate needs, planning to change or upgrade relatively soon.</span></asp:ListItem>
                            <asp:ListItem Value="2">4-5 Years<span class="option-desc">The standard sweet spot. Reliable performance over a normal lifespan.</span></asp:ListItem>
                            <asp:ListItem Value="3">6+ Years into the future<span class="option-desc">Want a powerhouse that safely survives future software updates for a long time.</span></asp:ListItem>
                            <asp:ListItem Value="4">No compromise longevity<span class="option-desc">Overkill specs right now to guarantee maximum relevance for years to come.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div class="question-step" data-step="8">
                        <h3 class="text-info mb-4">8. How many programs or browser tabs do you usually keep open together?</h3>
                        <asp:RadioButtonList ID="rblMultitasking" runat="server" CssClass="custom-rbl">
                            <asp:ListItem Value="1">One thing at a time<span class="option-desc">I usually focus on a single app or keep just 2-3 tabs open.</span></asp:ListItem>
                            <asp:ListItem Value="2">A normal amount<span class="option-desc">A browser with a few tabs, Spotify background music, and a chat app.</span></asp:ListItem>
                            <asp:ListItem Value="3">A lot of windows open<span class="option-desc">Heavier multitasking, switching between code editors, heavy excel sheets, and streaming.</span></asp:ListItem>
                            <asp:ListItem Value="4">Extreme overload<span class="option-desc">Dozens of browser tabs, virtual machines, background servers, and editing tools running all at once.</span></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div id="navButtons" class="d-flex justify-content-between mt-5">
                        <button type="button" id="btnPrev" class="btn btn-outline-secondary px-4 fw-bold" onclick="changeStep(-1)" style="display:none;">⬅ Back</button>
                        <button type="button" id="btnNext" class="btn btn-info text-white px-5 fw-bold" onclick="changeStep(1)">Let's Start! ➡</button>
                        <asp:Button ID="btnPredict" runat="server" OnClick="btnPredict_Click" Style="display: none;" />
                    </div>
                </div>

                <div id="loadingContainer" class="spinner-container">
                    <div class="custom-spinner mb-3"></div>
                    <h3 class="text-muted">ByteForge AI is analyzing your answers...</h3>
                    <p class="text-info fs-6">Calculating hardware benchmarks and choosing the absolute best matching tier...</p>
                </div>
            </div>
        </div>
    </asp:Panel>

    <div class="row justify-content-center">
        <div class="col-md-8 text-center">
            <asp:Panel ID="pnlResult" runat="server" Visible="false" Style="animation: fadeIn 0.8s ease-out; padding: 40px 0;">
                <h2 class="text-muted mb-2" style="font-size: 1.2rem; letter-spacing: 3px;">YOUR PERFECT MATCH</h2>
                
                <asp:Label ID="lblResult" runat="server" CssClass="display-3 fw-bold d-block mb-3" Style="background: linear-gradient(45deg, #00f2fe, #4facfe, #7c3aed); -webkit-background-clip: text; -webkit-text-fill-color: transparent;"></asp:Label>
                
                <div class="p-3 mb-4 mx-auto" style="max-width: 600px;">
                    <asp:Label ID="lblDescription" runat="server" CssClass="fs-5 text-light d-block lh-base"></asp:Label>
                </div>

                <a href="ForgePCUser.aspx" class="btn btn-outline-info mt-3 px-4 py-2 fw-bold" style="border-radius: 8px;">🔄 Retake Questionnaire</a>
            </asp:Panel>
        </div>
    </div>
</div>

<script type="text/javascript">
    let currentStep = 0;
    const totalSteps = 8;

    function setupCardSelection() {
        document.querySelectorAll('.custom-rbl tr').forEach(tr => {
            tr.onclick = function () {
                const radio = tr.querySelector('input[type="radio"]');
                if (radio) {
                    radio.checked = true;
                    tr.closest('tbody').querySelectorAll('tr').forEach(r => r.classList.remove('selected-card'));
                    tr.classList.add('selected-card');
                }
            };
            const radio = tr.querySelector('input[type="radio"]');
            if (radio && radio.checked) {
                tr.classList.add('selected-card');
            }
        });
    }

    function updateNavLayout(step) {
        const navContainer = document.getElementById('navButtons');
        const btnNext = document.getElementById('btnNext');

        if (step === 0) {
            navContainer.classList.add('justify-content-center');
            navContainer.classList.remove('justify-content-between');
            btnNext.classList.remove('ms-auto');
        } else {
            navContainer.classList.remove('justify-content-center');
            navContainer.classList.add('justify-content-between');
            btnNext.classList.add('ms-auto'); 
        }
    }

    function changeStep(direction) {
        if (direction === 1 && currentStep >= 1) {
            const currentActiveStep = document.querySelector(`.question-step[data-step="${currentStep}"]`);
            const selectedRadio = currentActiveStep.querySelector('input[type="radio"]:checked');

            if (!selectedRadio) {
                alert("Please select an option before moving forward!");
                return;
            }
        }

        document.querySelector(`.question-step[data-step="${currentStep}"]`).classList.remove('active');
        currentStep += direction;

        if (currentStep > totalSteps) {
            triggerAILoading();
            return;
        }

        document.querySelector(`.question-step[data-step="${currentStep}"]`).classList.add('active');
        updateNavLayout(currentStep);
        if (currentStep === 0) {
            document.getElementById('btnPrev').style.display = 'none';
            document.getElementById('progressWrapper').style.display = 'none';
            document.getElementById('btnNext').innerText = "Let's Start! ➡";
        } else {
            document.getElementById('btnPrev').style.display = 'block';
            document.getElementById('progressWrapper').style.display = 'flex';
            document.getElementById('btnNext').innerText = currentStep === totalSteps ? ' Calculate Result' : 'Next ➡';
            let progressPercent = (currentStep / totalSteps) * 100;
            document.getElementById('progressBar').style.width = progressPercent + '%';
            document.getElementById('progressText').innerText = `Question ${currentStep} of ${totalSteps}`;
        }
    }

    function triggerAILoading() {
        document.getElementById('quizCard').style.display = 'none';
        document.getElementById('progressWrapper').style.display = 'none';
        document.getElementById('loadingContainer').style.display = 'block';

        setTimeout(function () {
            document.getElementById('<%= btnPredict.ClientID %>').click();
        }, 1500);
    }

    window.onload = function () {
        setupCardSelection();
        updateNavLayout(currentStep);
    };
</script>
</asp:Content>