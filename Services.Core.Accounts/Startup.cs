using System;
using HotChocolate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Core.Accounts.GraphQL.Extensions;
using Services.Core.Accounts.Services;

namespace Services.Core.Accounts
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.environment = env;
            
            Console.WriteLine($"================== {DateTime.UtcNow} ==================");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddTransient<IUserRepository, UserRepository>();
            
            services.AddHealthChecks();

            // Add GraphQL Services
            services
                .AddGraphQLServer()
                .OnSchemaError((context, ex) =>
                {
                    if (ex is SchemaException se)
                    {
                        System.Diagnostics.Debug.WriteLine(se.ToString());
                    }
                })
                .AddAccountTypes();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (this.environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseRouting();

            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello from Services.Core.Accounts! [{DateTime.UtcNow}]").ConfigureAwait(false);
                });
            });
        }
    }
}
