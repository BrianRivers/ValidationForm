using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Had to add this to allow for form validation.
        this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

       // ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
        //scriptManager.
        //Page.Form.Controls.Add(UpdatePanel1);

        
        currentDate.Text = "Suggested reading for " + DateTime.Now.ToLongDateString();

        //Normally I would prevent the 
        populateYears();
        populateMonths();     
    }

  
    protected void yearDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Checks to see if both yearDropDown and monthDropDown are away from default value.  If so, enables dayDropdown and populates
        if (int.Parse(yearDropDown.SelectedValue.ToString()) != -1 && int.Parse(monthDropDown.SelectedValue.ToString()) != -1)
        {
                dayDropDown.Enabled = true;
                populateDays();
               // UpdatePanel1.Update();
                
        }
        else
        {
            dayDropDown.Enabled = false;
        }
        
    }

    protected void monthDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Checks to see if both yearDropDown and monthDropDown are away from default value.  If so, enables dayDropdown and populates
        if (int.Parse(yearDropDown.SelectedValue.ToString()) != -1 && int.Parse(monthDropDown.SelectedValue.ToString()) != -1)
        { 
            dayDropDown.Enabled = true;
                populateDays();
               // UpdatePanel1.Update();    
        }
        else
        {
            dayDropDown.Enabled = false;
        }
    }
   
   public void populateDays(){
            
            
           List<ListItem> daysList = new List<ListItem>();
           ListItem item = new ListItem("Days", "-1");

            //Clears current lists
           daysList.Clear();
           dayDropDown.Items.Clear();
           
           dayDropDown.Items.Add(item);


           //Creates array of days and adds it to day dropdown
           for (int count = 1; count <= System.DateTime.DaysInMonth(int.Parse(yearDropDown.SelectedValue.ToString()), int.Parse(monthDropDown.SelectedValue.ToString())); count++)
           {
               daysList.Add(new ListItem(count.ToString(), count.ToString()));
           }
           dayDropDown.Items.AddRange(daysList.ToArray());
   }


   public void populateMonths() {

       List<ListItem> monthList = new List<ListItem>();
       const int MONTHS_IN_YEAR = 12;


       //Creates array of months and adds it to month dropdown
       for (int count = 1; count <= MONTHS_IN_YEAR; count++)
       {
           monthList.Add(new ListItem(count.ToString(), count.ToString()));
       }
       monthDropDown.Items.AddRange(monthList.ToArray());
      
   }

   public void populateYears() {

       List<ListItem> yearList = new List<ListItem>();

       for (int count = DateTime.Now.Year; count >= 1900; count--)
       {
           yearList.Add(new ListItem(count.ToString(), count.ToString()));
       }
       yearDropDown.Items.AddRange(yearList.ToArray());
       
   }


   protected void submitButtonClick(object sender, EventArgs e)
   {
        const int DAYS_IN_18_YEARS = 18 * 365;
        const double PRICE_FOR_ONE = 5.00;
        const double PRICE_FOR_MORE = 5.00;
        const double MAX_PRICE = 20.00;
        const double MAX_BEFORE_TWENTY = 6;

       //Gets the current time, and the time entered in the form, then subtracts the two.
        DateTime currentDate = new DateTime();
        currentDate = DateTime.Now;
        DateTime enteredDate = new DateTime(int.Parse(yearDropDown.SelectedValue.ToString()), int.Parse(monthDropDown.SelectedValue.ToString()), int.Parse(dayDropDown.SelectedValue.ToString()));
        System.TimeSpan diff1 = currentDate - enteredDate;

        if (DAYS_IN_18_YEARS < diff1.TotalDays)
        {
            //Takes in restaurants from form and splits them by newLine.  Inserts them into array
            string[] restaurantList = restaurantForm.Text.ToString().Split(new string [] {Environment.NewLine}, StringSplitOptions.None);

            double couponBookPrice = 0;
            int restLength = restaurantList.Length;

            //Calculates cost of Coupon Book
            if (restLength == 1)
            {
                couponBookPrice = PRICE_FOR_ONE;
            }
            else if (restLength > 1 && restLength < MAX_BEFORE_TWENTY)
            {
                couponBookPrice = PRICE_FOR_ONE + PRICE_FOR_MORE * restLength-1;
            } 
            else
            {
                couponBookPrice = MAX_PRICE;
            }

            //Displays Success Message and calls email function at the end.
            documentContainer.Visible = false;
            Label messageLabel = new Label();
            messageLabel.Text = "Thank you for registering! <br /> We will be in touch. <br /> Please consider ordering a customized coupon book for just " + couponBookPrice + " dollars! <br />" + sendEmail(restaurantList, enteredDate);           
            messageContainer.Controls.Add(messageLabel);
           
        }
        else
        {

            documentContainer.Visible = false;
            Label messageLabel = new Label();
        
            messageLabel.Text= "Oops! You have " + ((int)DAYS_IN_18_YEARS - (int)diff1.TotalDays) + " days until you are 18!";

            //Displays Failure message.  Shows number of days until they turn 18
            messageContainer.Controls.Add(messageLabel);
        }
           
   }

   public string sendEmail(string [] restaurantList, DateTime enteredDate) {
       //Outputs array of restaurants into a sorted string
       string restaurantListPrinted = "";
       Array.Sort(restaurantList);
       foreach (string rlP in restaurantList)
       {
           restaurantListPrinted += rlP + "<br/>";
       }

       //Creates the Body message
       string mailMessage = "<br/>Name: " + nameForm.Text.ToString() + "<br /><br />" +
                           "Email: " + emailForm.Text.ToString() + "<br /><br />" +
                           "Date Of Birth: " + enteredDate.ToShortDateString() + "<br /><br />" +
                           "Favorite Restaurants: <br/>" + restaurantListPrinted + "<br/>";

       //Constructs the email
       MailMessage message = new MailMessage();
       message.To.Add("brivers1@my.apsu.edu");
       message.CC.Add(emailForm.Text.ToString());
       message.From = new MailAddress("brivers1@my.apsu.edu");
       message.Subject = "New registration submitted on " + DateTime.Now.ToShortDateString();
       message.Body = mailMessage;
       message.IsBodyHtml = true;

       //Attempts to send the message, prints error on failure.
       try
       {
           SmtpClient smtp = new SmtpClient("localhost", 25);
           smtp.Send(message);
       }
       catch (Exception ex) {
           Console.WriteLine("Exception Caught \n" + ex);
       }
       return mailMessage;

   }
   protected void dayDropDown_SelectedIndexChanged(object sender, EventArgs e)
   {
       
  
   }
}

/*
 
 <!-- <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager> -->





            <!-- Birthdate Form -->
            <asp:Label Text="Birthday: " runat="server" />
            <!-- Start Update Panel -->
           <!-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnDataBinding="Page_Load" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate> -->
            
                    <!-- Day Form -->
                    <asp:DropDownList ID="dayDropDown" runat="server" Enabled="False" AutoPostBack="true" OnSelectedIndexChanged="dayDropDown_SelectedIndexChanged" EnableViewState="true" >
                        <asp:ListItem Value="-1">Day</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label Text=" / " runat="server" />

                    <asp:RequiredFieldValidator ID="dayRequiredValidator" ControlToValidate="dayDropDown" runat="server" ErrorMessage="RequiredFieldValidator" InitialValue="-1"></asp:RequiredFieldValidator>
            
              
           
                    <!-- Month Form -->
                    <asp:DropDownList ID="monthDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="monthDropDown_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Month</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label Text=" / " runat="server" />
                    <asp:RequiredFieldValidator ID="monthRequiredDropDown" ControlToValidate="monthDropDown" runat="server" ErrorMessage="RequiredFieldValidator" InitialValue="-1"></asp:RequiredFieldValidator>

          
                    <!-- Year Form -->
                    <asp:DropDownList ID="yearDropDown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="yearDropDown_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Year</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="yearRequiredDropDown" ControlToValidate="yearDropDown" runat="server" ErrorMessage="RequiredFieldValidator" InitialValue="-1"></asp:RequiredFieldValidator>
                    <br />
                    <br />


                <!-- </ContentTemplate>
                <Triggers> 
                    
                    <asp:PostBackTrigger ControlID="dayDropDown" />
                </Triggers>
            </asp:UpdatePanel> -->
            <!-- End Birthday -->
 
 */