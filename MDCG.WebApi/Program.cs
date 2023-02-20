using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;

namespace MDCG.WebApi
{
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MDCGDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MDCGSqlServer") ?? throw new InvalidOperationException("Connection string 'MDCGSqlServer' not found.")));

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddLogging();
            builder.Logging.AddSimpleConsole(options => {
                options.IncludeScopes = false;
                options.SingleLine = true;
                options.ColorBehavior = LoggerColorBehavior.Enabled;
                options.UseUtcTimestamp = true;
                options.TimestampFormat = "hh:mm:ss:ff  ";
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            builder.Services.AddScoped(typeof(IDataValidationService<>), typeof(PassThroughDataValidationService<>));

            builder.Services.AddScoped<IDataManagementService<User> ,UserService>();
            builder.Services.AddScoped<IDataManagementService<FxSpotMarketData>, FxSpotMarketDataService>();
            builder.Services.AddScoped<IDataManagementService<EquitySpotMarketData>, EquitySpotMarketDataService>();
            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
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