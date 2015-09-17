<Query Kind="Expression">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//orderby
from x in Items orderby x.Description select x

//also available descending
from food in Items orderby food.CurrentPrice descending select food
//or in method syntax
Items.OrderByDescending (food => food.CurrentPrice)

from food in Items orderby food.CurrentPrice descending,food.Calories ascending select food
//or in method syntax
Items.OrderByDescending (food => food.CurrentPrice).ThenBy(food => food.Calories)


//you can use where and orderby together
// order of wher and other clauses can interchange
from food in Items orderby food.CurrentPrice descending, food.Calories ascending where food.MenuCategory.Description.Equals("Entree") select food
//or this
from food in Items where food.MenuCategory.Description.Equals("Entree") orderby food.CurrentPrice descending, food.Calories ascending select food
//or in method syntax
Items.OrderByDescending (food => food.CurrentPrice).ThenBy (food => food.Calories).Where (food => food.MenuCategory.Description.Equals ("Entree"))
