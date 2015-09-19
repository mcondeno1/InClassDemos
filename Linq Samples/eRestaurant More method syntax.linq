<Query Kind="Expression">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>


(from theBill in BillItems
where theBill.BillID == 104
select theBill.SalePrice * theBill.Quantity).Sum()


BillItems
    .Where (theBill => theBill.BillID == 104)
    .Select(theBill => theBill.SalePrice * theBill.Quantity)
    .Sum()
	
	
	
(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum(theBill => theBill.SalePrice * theBill.Quantity)).Max()

(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum(theBill => theBill.SalePrice * theBill.Quantity)).Min()

//average pay bill
(from customer in Bills
where customer.PaidStatus == true
select customer.BillItems.Sum(theBill => theBill.SalePrice * theBill.Quantity)).Average()


//what is the average number of items per paid bill
//we need to get a list of numbers representing the items per bill
//we take an average of the list
(from customer in Bills where customer.PaidStatus
select customer.BillItems.Count()).Average()


Waiters.SingleOrDefault(person => person.FirstName.StartsWith("Z"))


(from people in Reservations
select people.CustomerName).Distinct()

Items.Take(4)
Items.AsEnumerable().TakeWhile(food=>food.MenuCategory.Description != "Entree")

from category in MenuCategories
where category.Items.Any(item => item.CurrentCost > 2.0m)
select category

Bills.Any(customer => !customer.PaidStatus)




(
from customer in Bills
where customer.ReservationID.HasValue
   && customer.PaidStatus == true
   && customer.BillDate.Year == 2014
   && customer.BillDate.Month == 10
   && customer.BillDate.Day > 23
select new
{
   BillId = customer.BillID,
   Total = customer.BillItems.Sum(item => item.Quantity * item.SalePrice),
   Type = "Reservation"
}
).Union(
from customer in Bills
where customer.TableID.HasValue
   && customer.PaidStatus == true
   && customer.BillDate.Year == 2014
   && customer.BillDate.Month == 10
   && customer.BillDate.Day > 22
select new
{
   BillId = customer.BillID,
   Total = customer.BillItems.Sum(item => item.Quantity * item.SalePrice),
   Type = "Walk-In"
}
)