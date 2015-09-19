<Query Kind="Program">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//change this to C#Program in able to work
void Main()
{
//	//anonymous data type queries
//	from food in Items 
//	where food.MenuCategory.Description.Equals("Entree")
//	&& food.Active
//	orderby food.CurrentPrice descending
//	select new
//	{ Description = food.Description,
//	  Price = food.CurrentPrice, 
//	  Cost = food.CurrentCost,
//	  Profit = food.CurrentPrice - food.CurrentCost
//	}
//	
//	
//	// this also works but without the calculation
//	from food in Items 
//	where food.MenuCategory.Description.Equals("Entree")
//	&& food.Active
//	orderby food.CurrentPrice descending
//	select new
//	{ food.Description,
//	 food.CurrentPrice, 
//	   food.CurrentCost,
//	// (food.CurrentPrice - food.CurrentCost)
//	}
//	
//	
//	
//	///this should work in VS
//	from food in Items 
//	where food.MenuCategory.Description.Equals("Entree")
//	&& food.Active
//	orderby food.CurrentPrice descending
//	select new POCOObjectName
//	{ Description = food.Description,
//	  Price = food.CurrentPrice, 
//	  Cost = food.CurrentCost,
//	  Profit = food.CurrentPrice - food.CurrentCost
//	}
//	
	
	
	var results = from food in Items 
	where food.MenuCategory.Description.Equals("Entree")
	&& food.Active
	orderby food.CurrentPrice descending
	select new FoodMargins()
	{ Description = food.Description,
	  Price = food.CurrentPrice, 
	  Cost = food.CurrentCost,
	  Profit = food.CurrentPrice - food.CurrentCost
	};
	
	results.Dump();
	//get all the bills and bill items for waiters in September of 2014.
	//get only those bills which were paid
	
	var results2 = from orders in Bills where orders.PaidStatus &&  (orders.BillDate.Month == 9 &&
	orders.BillDate.Year == 2014) 
	orderby orders.Waiter.LastName, orders.Waiter.FirstName
	select new 
	{
	   BillID = orders.BillID,
	   WaiterName = orders.Waiter.LastName + ", " + orders.Waiter.FirstName,
	   Orders = orders.BillItems
	};
	
	results2.Dump();
}

// Define other methods and classes here

//This is a PCO class
public class FoodMargins
{
	public string Description {get;set;}
	public decimal Price {get;set;}
	public decimal Cost {get;set;}
	public decimal Profit {get;set;}
}

//this is a DTO class
public class BillOrders
{
  public int BillID{get;set;}
  public string WaiterName{get;set;}
  public BillItems Orders {get;set;}
}