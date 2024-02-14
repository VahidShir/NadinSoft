
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using NadinSoft.Application;
using NadinSoft.Application.Commands;
using NadinSoft.Application.Contracts;
using NadinSoft.Domain;
using NadinSoft.EntityFrameworkCore;
using NadinSoft.EntityFrameworkCore.Repositories;

using System.Text;

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

        builder.Services.AddDateOnlyTimeOnlyStringConverters();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Nadin Soft", Version = "v1" });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Add bearer token in the field",
                Name = "Authhentication",
                Type = SecuritySchemeType.ApiKey
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement {

                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{ }
                }
            });
        });

        services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

        //todo: docker sql server
        services.AddDbContext<NadinSoftDbContext>(options =>
                                    options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<NadinSoftDbContext>();


        var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
        var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = apiSettings.ValidAudience,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAutoMapper(typeof(ProductAutoMapperProfile));

        builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

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

        app.UseAuthentication();
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
