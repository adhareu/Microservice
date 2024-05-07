using MassTransit;
using Play.Catalog.Service.Entities;

using Play.Common.MassTransit;
using Play.Common.MongoDB;
using Play.Common.Settings;

namespace Play.Catalog.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            builder.Services
                .AddMongo()
                .AddMongoRepository<Item>("items")
                .AddMassTransitWithRabbitMq();
            //Enable CORS//Cross site resource sharing
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    b => b.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });

            builder.Services.AddControllers(options => {
                options.SuppressAsyncSuffixInActionNames =false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
          
           var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");


            app.MapControllers();

            app.Run();
        }
    }
}
