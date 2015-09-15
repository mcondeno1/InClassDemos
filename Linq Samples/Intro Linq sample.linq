<Query Kind="Program" />

void Main()
{
	// simple contcatenation
	//"hello world"
	//int[] numbers = new int[3] {0,1,2};
	//var numQuery= from num in numbers where (num%2) == 0 select num;
	
	//simple C# statements
	int[] numbers = new int[3] {0,1,2};
	var numQuery= from num in numbers where (num%2) == 0 select num;
	foreach (int num in numQuery)
	{
	 num.ToString().Dump();
	}
	
	
	//subroutine call
	SayHello("Matt");
}

//Define other methods and classes here
public void SayHello(string name)
{
  string message = "hello " + name;
  message.Dump();
}

// Define other methods and classes here
