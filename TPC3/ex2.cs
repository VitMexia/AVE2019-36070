using System;

public class App{
	
	private static bool Foo(String msg){
		
		int l = msg.Length;
		
		if(l == 1)
			return true;
		
		if(msg[l-1] != msg[0])
			return false;
		
		if(l == 2)
			return true;
		
		return Foo(msg.Substring(1, l-2));
		
	}
	
	
}