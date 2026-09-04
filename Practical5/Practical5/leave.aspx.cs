
using System;

namespace Practical5
{
    public partial class Leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check whether user is logged in
            if (Session["User_ID"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Get Employee ID from Session
                txtEmployeeId.Text =
                    Session["User_ID"].ToString();

                // Get Leave Date from Session
                if (Session["date"] != null)
                {
                    lbladte.Text =
                        Session["date"].ToString();
                }
                else
                {
                    // If no date is selected,
                    // return to calendar page
                    Response.Redirect("default.aspx");
                    return;
                }
            }
        }

        protected void btnsubmit_Click(
            object sender, EventArgs e)
        {
            // Validate all validators
            Page.Validate();

            if (!Page.IsValid)
            {
                pnlSummary.Visible = false;
                return;
            }

            // Display Employee ID
            lblSummaryEmployeeId.Text =
                txtEmployeeId.Text;

            // Display Leave Date
            lblSummaryDate.Text =
                lbladte.Text;

            // Display Leave Type
            lblSummaryLeaveType.Text =
                ddlLeaveType.SelectedValue;

            // Display Reason
            lblSummaryReason.Text =
                string.IsNullOrWhiteSpace(txtreason.Text)
                ? "Not specified"
                : txtreason.Text.Trim();

            // Display Load Adjustment
            lblSummaryLoad.Text =
                txtLoadAdjusted.Text.Trim();

            // Show summary panel
            pnlSummary.Visible = true;
        }

        protected void btnLogout_Click(
            object sender, EventArgs e)
        {
            // Clear Session
            Session.Clear();
            Session.Abandon();

            // Return to login page
            Response.Redirect("login.aspx");
        }
    }
}

