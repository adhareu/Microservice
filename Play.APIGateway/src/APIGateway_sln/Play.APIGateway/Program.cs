
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
using Play.APIGateway.Config;
using System;
namespace Play.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var routesFolder = builder.Configuration["OcelotSwaggerRouteConfiguration:RoutesFolder"];
            builder.Configuration.AddOcelotWithSwaggerSupport(options =>
            {
                options.Folder = routesFolder;
            });
            builder.Services.AddSwaggerForOcelot(builder.Configuration);
            bool isEurekaActive = Convert.ToBoolean(builder.Configuration["Eureka:Active"]);
            if (isEurekaActive)
            {
                builder.Services.AddOcelot(builder.Configuration)
                    .AddCacheManager(x =>
                    {
                        x.WithDictionaryHandle();
                    })
                    .AddEureka()
                    .AddPolly();

                builder.Services.AddServiceDiscovery(o => o.UseEureka());
            }
            else
            {
                builder.Services.AddOcelot(builder.Configuration).AddPolly();
            }
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routesFolder, builder.Environment);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    b => b.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
               // app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseSwagger();

            app.UseAuthorization();
            app.UseSwaggerForOcelotUI(options =>
            {
                options.ReConfigureUpstreamSwaggerJson = AlterUpstream.AlterUpstreamSwaggerJson;
                options.PathToSwaggerGenerator = "/swagger/docs";
                

            }).UseOcelot().Wait();

            app.MapControllers();

            app.Run();
        }
    }
}
