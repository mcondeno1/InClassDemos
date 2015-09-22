<Query Kind="Expression">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//anonymous data type queries
from food in Items 
where food.MenuCategory.Description.Equals("Entree")
&& food.Active
orderby food.CurrentPrice descending
select new
{ Description = food.Description,
  Price = food.CurrentPrice, 
  Cost = food.CurrentCost,
  Profit = food.CurrentPrice - food.CurrentCost
}


// this also works but without the calculation
from food in Items 
where food.MenuCategory.Description.Equals("Entree")
&& food.Active
orderby food.CurrentPrice descending
select new
{ food.Description,
 food.CurrentPrice, 
   food.CurrentCost,
// (food.CurrentPrice - food.CurrentCost)
}



///this should work in VS
from food in Items 
where food.MenuCategory.Description.Equals("Entree")
&& food.Active
orderby food.CurrentPrice descending
select new POCOObjectName
{ Description = food.Description,
  Price = food.CurrentPrice, 
  Cost = food.CurrentCost,
  Profit = food.CurrentPrice - food.CurrentCost
}