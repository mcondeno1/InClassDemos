using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespace
using eRestaurantSystem.BLL;

#endregion

public partial class UserControls_DateTimeMocker : System.Web.UI.UserControl
{

    public DateTime MockDate
    {
        get {
          //create a DateTime variable and assign it a default value
            DateTime date = DateTime.MinValue;


            //override the defaul with the contents of the textbox search date
            DateTime.TryParse(SearchDate.Text, out date);

            //passback a date either default or text box
            return date;
        }
        set {
            SearchDate.Text = value.ToString("yyyy-MM-dd");
        }
    }


    public TimeSpan MockTime
    {
        get
        {
            //create a DateTime variable and assign it a default value
            TimeSpan time = TimeSpan.MinValue;


            //override the defaul with the contents of the textbox search date
            //textbox
            TimeSpan.TryParse(SearchTime.Text, out time);

            //passback a date either default or text box
            return time;
        }
        set
        {
            SearchTime.Text = DateTime.Today.Add(value).ToString("HH:MM:ss");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
           
    }

    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        DateTime info = sysmgr.GetLastBillDateTime();
        SearchDate.Text = info.ToString("yyyy-MM-dd");
        SearchTime.Text = info.ToString("HH:mm");

    }
}