using System;
using Orleans;
using Orleans.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Client {
    public interface ITemperatureSensorGrain : IGrainWithIntegerKey {
        Task SubmitTemperatureAsync(float temperature);
    }

    class Program {
        static async System.Threading.Tasks.Task Main(string[] args) {
            var clientBuilder =
                  new ClientBuilder()
                      .UseLocalhostClustering()
                      .Configure<ClusterOptions>(options => {
                          options.ClusterId = "dev";
                          options.ServiceId = "Orleans2GettingStarted";
                      })
                      .ConfigureLogging(logger => logger.AddConsole());

            using (var client = clientBuilder.Build()) {
                await client.Connect();
                var random = new Random();
                var sky = "blue";
                while (sky == "blue") {
                    var grainId = random.Next(0, 500);
                    var temperature = random.NextDouble() * 40;
                    var sensor = client.GetGrain<ITemperatureSensorGrain>(grainId);
                    await sensor.SubmitTemperatureAsync((float)temperature);
                }
            }

        }
    }
}
