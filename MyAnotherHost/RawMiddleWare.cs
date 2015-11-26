using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Owin;

namespace MyAnotherHost
{
	using AppFunc = Func<IDictionary<string, object>, Task>;

	public class RawMiddleWare
	{
		private readonly AppFunc next;
		private readonly GreetingOptions options;

		public RawMiddleWare(AppFunc next, GreetingOptions options)
		{
			this.next = next;
			this.options = options;
		}

		public async Task Invoke(IDictionary<string, object> env)
		{
			IOwinContext context = new OwinContext (env);
			string message = this.options.Message;
			if (this.options.IsHtml) 
			{
				message = String.Format ("<h1>{0}</h1>", message);
			}
			byte[] bytes = Encoding.UTF8.GetBytes (message);
			await context.Response.WriteAsync (bytes);
			await this.next (env);
		}
	}
	public class GreetingOptions
	{
		public string Message { get; set; }
		public bool IsHtml { get; set; }
	}
}

