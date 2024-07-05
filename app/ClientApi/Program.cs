using ClientApi.Core;
using ClientApi.Helpers;
using ClientApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net;
using System.Text;

namespace ClientApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Client Api";

            var builder = WebApplication.CreateBuilder(args);
            var apiUrl = builder.Configuration["ApiUrl"];
            var appVersion = builder.Configuration["AppVersion"];
            var apiUrlNewSpot = builder.Configuration["NewSpotApiUrl"];
            var serverHost = builder.Configuration["ServerHost"];

            builder.Host.UseSerilog((hst, cnf) =>
            {
                cnf.ReadFrom.Configuration(hst.Configuration);
                cnf.Enrich.FromLogContext();
                cnf.Enrich.WithProperty("ApplicationName", hst.HostingEnvironment.ApplicationName);
                cnf.MinimumLevel.Debug();
                cnf.WriteTo.Console();
                cnf.WriteTo.File("Logs/api.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);
            });

            builder.Services.AddSingleton<CryptographyHelper>();

            builder.Services.AddSingleton((a) =>
            {
                var service = new SpotService(apiUrl, a.GetRequiredService<ILogger<SpotService>>());                
                return service;
            });

            builder.Services.AddSingleton((a) =>
            {
                var service = new AmqpService(a.GetRequiredService<ILogger<AmqpService>>());
                service.CreateConnection();
                return service;
            });

            builder.Services.AddSingleton((a) =>
            {
                var service = new NewSpotService(apiUrlNewSpot);
                return service;
            });

            builder.Services.AddControllers();

            var key = Encoding.ASCII.GetBytes(AppSettings.JwtKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            builder.Services.AddAuthorization();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc($"v{appVersion}", new OpenApiInfo
                    {
                        Title = $"Client Api",
                        Description = "Legacy Spot Trading Platform integration API"
                    });

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                });
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.SetIsOriginAllowed(a => true).AllowAnyMethod().AllowAnyHeader()
                        .AllowCredentials());
            });

            var app = builder.Build();

            if (app.Environment.IsProduction())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/v{appVersion}/swagger.json", $"v{appVersion}");
                    options.RoutePrefix = "swagger";
                    options.DocumentTitle = "Client Api";
                    options.DisplayRequestDuration();
                    options.EnableTryItOutByDefault();
                    options.DefaultModelsExpandDepth(-1);
                    options.DocExpansion(DocExpansion.None);
                });
            }
            else
            {
                app.MapGet("/", async context =>
                {
                    var header = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                    if (!IPAddress.TryParse(header, out var ip))
                    {
                        ip = context.Connection.RemoteIpAddress;
                    }

                    var sb = new StringBuilder();
                    sb.AppendLine("Server IP: " + serverHost);
                    sb.AppendLine("Client IP: " + ip.ToString());
                    sb.AppendLine("AppVersion: " + appVersion);

                    context.Response.ContentType = "text/plan";
                    context.Response.StatusCode = 200;

                    await context.Response.WriteAsync(sb.ToString());
                });
            }

            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
