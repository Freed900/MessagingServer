using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async context =>
            {
                if (context.Request.Method == "POST")
                {
                    if (context.Request.Query.ContainsKey("GetMessages"))
                    {
                        StringBuilder msgs = new StringBuilder();
                        for (int i = messages.Count - 1, t = 5; i >= 0 && t > 0; i--, t--)
                        {
                            msgs.Append(messages[i]).Append("<br>");
                        }
                        await context.Response.WriteAsync(msgs.ToString());
                    }
                    else if (context.Request.Query.ContainsKey("NewMessage"))
                        messages.Add(context.Request.Query["NewMessage"]);


                }
                else if (context.Request.Method == "GET")
                {
                    if (context.Request.Headers.ContainsKey("GetMessages"))
                    {
                        StringBuilder msgs = new StringBuilder();
                        for (int i = messages.Count - 1, t = 5; i >= 0 && t > 0; i--, t--)
                        {
                            msgs.Append(messages[i]).Append("<br>");
                        }
                        Console.WriteLine("sending: "+msgs.ToString());
                        await context.Response.WriteAsync(msgs.ToString());
                    }
                    else if (context.Request.Headers.ContainsKey("NewMessage"))
                    {
                        messages.Add(context.Request.Headers["NewMessage"]);
                        Console.WriteLine("New message: " + context.Request.Headers["NewMessage"]);
                    }

                }

            });
        }

        List<string> messages = new List<string>();

    }
}
