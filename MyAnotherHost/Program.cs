using System;
using Microsoft.Owin.Hosting;

namespace MyAnotherHost
{
	class Program
	{
		public static void Main (string[] args)
		{
			using (WebApp.Start<Startup> ("http://localhost:5000")) {
				Console.WriteLine ("Server ready.. Press Enter to quit stuff.");

				Console.ReadLine ();
			}
		}
	}
}
