using CrawlerProventos.Console.Configs;
using CrawlerProventos.Core.Interfaces;
using CrawlerProventos.Core.Services.CrawlerProventosUseCases;
using CrawlerProventos.Core.Services.ProventoUseCases;
using CrawlerProventos.Infrastructure.Contexts;
using CrawlerProventos.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CrawlerProventos.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IHostedService, ServiceProventosHandler>();
                    //Infrastructure
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseMySql(AppConfig.MySqlConnectionString,
                        options => options.EnableRetryOnFailure())
                    );

                    //Services
                    services.AddMediatR(typeof(CriarProventosHandler));
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
