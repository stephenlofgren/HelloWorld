using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyAnotherHost.Startup))]
namespace MyAnotherHost
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.Use<MachineNamingMiddleware> ();

			app.Use (
				async (IOwinContext context, Func<Task> next) => {
					//Simulate File Not Fount
					context.Response.StatusCode = 404;
					await next.Invoke ();
				});

			app.Run (async (IOwinContext arg) => {
				var bytes = System.Text.Encoding.UTF8.GetBytes("<h1>Hello Universe</h1>");

				arg.Response.ContentLength = bytes.Length;

				await arg.Response.WriteAsync(bytes);
			});

			app.Use<RawMiddleWare>(new GreetingOptions(){
				Message = "Hello from ImprovedMiddleware",
				IsHtml = true
			});

			app.Map ("/planets", helloApp => {

				helloApp.Map("/3", helloEarth =>  {
					helloEarth.Run(async (IOwinContext arg) => {
						await arg.Response.WriteAsync("<h1>Hello Earth</h1>");
					});
				});

				helloApp.MapWhen(context => {
					if (context.Request.Path.HasValue)
					{
						int position;

						if(Int32.TryParse(context.Request.Path.Value.Trim('/'), out position))
						{
							if(position > 8)
							{
								return true;
							}
						}
					}
					return false;
				}, helloPluto => {
					helloPluto.Run(async (IOwinContext context) => {
						await context.Response.WriteAsync("<h1>Oops! We are out of Solar System</h1>");
					});
				});

				helloApp.Use (async(IOwinContext context, Func<Task> next) => {
					await context.Response.WriteAsync ("<h1>Hello Mercury</h1>");
					await next.Invoke ();
					await context.Response.WriteAsync ("<h1>Hello Mercury on return</h1>");
				});

				helloApp.Run (async (IOwinContext context) => {
					await context.Response.WriteAsync ("<h1>Hello Neptune</h1>");
				});

				app.Run (async (IOwinContext context) => {
					await context.Response.WriteAsync("<h1>Hello Universe</h1>");
				});
			});
		}
	}
}

