using Device.ConsoleApp;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System.Text;

await Task.Delay(5000);

DeviceClient deviceClient;

var data = new RegisterDeviceRequest
{
    DeviceId = "console_1",
    DeviceType = "console app"
};

using var client = new HttpClient();
var response = await client.PostAsync("http://localhost:7198/api/RegisterDevice", new StringContent(JsonConvert.SerializeObject(data)));
var connnectionString = await response.Content.ReadAsStringAsync();
if (!string.IsNullOrEmpty(connnectionString))
{

    //Connecting to IoT Hub 
    deviceClient = DeviceClient.CreateFromConnectionString(connnectionString);

    //Updating device twin properties
    var twin = new TwinCollection();
    twin["deviceType"] = data.DeviceType;
    await deviceClient.UpdateReportedPropertiesAsync(twin);

    while(true)
    {
        //execute event
        var message = JsonConvert.SerializeObject(new { time = DateTime.Now.ToString() });
        await deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes(message)));

        Console.Write($"Sent: {message}"); 
        await Task.Delay(100000);

    }

}


Console.ReadKey();