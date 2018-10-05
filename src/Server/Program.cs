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
                var clientBuilder = new ClientBuilder()
                   .UseLocalhostClustering()
                   .Configure<ClusterOptions>(options => {
                       options.ClusterId = "dev";
                       options.ServiceId = "Orleans2GettingStarted";
                   })
                   .ConfigureLogging(logging => logging.AddConsole());

                using (var client = clientBuilder.Build()) {
                    await client.Connect();

                    var random = new Random();
                    string sky = "blue";

                    while (sky == "blue") {
                        int grainId = random.Next(0, 500);
                        double temperature = random.NextDouble() * 40;
                        var sensor = client.GetGrain<ITemperatureSensorGrain>(grainId);
                        await sensor.SubmitTemperatureAsync((float)temperature);
                    }
                }
            }
        }
    }
}