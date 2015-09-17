<Query Kind="Expression">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//where clause
Tables

// list all tables that hold more than 3 people
// query syntax
from row in Tables where row.Capacity > 3 select row

//method syntax
Tables.Select(row =>row.Capacity >3)

Tables.Where (row => row.Capacity > 3)

//list all items more than 500 calories
from food in Items where food.Calories > 500 select food
Items.Where(row =>row.Calories > 500)

//list all items with more than 500 calories and selling for more than $10.00
//indicate m for decimal/double or float
Items.Where(row =>(row.Calories > 500 && row.CurrentPrice > 10.00m))
from food in Items where food.Calories > 500 && food.CurrentPrice > 10.00m select food


//list all items with more than 500 calories and are Entrees on the menu
//HINT: Navigational properties of the datase are known by Linqpad
//as long as the tables has a relationship
from food in Items where food.Calories > 500 && food.MenuCategory.Description.Equals("Entree") select food
Items.Where(food=>(food.Calories >500 && food.MenuCategory.Description.Equals("Entree")))