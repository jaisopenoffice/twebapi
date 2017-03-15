using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace tWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseApplicationInsightsRequestTelemetry();

            //app.UseApplicationInsightsExceptionTelemetry();
            var context = app.ApplicationServices.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc();
        }

        private static void AddTestData(ApiContext context)
        {
            var testUser1 = new Models.User
            {
                Id = "US-133-223213",
                FirstName = "Mark",
                LastName = "Walter"
            };

            context.Users.Add(testUser1);

            var testArticle1 = new Models.Article
            {
                Id = "AR-444-223599",
                UserId = testUser1.Id,
                Content = "What a piece of book."
            };
            var testArticle2 = new Models.Article
            {
                Id = "AR-444-342231",
                UserId = testUser1.Id,
                Content = "Truely amazing Streets of Melbourne"
            };

            context.Articles.Add(testArticle1);
            context.Articles.Add(testArticle2);

            context.SaveChanges();
        }
    }
}
