<Query Kind="Statements">
  <Connection>
    <ID>a8999fca-8c48-4af5-98db-1a0f0dfd7ce3</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//simplest form for dumping entity
Waiters

//simple query syntax
from person in Waiters select person

//simple method syntax
Waiters.Select(person =>person)
Waiters.Select(x =>x)

//inside our project we will be writing C# statements
var results = from person in Waiters select person;
//to display the contents of a variable in LinqPad use the .Dump()
results.Dump();

/*//impleted inside VS project class libray BLL method
[DataObjectMethod(DataObjectMethodType.Select,false)]
public List<Waiters> SomeMethodName()
{
 // you will need to connect to your DAL object
 // this will be done using a new xxxx() constructor
 // assume your connection variable is called contextVariable
 
 // do your query
 	var results = from person in contextVariable.Waiters select person;
	return results.ToList();
}*/




