using System.IO;
using DatabaseLayout.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server;
using Services;

var builder = WebApplication.CreateBuilder(args);
const string myAllowSpecificOrigins = "_AllowSpecificOrigins";

ConfigBuilder();

ConfigServices();

ConfigCors();

RunApp();

void ConfigBuilder()
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false).Build();
#else
        .AddJsonFile("appsettings.json", optional: false).Build();
#endif
    builder.Configuration.AddConfiguration(config);
}

void ConfigServices()
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<PortTrackerContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddRepositories();
    builder.Services.AddServices();
    builder.Services.AddControllers();

    builder.Services.AddLogging();
}

void ConfigCors()
{
    builder.Services.AddCors(options =>
        {
            options.AddPolicy(myAllowSpecificOrigins, policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        }
    );
}

void RunApp()
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors(myAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
