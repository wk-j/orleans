using System;

namespace Client {
    class Program {
        static void Main(string[] args) {
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
                var sensor = client.GetGrain<ITemperatureSensorGrain>(123);
            }

        }
    }
}
