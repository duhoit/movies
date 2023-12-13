using System.IO;
using backend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using backend.Repository;
internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        Configure(app, builder.Environment);

        app.Run();
    }
    private static void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Register MovieRepository as IMovieRepository
    services.AddScoped<IMovieRepository, MovieRepository>();

    // If MovieRepo is an implementation of IMovieRepository
    // services.AddScoped<IMovieRepository, MovieRepo>();

    // You can remove the following line, as controllers are typically created by the framework
    services.AddScoped<MovieController>();
}

    private static void Configure(WebApplication app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();
    }

    private static string GetConnectionString(string connectionStringName = "Default")
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var config = builder.Build();
        return config.GetConnectionString(connectionStringName);
    }
}
