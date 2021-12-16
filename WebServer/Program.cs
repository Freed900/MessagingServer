using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WebServer;

BuildWebHost(args).Run();

static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();