using System.Interview.TinyUrl.ByHash.DataLayer;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace System.Interview.TinyUrl.ByHash;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        const string serviceName = "TinyUrlByHash";


        //todo: create Aspire.ServiceDefaults project in order to spin up telemetry instrumentation
        //https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/service-defaults

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.SetResourceBuilder(
                ResourceBuilder.CreateDefault().AddService(serviceName)).AddConsoleExporter();
        });

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing.AddAspNetCoreInstrumentation().AddConsoleExporter())
            .WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation().AddConsoleExporter());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.MapControllers();
        app.UseEndpoints(routeBuilder => routeBuilder.MapControllers());

        CreateTables();

        app.Run();
    }

    private static void CreateTables()
    {
        using (var dbContext = new PostgreDbContext())
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
