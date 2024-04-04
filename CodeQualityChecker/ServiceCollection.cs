using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CodeQualityChecker;

public static class ServiceCollection
{
    public static void AddServices(IServiceCollection serviceCollection, string settingsFile)
    {
        var settingsContent = File.ReadAllText(settingsFile);
        var settings = JsonConvert.DeserializeObject<Settings>(settingsContent);

        if (settings == null)
        {
            throw new Exception($"Failed to parse settings file '{settingsFile}'");
        }

        serviceCollection.AddSingleton(settings);
        serviceCollection.AddSingleton<Checker>();
        serviceCollection.AddSingleton<ReportGenerator>();
    }
}