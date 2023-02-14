using MDCG.WebApi.Data;
using MDCG.WebApi.Repository;
using MDCG.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace MDCG.WebApi {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MDCGDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MDCGDbContext") ?? throw new InvalidOperationException("Connection string 'MDCGDbContext' not found.")));

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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