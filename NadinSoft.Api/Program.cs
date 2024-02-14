
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using NadinSoft.Application;
using NadinSoft.Application.Contracts;
using NadinSoft.Domain;
using NadinSoft.EntityFrameworkCore;
using NadinSoft.EntityFrameworkCore.Repositories;

using System;

namespace NadinSoft.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        // Add services to the container.
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //todo: docker sql server
        services.AddDbContext<NadinSoftDbContext>(options =>
                                    options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddAutoMapper(typeof(ProductAutoMapperProfile));

        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddTransient<ProductManager>();
        services.AddTransient<IProductService, ProductService>();

        var app = builder.Build();

        await MigrateDatabase(app.Services);

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

    private static async Task MigrateDatabase(IServiceProvider service)
    {
        using (var serviceScope = service.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<NadinSoftDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
