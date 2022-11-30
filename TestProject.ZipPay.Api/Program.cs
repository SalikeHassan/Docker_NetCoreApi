using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Api;
using TestProject.ZipPay.Infrastructure.Context;
using Serilog;
using TestProject.ZipPay.Api.CustomMiddleware;

namespace TestProject.Zip.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configuration of Serilog
            var logger = new LoggerConfiguration()
                                .ReadFrom
                                .Configuration(builder.Configuration)
                                .Enrich.FromLogContext()
                                .CreateLogger();

            builder.Logging.AddSerilog(logger);

            // Add services to the container.
            builder.Services.AddDbContext<ZipPayContext>(x => x.UseSqlServer(
                   builder.Configuration.GetConnectionString("ZipPayConnStr"),
                   sqlServerOptionsAction: sqlOptions =>
                   {
                       sqlOptions.EnableRetryOnFailure
                       (
                          maxRetryCount: 3,
                          maxRetryDelay: TimeSpan.FromSeconds(10),
                          errorNumbersToAdd: null
                       );
                   }
                ));

            //Register mediatR service for command classes
            builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            var serviceAssemble = AppDomain.CurrentDomain.Load("TestProject.ZipPay.Command");
            builder.Services.AddMediatR(serviceAssemble);

            //Register mediatR service for query classes
            var serviceAssembleQuery = AppDomain.CurrentDomain.Load("TestProject.ZipPay.Query");
            builder.Services.AddMediatR(serviceAssembleQuery);

            //Code for api versioning
            builder.Services.AddApiVersioning(opt =>
            {
                //Code to setup api versioning
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

            //Code to fix the swagger while using multiple version of api
            //Install package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
            builder.Services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware(typeof(ExceptionMiddleware));
      
            DatabaseManagementService.MigrationInitialisation(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}