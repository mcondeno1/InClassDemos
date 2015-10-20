using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
using System.ComponentModel; //Object DataSources
#endregion


namespace eRestaurantSystem.BLL
{
    [DataObject] //Required for the ODS
    public class AdminController
    {

        #region Queries
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
               // return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //Query syntax
                var results = from item in context.SpecialEvents orderby item.Description select item;
                return results.ToList();

            }
            
        }


        //the user has to choose the method
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Reservation> GetReservationsByEventCode(string eventcode)
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            //if "using" does not finish, this transaction will rollback
            using (var context = new eRestaurantContext())
            {
                // method syntax
                // return context.SpecialEvents.OrderBy(x => x.Description).ToList();
           

                //Query syntax
                var results = from item in context.Reservations 
                              where item.EventCode.Equals(eventcode)
                              orderby item.CustomerName,item.ReservationDate
                              select item;
                return results.ToList();

            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReservationsByDate> GetReservationsByDate(string reservationdate)
        {
            //use a transaction
            using (var context = new eRestaurantContext())
            {
                 //Linq is not very playful or cooperative with DateTime
                 //Extract the year month and day ourselves out of the passed parameter value

                int theYear = (DateTime.Parse(reservationdate)).Year;
                int theMonth = (DateTime.Parse(reservationdate)).Month;
                int theDay = (DateTime.Parse(reservationdate)).Day;

                var results = from eventitem in context.SpecialEvents
                              orderby eventitem.Description
                              select new ReservationsByDate() // a new instance for each special event row for the table
                              {
                                  Description = eventitem.Description,
                                  Reservations = from row in eventitem.Reservations
                                                 where row.ReservationDate.Year == theYear
                                                 && row.ReservationDate.Month == theMonth
                                                 && row.ReservationDate.Day == theDay
                                                 select new ReservationDetail() 
                                                 // is a new instance for each reservation
                                                 // of a particular specialevent code
                                                 {
                                                     CustomerName = row.CustomerName,
                                                     ReservationDate = row.ReservationDate,
                                                     NumberInParty = row.NumberInParty,
                                                     ContactPhone = row.ContactPhone,
                                                     ReservationStatus = row.ReservationStatus
                                                 }
                              };

                return results.ToList();
            }
        }



        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryItems> MenuCategoyItems_List()
        {
            //use a transaction
            using (var context = new eRestaurantContext())
            {
               
                var results = from menuitem in context.MenuCategories
                              orderby menuitem.Description
                              select new MenuCategoryItems() // a new instance for each special event row for the table
                              {
                                  Description = menuitem.Description,
                                  MenuItems = from row in menuitem.MenuItems
                                                 select new MenuItem()
                                                 {
                                                     Description = row.Description,
                                                     Price = row.CurrentPrice,
                                                     Calories = row.Calories,
                                                     Comment = row.Comment
                                                 }
                              };

                return results.ToList();
            }
        }

        /// <summary>
        /// List of Waiter
        /// </summary>
        /// <param name="item"></param>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Waiter> Waiter_List()
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            //if "using" does not finish, this transaction will rollback
            using (var context = new eRestaurantContext())
            {
                // method syntax
                // return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //Query syntax
                var results = from item in context.Waiters orderby 
                                  item.LastName, item.FirstName select item;
                return results.ToList();

            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Waiter GetWaiterByID( int waiterid)
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            //if "using" does not finish, this transaction will rollback
            using (var context = new eRestaurantContext())
            {
                // method syntax
                // return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //Query syntax
                var results = from item in context.Waiters
                              where item.WaiterID == waiterid
                              select item;

                // returns one row at most
                return results.FirstOrDefault();

            }

        }
        #endregion


        #region Add, Update, Delete of CRUD for CQRS
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void SpecialEvents_Add(SpecialEvent item)
        { 
            using (eRestaurantContext context = new eRestaurantContext())
            {
                // these methods are executed using an instance level item
                // setup an instance pointer and initialize to null
                SpecialEvent added = null;

                //setup the command to execute the add
                added = context.SpecialEvents.Add(item);
               
                // the command is not executed until it is actually saved
                context.SaveChanges();
            }
            
        }


        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void SpecialEvents_Update(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                // indicate the updating item instance
                //alter the modified status flag for this instance
                context.Entry<SpecialEvent>(context.SpecialEvents.Attach(item)).State = 
                    System.Data.Entity.EntityState.Modified;

                // the command is not executed until it is actually saved
                context.SaveChanges();
            }

        }


        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SpecialEvents_Delete(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //lookup the instance and record if found
                SpecialEvent existing = context.SpecialEvents.Find(item.EventCode);

                //if the record is found then remove the instance
                context.SpecialEvents.Remove(existing);

                // the command is not executed until it is actually saved
                context.SaveChanges();
            }

        }



        //Waiter
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Waiters_Add(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                // these methods are executed using an instance level item
                // setup an instance pointer and initialize to null
                Waiter added = null;

                //setup the command to execute the add
                added = context.Waiters.Add(item);

                // the command is not executed until it is actually saved
                context.SaveChanges();

                //the Waiter instance added contains the newly inserted record to sql
                //including the generated pkey value
                return added.WaiterID;
            }

        }


        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Waiters_Update(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                // indicate the updating item instance
                //alter the modified status flag for this instance
                context.Entry<Waiter>(context.Waiters.Attach(item)).State =
                    System.Data.Entity.EntityState.Modified;

                // the command is not executed until it is actually saved
                context.SaveChanges();
            }

        }


        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Waiter_Delete(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //lookup the instance and record if found
                SpecialEvent existing = context.SpecialEvents.Find(item.WaiterID);

                //if the record is found then remove the instance
                context.SpecialEvents.Remove(existing);

                // the command is not executed until it is actually saved
                context.SaveChanges();
            }

        }
        #endregion
    }
}//eof namespace
