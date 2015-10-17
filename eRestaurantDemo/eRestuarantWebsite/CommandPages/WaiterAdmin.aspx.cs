using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL; //Controller
using eRestaurantSystem.DAL.Entities; //Entity
using EatIn.UI; //Delegate ProcessRequest

#endregion

public partial class CommandPages_WaiterAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }


    protected void FetchWaiter_Click(object sender, EventArgs e)
    {
        //to properly interface with our MessageUserControl
        //we will delegate the execution of this click Event 
        //under the MessageUserControl
        if (WaiterList.SelectedIndex == 0)
        {
            MessageUserControl.ShowInfo("Please select a waiter to process.");
        }
        else
        { 
            //Execute the necessary standard lookup code under the
            //control of the MessageUserControl
            MessageUserControl.TryRun((ProcessRequest)GetWaiterInfo);
        }

    }

    public void GetWaiterInfo()
    { 
        // a standard lookup process
        AdminController sysmgr = new AdminController();

        //type cast bec WaiterList.SelectedValue return a value
        var waiter = sysmgr.GetWaiterByID(int.Parse(WaiterList.SelectedValue));
        WaiterID.Text = waiter.WaiterID.ToString();
        FirstName.Text = waiter.FirstName;
        LastName.Text = waiter.LastName;
        Address.Text = waiter.Address;
        HireDate.Text = waiter.HireDate.ToShortDateString();
        Phone.Text = waiter.Phone;

        //null field check
        if (waiter.ReleaseDate.HasValue)
        {
            ReleaseDate.Text = waiter.ReleaseDate.ToString();
        }
        else
        {
            ReleaseDate.Text = "";
        }




    }
}