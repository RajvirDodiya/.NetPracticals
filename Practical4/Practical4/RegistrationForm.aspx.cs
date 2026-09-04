using System;
using System.Web.UI.WebControls;

namespace Practical4
{
    public partial class RegistrationForm : System.Web.UI.Page
    {
        protected void CuValidator1_ServerValidate(
            object source,
            ServerValidateEventArgs args)
        {
            args.IsValid = rbMale.Checked || rbFemale.Checked;
        }

        protected void CuValidator2_ServerValidate(
            object source,
            ServerValidateEventArgs args)
        {
            args.IsValid =
                Paper_Presentation.Checked ||
                Poster_Presentation.Checked ||
                Coding_Mania.Checked;
        }

        protected void btnRegister_Click(
            object sender,
            EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string gender = rbMale.Checked ? "Male" : "Female";

            string events = "";

            if (Paper_Presentation.Checked)
                events += "Paper Presentation, ";

            if (Poster_Presentation.Checked)
                events += "Poster Presentation, ";

            if (Coding_Mania.Checked)
                events += "Coding Mania, ";

            if (events.EndsWith(", "))
                events = events.Substring(0, events.Length - 2);

            string date =
                calDate.SelectedDate == DateTime.MinValue
                ? "Not Selected"
                : calDate.SelectedDate.ToString("dd-MM-yyyy");

            lblOutput.Text =
                "<h2>Event Registration Details</h2>" +
                "<hr/>" +
                "<b>Student Name :</b> " +
                txtStudentName.Text +
                "<br/><br/>" +

                "<b>GR No :</b> " +
                txtGrNo.Text +
                "<br/><br/>" +

                "<b>Email ID :</b> " +
                txtEmailId.Text +
                "<br/><br/>" +

                "<b>Mobile No :</b> " +
                txtMobileNo.Text +
                "<br/><br/>" +

                "<b>Age :</b> " +
                txtAge.Text +
                "<br/><br/>" +

                "<b>Gender :</b> " +
                gender +
                "<br/><br/>" +

                "<b>Branch :</b> " +
                DropDownList1.SelectedItem.Text +
                "<br/><br/>" +

                "<b>Event :</b> " +
                events +
                "<br/><br/>" +

                "<b>Date :</b> " +
                date;
        }

        protected void btnClear_Click(
            object sender,
            EventArgs e)
        {
            txtStudentName.Text = "";
            txtGrNo.Text = "";
            txtEmailId.Text = "";
            txtMobileNo.Text = "";
            txtAge.Text = "";
            txtPassword.Text = "";
            txtConfirmPass.Text = "";

            rbMale.Checked = false;
            rbFemale.Checked = false;

            DropDownList1.SelectedIndex = 0;

            Paper_Presentation.Checked = false;
            Poster_Presentation.Checked = false;
            Coding_Mania.Checked = false;

            calDate.SelectedDates.Clear();

            lblOutput.Text = "";
        }
    }
}