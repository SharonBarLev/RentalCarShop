using System;
using System.Web.UI;
using ByteForgeML;

public partial class ForgePCUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("SignInVisitor.aspx");
            return;
        }
    }

    protected void btnPredict_Click(object sender, EventArgs e)
    {
        float usage = float.Parse(rblUsage.SelectedValue);
        float budget = float.Parse(rblBudget.SelectedValue);
        float hours = float.Parse(rblHours.SelectedValue);
        float storage = float.Parse(rblStorage.SelectedValue);
        float graphics = float.Parse(rblGraphics.SelectedValue);
        float upgrade = float.Parse(rblUpgrade.SelectedValue);
        float futureApps = float.Parse(rblFutureApps.SelectedValue);
        float multitasking = float.Parse(rblMultitasking.SelectedValue);

        try
        {
            string predictedCode = MLManager.Predict(usage, budget, hours, storage, graphics, upgrade, futureApps, multitasking);

            string title = "";
            string description = "";

            switch (predictedCode)
            {
                case "101":
                    title = "Ultimate Gaming Rig";
                    description = "A top-tier beast designed to run any video game on maximum cinematic graphics settings with absolute smoothness.";
                    break;

                case "102":
                    title = "High-End Gaming PC";
                    description = "A powerful desktop setup that delivers amazing fluid performance for modern games and heavy daily apps without full price overkill.";
                    break;

                case "103":
                    title = "Mid-Range Gaming Laptop";
                    description = "A portable powerhouse that lets you enjoy smooth gaming and complex apps wherever you go, from your desk to a friend's house.";
                    break;

                case "104":
                    title = "Budget Office Desktop";
                    description = "A reliable, compact desktop built for everyday web surfing, heavy reading, school work, and streaming video content.";
                    break;

                case "105":
                    title = "Professional Workstation";
                    description = "A high-speed desktop built for severe professional coding, heavy calculations, running background servers, and constant complex workloads.";
                    break;

                case "106":
                    title = "Creator Powerhouse";
                    description = "An ultimate creative station optimized specifically for ultra-fast 4K video editing, heavy design rendering, and saving massive folders of assets.";
                    break;

                case "107":
                    title = "Entry-Level Gaming PC";
                    description = "A budget-friendly desktop setup that opens the door to modern video games and smooth daily multi-tasking at a very accessible price point.";
                    break;

                default:
                    title = "Custom Balanced Setup";
                    description = "A uniquely tailored configuration balancing general system speed, low power consumption, and great long-term value.";
                    break;
            }

            lblResult.Text = title;
            lblDescription.Text = description;

            pnlQuiz.Visible = false;
            pnlResult.Visible = true;
        }
        catch (Exception ex)
        {
            pnlQuiz.Visible = true;
            pnlResult.Visible = false;
            Response.Write("<script>alert('Error running AI model: " + ex.Message + "');</script>");
        }
    }
}