using System;
using Microsoft.Owin.Hosting;
using HelloWorld;

namespace MySimpleHost
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (WebApp.Start<Startup>("http://localhost:5000")) {
				Console.WriteLine (
					"Server ready... Pres Enter to quit.");

				Console.ReadLine ();
			}
		}
	}
}
