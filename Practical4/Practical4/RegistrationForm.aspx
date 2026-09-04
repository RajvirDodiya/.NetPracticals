<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="RegistrationForm.aspx.cs"
    Inherits="Practical4.RegistrationForm"
    UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Online Event Registration</title>
</head>

<body style="font-family:Arial;background:#f2f2f2;margin:0">

<form id="form1" runat="server">

<div style="width:700px;margin:30px auto;padding:25px;background:white;border-radius:10px">

<h1 style="text-align:center">Online Event Registration</h1>

<table style="width:100%;border-spacing:10px">

    <!-- NAME -->
    <tr>
        <td><b>Enter Name</b></td>
        <td>
            <asp:TextBox ID="txtStudentName" runat="server" />
        </td>
        <td>
            <asp:RequiredFieldValidator
                ID="RFValidator"
                runat="server"
                ControlToValidate="txtStudentName"
                ErrorMessage="Student Name Is Required"
                ForeColor="Red">*</asp:RequiredFieldValidator>
        </td>
    </tr>

    <!-- GR NO -->
    <tr>
        <td><b>Enter GR No.</b></td>
        <td>
            <asp:TextBox ID="txtGrNo" runat="server" />
        </td>
    </tr>

    <!-- EMAIL -->
    <tr>
        <td><b>Enter Email ID</b></td>
        <td>
            <asp:TextBox ID="txtEmailId" runat="server" />
        </td>
        <td>
            <asp:RegularExpressionValidator
                ID="REValidator1"
                runat="server"
                ControlToValidate="txtEmailId"
                ErrorMessage="Enter Valid Email"
                ForeColor="Red"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
        </td>
    </tr>

    <!-- MOBILE -->
    <tr>
        <td><b>Enter Mobile No</b></td>
        <td>
            <asp:TextBox ID="txtMobileNo" runat="server" />
        </td>
    </tr>

    <!-- AGE -->
    <tr>
        <td><b>Enter Age</b></td>
        <td>
            <asp:TextBox ID="txtAge" runat="server" />
        </td>
        <td>
            <asp:RangeValidator
                ID="RValidator1"
                runat="server"
                ControlToValidate="txtAge"
                MinimumValue="18"
                MaximumValue="40"
                Type="Integer"
                ErrorMessage="Age Should Be Between 18 to 40"
                ForeColor="Red" />
        </td>
    </tr>

    <!-- PASSWORD -->
    <tr>
        <td><b>Enter Password</b></td>
        <td>
            <asp:TextBox
                ID="txtPassword"
                runat="server"
                TextMode="Password" />
        </td>
    </tr>

    <!-- CONFIRM PASSWORD -->
    <tr>
        <td><b>Confirm Password</b></td>
        <td>
            <asp:TextBox
                ID="txtConfirmPass"
                runat="server"
                TextMode="Password" />
        </td>
        <td>
            <asp:CompareValidator
                ID="CValidator1"
                runat="server"
                ControlToValidate="txtConfirmPass"
                ControlToCompare="txtPassword"
                ErrorMessage="Password and Confirm Password Both Should Match"
                ForeColor="Red" />
        </td>
    </tr>

    <!-- GENDER -->
    <tr>
        <td><b>Gender</b></td>
        <td>
            <asp:RadioButton
                ID="rbMale"
                runat="server"
                Text="Male" />

            &nbsp;&nbsp;

            <asp:RadioButton
                ID="rbFemale"
                runat="server"
                Text="Female" />
        </td>
        <td>
            <asp:CustomValidator
                ID="CuValidator1"
                runat="server"
                ErrorMessage="Select any one gender"
                ForeColor="Red"
                OnServerValidate="CuValidator1_ServerValidate"
                ValidateEmptyText="True" />
        </td>
    </tr>

    <!-- BRANCH -->
    <tr>
        <td><b>Branch</b></td>
        <td>
            <asp:DropDownList
                ID="DropDownList1"
                runat="server">

                <asp:ListItem Selected="True">Select Branch</asp:ListItem>
                <asp:ListItem>CSE</asp:ListItem>
                <asp:ListItem>CSE-CS</asp:ListItem>
                <asp:ListItem>AI/ML</asp:ListItem>

            </asp:DropDownList>
        </td>
    </tr>

    <!-- EVENT -->
    <tr>
        <td><b>Select Event Type</b></td>
        <td>

            <asp:CheckBox
                ID="Paper_Presentation"
                runat="server"
                Text="Paper Presentation" />

            <br />

            <asp:CheckBox
                ID="Poster_Presentation"
                runat="server"
                Text="Poster Presentation" />

            <br />

            <asp:CheckBox
                ID="Coding_Mania"
                runat="server"
                Text="Coding Mania" />

        </td>
        <td>
            <asp:CustomValidator
                ID="CuValidator2"
                runat="server"
                ErrorMessage="Select at least one event"
                ForeColor="Red"
                OnServerValidate="CuValidator2_ServerValidate"
                ValidateEmptyText="True" />
        </td>
    </tr>

    <!-- DATE -->
    <tr>
        <td><b>Select Date</b></td>
        <td>

            <asp:Calendar
                ID="calDate"
                runat="server"
                BackColor="White"
                BorderColor="Black"
                BorderWidth="1px"
                Width="350px">

                <DayHeaderStyle Font-Bold="True" />

                <SelectedDayStyle
                    BackColor="#333399"
                    ForeColor="White" />

                <TodayDayStyle BackColor="#CCCCCC" />

                <OtherMonthDayStyle ForeColor="#999999" />

                <TitleStyle
                    Font-Bold="True"
                    ForeColor="#333399" />

            </asp:Calendar>

        </td>
    </tr>

    <!-- IMAGE -->
    <tr>
        <td><b>Event Banner</b></td>
        <td>

            <asp:Image
                ID="Image1"
                runat="server"
                ImageUrl="Image/event.png"
                Width="300px" />

        </td>
    </tr>

    <!-- BUTTONS -->
    <tr>
        <td></td>
        <td>

            <asp:Button
                ID="btnRegister"
                runat="server"
                Text="Register"
                OnClick="btnRegister_Click"
                CausesValidation="True"
                style="padding:8px 20px" />

            <asp:Button
                ID="btnClear"
                runat="server"
                Text="Clear Form"
                OnClick="btnClear_Click"
                CausesValidation="False"
                style="padding:8px 20px" />

        </td>
    </tr>

    <!-- OUTPUT -->
    <tr>
        <td colspan="3">

            <asp:Label
                ID="lblOutput"
                runat="server" />

        </td>
    </tr>

    <!-- VALIDATION SUMMARY -->
    <tr>
        <td colspan="3">

            <asp:ValidationSummary
                ID="ValidationSummary1"
                runat="server"
                HeaderText="Please correct the following errors:"
                ForeColor="Red" />

        </td>
    </tr>

</table>

</div>

</form>

</body>
</html>