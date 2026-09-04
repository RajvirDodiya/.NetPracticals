
using System;

namespace Practical5
{
    public partial class Default : System.Web.UI.Page
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
                // Display logged-in User ID
                lblHello.Text =
                    "Hello, " + Session["User_ID"].ToString();

                lblchoosedate.Text = "";
                lblDateError.Text = "";
            }
        }

        protected void CalSelectDate_SelectionChanged(
            object sender, EventArgs e)
        {
            DateTime dt =
                CalSelectDate.SelectedDate;

            if (dt != DateTime.MinValue)
            {
                // Display selected date
                lblchoosedate.Text =
                    dt.ToString("dd/MM/yyyy");

                // Store selected date in Session
                Session["date"] =
                    dt.ToString("dd/MM/yyyy");

                lblDateError.Text = "";
            }
        }

        protected void APleave_Click(
            object sender, EventArgs e)
        {
            // Check whether date is selected
            if (CalSelectDate.SelectedDate ==
                DateTime.MinValue)
            {
                lblDateError.Text =
                    "Please select a leave date before applying for leave.";

                return;
            }

            // Store selected date in Session
            Session["date"] =
                CalSelectDate.SelectedDate
                .ToString("dd/MM/yyyy");

            // Open leave application page
            Response.Redirect("leave.aspx");
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

