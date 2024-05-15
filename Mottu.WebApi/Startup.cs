using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Desafio.Application.Common.Models;
using Desafio.Application.DependencyInjection;
using Desafio.Infrastructure.Context;
using Desafio.Infrastructure.DependencyInjection;
using Desafio.WebApi.Filters;
using System.Text;

namespace Desafio.WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) =>
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

        services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
        services.Configure<Authentication>(Configuration.GetSection("Authentication"));
        services.Configure<AWS>(Configuration.GetSection("AWS"));

        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.EnableSensitiveDataLogging();
        });

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();


        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Desafio.Backend",
                Description = ".NET Core 8.0 Web API"
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                var paramsValidation = x.TokenValidationParameters;
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

        services.AddHttpContextAccessor();

        services.AddAuthorization(auth =>
        {
            auth.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            auth.AddPolicy("DeliveryMan", policy => policy.RequireRole("DeliveryMan"));
            auth.AddPolicy("All", policy => policy.RequireRole("Admin", "DeliveryMan"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio v1"));
        app.UseDeveloperExceptionPage();

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();

        app.UseRequestLocalization();

        app.UseRouting();
        app.UseCors("AllowAllOrigins");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
        });
    }
}
