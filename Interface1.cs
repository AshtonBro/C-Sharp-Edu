using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshtonBro.CodeBlog._2
{
	public class MyInterfaces
	{
		public interface IMyInterfaces
		{
			string MyFunction();

			string TestAttributeFunction();
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
}
