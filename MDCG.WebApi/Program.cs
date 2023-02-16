using MDCG.WebApi.Data;
using MDCG.WebApi.Models;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
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

            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IDataManagementService<User> ,UserService>();
            builder.Services.AddScoped<IDataValidationService<User>, PassThroughDataValidationService<User>>();

            builder.Services.AddScoped<IRepository<FxSpotMarketData>, FxSpotMarketDataRepository>();
            builder.Services.AddScoped<IDataManagementService<FxSpotMarketData>, FxSpotMarketDataService>();
            builder.Services.AddScoped<IDataValidationService<FxSpotMarketData>, PassThroughDataValidationService<FxSpotMarketData>>();

            builder.Services.AddScoped<IRepository<EquitySpotMarketData>, EquitySpotMarketDataRepository>();
            builder.Services.AddScoped<IDataManagementService<EquitySpotMarketData>, EquitySpotMarketDataService>();
            builder.Services.AddScoped<IDataValidationService<EquitySpotMarketData>, PassThroughDataValidationService<EquitySpotMarketData>>();

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