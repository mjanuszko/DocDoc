using Serilog;

namespace DocDocs.WebApi
{
    public class Program
    {
        private const string AppName = "DocDocs.WebApi";

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", AppName)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Host.UseSerilog((context, services, configuration) => configuration
                .Enrich.WithProperty("Application", AppName)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext());

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
