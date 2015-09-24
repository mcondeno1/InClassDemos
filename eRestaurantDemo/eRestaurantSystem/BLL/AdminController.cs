using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using System.ComponentModel; //Object DataSources
#endregion


namespace eRestaurantSystem.BLL
{
    [DataObject] //Required for the ODS
    public class AdminController
    {

        //the user has to choose the method
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SpecialEvent> SpecialEvents_List()
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            //if "using" does not finish, this transaction will rollback
            using (var context = new eRestaurantContext())
            {
                // method syntax
                return context.SpecialEvents.OrderBy(x => x.Description).ToList();
            }
            
        }
    }
}
