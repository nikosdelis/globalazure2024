// Send some telemetry to Azure IoT Hub
// This code is for example purposes only. 
// It is recommended to review the code in the context of the actual application
// and modify it as needed.

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace SimulatedDevice
{
    class Program
    {
        private static DeviceClient s_deviceClient;
        private static readonly string s_connectionString = Environment.GetEnvironmentVariable("IOTHUB_DEVICE_CONNECTION_STRING")!;

        private static async Task SendDeviceToCloudMessagesAsync()
        {
            double minTemperature = 20;
            Random rand = new Random();

            while (true)
            {
                double currentTemperature = rand.NextDouble() * 2 + minTemperature;

                string messageBody = $"{{\"Timestamp\":\"{DateTime.UtcNow.ToString()}\",\"Temperature\":\"{currentTemperature.ToString("N2").Replace(",", ".")}\"}}";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                await s_deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageBody);

                await Task.Delay(5000);
            }
        }

        private static async Task Main(string[] args)
        {
            Console.WriteLine("IoT Hub Quickstarts - Simulated device. Ctrl-C to exit.\n");

            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);

            await SendDeviceToCloudMessagesAsync();
        }
    }
}