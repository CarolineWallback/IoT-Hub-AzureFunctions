using AzureFunctions.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(x =>
    {
        x.AddDbContext<DataContext>(x => x.UseCosmos(Environment.GetEnvironmentVariable("CosmosDbConnectionString")!, Environment.GetEnvironmentVariable("CosmosDbName")!));
    })
    .Build();

host.Run();
