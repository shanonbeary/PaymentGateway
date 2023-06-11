using Checkout.AcquiringBank.Client;
using Checkout.PaymentGateway.Repository;
using Checkout.PaymentGateway.Service;
using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add builder.Services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DatabaseContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IPaymentRepository, PaymentSqlRepository>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddHttpClient<IAcquiringBankClient, AcquiringBankClient>(client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("AcquiringBankUrl"));
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout Payment Gateway Api");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}