using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Server {

    public interface ITemperatureSensorGrain : IGrainWithIntegerKey {
        Task SubmitTemperatureAsync(float temperature);
    }

    public class TemperatureSensorGrain : Grain, ITemperatureSensorGrain {
        public Task SubmitTemperatureAsync(float temperature) {
            var grainId = this.GetPrimaryKeyLong();
            Console.WriteLine($"{grainId} received temperature: {temperature}");
            return Task.CompletedTask;
        }
    }


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