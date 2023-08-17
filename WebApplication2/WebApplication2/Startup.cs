using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MyApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapGet("/", async context =>
                {
                    var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
                    await context.Response.WriteAsync($"<h1>{appSettings.ApplicationName}</h1>");
                    await context.Response.WriteAsync($"<p>{appSettings.WelcomeMessage}</p>");
                    await context.Response.WriteAsync($"<p>Contact: {appSettings.ContactEmail}</p>");
                    await context.Response.WriteAsync($"<p>Features:</p>");
                    await context.Response.WriteAsync($"<ul>");
                    await context.Response.WriteAsync($"<li>Feature 1: {appSettings.Features.Feature1}</li>");
                    await context.Response.WriteAsync($"<li>Feature 2: {appSettings.Features.Feature2}</li>");
                    await context.Response.WriteAsync($"</ul>");
                    await context.Response.WriteAsync($"<p>Colors:</p>");
                    await context.Response.WriteAsync($"<ul>");
                    foreach (var color in appSettings.Colors)
                    {
                        await context.Response.WriteAsync($"<li>{color}</li>");
                    }
                    await context.Response.WriteAsync($"</ul>");
                });
            });
        }
    }

    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public string WelcomeMessage { get; set; }
        public string ContactEmail { get; set; }
        public Features Features { get; set; }
        public string[] Colors { get; set; }
    }

    public class Features
    {
        public bool Feature1 { get; set; }
        public bool Feature2 { get; set; }
    }
}
