
using System;
using System.Web;

namespace Practical5
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check whether user is already logged in
            if (Session["User_ID"] != null)
            {
                Response.Redirect("default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblMessage.Text = "";

                // Read User ID from cookie
                HttpCookie cookie = Request.Cookies["User_ID"];

                if (cookie != null &&
                    !string.IsNullOrEmpty(cookie.Value))
                {
                    txtUserId.Text = cookie.Value;
                    chkRememberMe.Checked = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user_id = txtUserId.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Check login credentials
            if (user_id == "92400120478" &&
                password == "user@478")
            {
                // Store User ID in Session
                Session["User_ID"] = user_id;

                // Remember Me functionality
                if (chkRememberMe.Checked)
                {
                    HttpCookie cookie =
                        new HttpCookie("User_ID");

                    cookie.Value = user_id;

                    // Cookie valid for 30 days
                    cookie.Expires =
                        DateTime.Now.AddDays(30);

                    Response.Cookies.Add(cookie);
                }
                else
                {
                    // Delete existing cookie
                    HttpCookie cookie =
                        new HttpCookie("User_ID");

                    cookie.Expires =
                        DateTime.Now.AddDays(-1);

                    Response.Cookies.Add(cookie);
                }

                // Redirect to default page
                Response.Redirect("default.aspx");
            }
            else
            {
                lblMessage.Text =
                    "Invalid User ID or Password.";

                Session.Clear();
                Session.Abandon();
            }
        }
    }
}

