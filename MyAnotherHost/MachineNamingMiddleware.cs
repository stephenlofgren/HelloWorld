using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Owin;

namespace MyAnotherHost
{
	using AppFunc = Func<IDictionary<string, object>, Task>;

	public class MachineNamingMiddleware
	{
		private readonly AppFunc next;

		public MachineNamingMiddleware (AppFunc next)
		{
			this.next = next;
		}

		public async Task Invoke(IDictionary<string, object> env)
		{
			await this.next (env);

			IOwinContext context = new OwinContext (env);
			if (context.Response.StatusCode >= 400) 
			{
				context.Response.Headers.Add ("X-Box", new[] { System.Environment.MachineName });
			}
		}
	}
}

