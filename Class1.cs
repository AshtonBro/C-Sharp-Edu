using System;

public class MyInterfaces
{
	public interface IMyInterface
	{
		string MyFunction();
	}

	[AttributeUsage(AttributeTargets.All)]
	public class MyAttribute : Attribute
    {
		public MyAttribute()
		{ 
		}
		public MyAttribute(string data)
        {
			Data = data;
        }
		public string Data { get; set; }
    }

}
