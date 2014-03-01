<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Async="true" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coupons, Coupons, Coupons!</title>

    <!-- Bootstrap Links -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <!-- DateJS -->
    <script src="js/date.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            // DateTime yearsAgo = new DateTime();

            //$("#dayDropDown").prepend("<option selected='selected' value='0'> " + Date.today().addYears(-18).getDay() + " </option>");
            //$("#monthDropDown").prepend("<option selected='selected' value='0'> " + Date.today().addYears(-18).getMonth() + " </option>");
            // $("#yearDropDown").prepend("<option selected='selected' value='0'> " + Date.today().addYears(-18).getFullYear() + " </option>");


        })


    </script>

    <!-- JQuery -->

    <script src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

</head>
<body style="background-color:#222">

    <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="Register.aspx">Coupons, Coupons, Coupons!</a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav">
                    <li class="active"><a href="#">Home</a></li>
                    <li><a href="#about">About</a></li>
                    <li><a href="#contact">Contact</a></li>
                </ul>
            </div>
            <!--/.nav-collapse -->
        </div>
    </div>

    <div class="container" >
        <div class="navbar">
            <ul class="nav navbar-nav">
                <li class="active"><a href="#"></a></li>
                <li><a href="#features"></a></li>
                <li><a href="#"></a></li>
                <li><a href="#"></a></li>
            </ul>
        </div>

       
     
        <form id="form1" runat="server">
            <div class="well">
            <asp:Panel runat="server" ID="messageContainer" >

                <asp:Panel runat="server" ID="documentContainer" >
                <!-- Link to favorite programming blog.  Displays curret day/month/year-->
                <div class="well well-sm">


                    <h3>Great Programming Site:</h3>
                    <a href="http://scotch.io/">
                        <asp:Label ID="currentDate" runat="server"></asp:Label></a> <br /> <br />

                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Sorry, there are some required fields:" ForeColor="#CC3300" />
                </div>
                     <hr />
                <div id="formContainer" class="well well-lg">
                    <!--Name Form -->
                    <asp:Label ID="nameLabel" Text="Name: " runat="server" /><asp:TextBox ID="nameForm" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="nameValidator" ControlToValidate="nameForm" runat="server" ErrorMessage="Name must be between 3 and 50 letters" ValidationExpression="^[a-zA-Z'.\s]{3,50}$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="nameRequiredValidator" ControlToValidate="nameForm" runat="server" ErrorMessage="Your name is a required field!" Text="*"></asp:RequiredFieldValidator>
                    <br />
                    <br />

                    <!--Email Form -->
                    <asp:Label Text="Email: " runat="server" /><asp:TextBox ID="emailForm" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="emailValidValidator" runat="server" ErrorMessage="This is an invalid email address" ControlToValidate="emailForm" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="*"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="emailRequiredValidator" runat="server" ControlToValidate="emailForm" ErrorMessage="Your email is a required field!">*</asp:RequiredFieldValidator>
                    <br />
                    <br />

                    <!-- Birthdate Form -->
                    <asp:Label Text="Birthday: " runat="server" />
                    <!-- Day Form -->
                    <asp:DropDownList ID="dayDropDown" runat="server" Enabled="False" AutoPostBack="true" OnSelectedIndexChanged="dayDropDown_SelectedIndexChanged" EnableViewState="true">
                        <asp:ListItem Value="-1">Day</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label Text=" / " runat="server" />

                    <asp:RequiredFieldValidator ID="dayRequiredValidator" ControlToValidate="dayDropDown" runat="server" ErrorMessage="You must enter a day of birth!" InitialValue="-1" Text="*"></asp:RequiredFieldValidator>

                    <!-- Month Form -->
                    <asp:DropDownList ID="monthDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="monthDropDown_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Month</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label Text=" / " runat="server" />
                    <asp:RequiredFieldValidator ID="monthRequiredDropDown" ControlToValidate="monthDropDown" runat="server" ErrorMessage="You must enter a month of birth!" InitialValue="-1" Text="*">*</asp:RequiredFieldValidator>

                    <!-- Year Form -->
                    <asp:DropDownList ID="yearDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="yearDropDown_SelectedIndexChanged" >
                        <asp:ListItem Value="-1">Year</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="yearRequiredDropDown" ControlToValidate="yearDropDown" runat="server" ErrorMessage="You must enter a year of birth!" InitialValue="-1" Text="*"></asp:RequiredFieldValidator>
                    <br />
                    <br />

                    <!-- Favorite Restaurants -->
                    <asp:Label Text="Please Enter Your Favorite Restaurants" runat="server" />
                    <br />
                    <br />
                    <asp:TextBox ID="restaurantForm" Text="One Restaurant to a line" runat="server" TextMode="MultiLine" Height="400px" Width="400px" />
                    <asp:RequiredFieldValidator ID="restaurantRequiredForm" ControlToValidate="restaurantForm" runat="server" ErrorMessage="You Must enter at least 1 restaurant" InitialValue="One Restaurant to a line">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="restaurantRequiredForm2" ControlToValidate="restaurantForm" runat="server" ErrorMessage="You must enter at least 1 restaurant" >*</asp:RequiredFieldValidator>
                    <br />
                    <br />


                    <!-- Submit Button -->
                    <asp:Button ID="submitButton" Text="Submit" runat="server" OnClick="submitButtonClick" UseSubmitBehavior="false" />
                    </asp:Panel>
                
            </asp:Panel>
                </div>
                </div>
            
             
            <!-- /.container -->
        </form>
    </div>

</body>
</html>
