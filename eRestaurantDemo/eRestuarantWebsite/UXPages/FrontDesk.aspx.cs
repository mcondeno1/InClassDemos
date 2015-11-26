using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL;
using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;

#endregion
public partial class UXPages_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

   

    protected void SeatingGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        // extract the table number, number in party and the waiter ID
        // from the GridView.
        // we will also create the time from the MockDateTime control at the top of this
        // form. Typically, you would use DateTime.Today for current dateTime

        //once the data is collected, then it will be sent to the BLL for processing
        // the command will be done under the control of the MessageUserControl
        // if there is an error, the MUC can handle it. 
        // We will use the inlinc MUC TryRun technique
        MessageUserControl.TryRun(() => 
        { 
            //obtain the selected gridview row
            GridViewRow agvrow = SeatingGridView.Rows[e.NewSelectedIndex];
            // accessing a web control on the gridview row uses.FindControl("xxx") as dataType
            string tableNumber = (agvrow.FindControl("TableNumber") as Label).Text;
            string numberinparty = (agvrow.FindControl("NumberInParty") as TextBox).Text;
            string waiterid = (agvrow.FindControl("WaiterList") as DropDownList).SelectedValue;
            DateTime when = //DateTime.Parse(SearchDate.Text).Add(TimeSpan.Parse(SearchTime.Text));
                Mocker.MockDate.Add(Mocker.MockTime);

            //standard call to insert a record into the database
            AdminController sysmgr = new AdminController();
            sysmgr.SeatCustomer(when, byte.Parse(tableNumber), int.Parse(numberinparty), int.Parse(waiterid));
            
            //refresh the GridView
            SeatingGridView.DataBind();
        }, "Customer Seated", "New walk-in customer has been saved");
    }


    protected void ReservationSummaryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    { 
        // this is the method which will gather the seating 
        // information for reservations and pass the the BLL for processing
        //no processing will be done unless the e.CoomandName is equal to "Seat"

        if (e.CommandName.Equals("Seat"))
        { 
            MessageUserControl.TryRun(()  =>
            {
                //gather the necessary data from teh web controls
                int reservationid = int.Parse(e.CommandArgument.ToString());
                int waiterid = int.Parse(WaiterDropDownList.SelectedValue);
                DateTime when = Mocker.MockDate.Add(Mocker.MockTime);

                //we need to collect the possible multiple values from the ListBox control which contains the selected tables to be assigned to this group of customer

                List<byte> selectTables= new List<byte>();

                //walk through the lis box row by row
                foreach (ListItem item_tableid in ReservationTableListBox.Items)
                {
                    if (item_tableid.Selected)
                    {
                        selectTables.Add(byte.Parse(item_tableid.Text.Replace("Table ", "")));
                    }
                }
                   

                //with all data gathered. connect to your library controller
               // and send data for processing

                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when, reservationid, selectTables, waiterid);

                // Refresh the page
                // Refresh both the grid view
                SeatingGridView.DataBind();
                ReservationsRepeater.DataBind();
                ReservationTableListBox.DataBind();
            },"Customer seated","Reservation customer has arrvied and has been seated");
            
        }
    }


    protected bool ShowReservationSeating()
    {
        bool anyReservationToday = false;

        // call the BLL to indicate if there are any reservations today
        MessageUserControl.TryRun(() =>
            {
                DateTime when = Mocker.MockDate.Add(Mocker.MockTime);
                AdminController sysmgr = new AdminController();
                anyReservationToday = sysmgr.ReservationsForToday(when);
            });
        return anyReservationToday;
    }

}