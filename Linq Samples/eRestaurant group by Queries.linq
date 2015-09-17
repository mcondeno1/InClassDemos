<Query Kind="Expression">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//groupby
from food in Items group food by food.MenuCategory.Description

//this creates a key with a vale with the row collection for that key value

//more than one field
from food in Items 
group food by new {food.MenuCategory.Description, food.CurrentPrice}