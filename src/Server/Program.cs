using System;
using System.Net;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Server {
    class Program {
        static async System.Threading.Tasks.Task Main(string[] args) {
            var siloBuilder =
                new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options => {
                        options.ClusterId = "dev";
                        options.ServiceId = "Orleans2GettingStarted";
                    })
                    .Configure<EndpointOptions>(options => {
                        options.AdvertisedIPAddress = IPAddress.Loopback;
                    })
                    .ConfigureLogging(logger => logger.AddConsole());

            using (var host = siloBuilder.Build()) {
                await host.StartAsync();
                Console.ReadLine();
            }
        }
    }
}