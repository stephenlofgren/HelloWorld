using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
 
[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
//			// Configured to run first
//			app.Use<MachineNamingMiddleware>();
//			 
//			app.Use(
//				async (IOwinContext context, Func<Task> next) =>
//				{
//					// Simulate File Not Found.
//					context.Response.StatusCode = 404;
//					await next.Invoke();
//				});
//			 
//			app.Run(async (IOwinContext context) =>
//				{
//					var bytes = System.Text.Encoding
//						.UTF8.GetBytes("<h1>Hello World</h1>");
//					var moreBytes = System.Text.Encoding
//						.UTF8.GetBytes("<h1>Hello Universe</h1>");
//					 
//					context.Response.ContentLength =
//						bytes.Length + moreBytes.Length;
//					 
//					// Add breakpoint on the line below.
//					await context.Response.WriteAsync(bytes);
//					 
//					await context.Response.WriteAsync(moreBytes);
//				});

			////Reading the request body from Middleware
//			app.Use<RequestReadingMiddleware>();
//
//			app.Run(async (IOwinContext context) =>
//				{
//					string body = String.Empty;
//					 
//					using (var reader = new StreamReader(
//						context.Request.Body))
//					{
//						body = await reader.ReadToEndAsync();
//					}
//					 
//					context.Response.ContentLength = System.Text
//						.Encoding.UTF8.GetByteCount(body);
//					 
//					await context.Response.WriteAsync(body);
//				});
//			

			////Reading Response body from Middleware

			app.Use<ResponseReadingMiddleware>();
			 
			app.Run(async (IOwinContext context) =>
				{
					var bytes = System.Text.Encoding
						.UTF8.GetBytes("<h1>Hello World</h1>");
					 
					context.Response.ContentLength = bytes.Length;
					 
					await context.Response.WriteAsync(bytes);
				});
		}
	}
}
