using System.Diagnostics;
using CodeQualityChecker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var sw = Stopwatch.StartNew();
var directory = args.Length == 0 ? Console.ReadLine() : args[0];
var settingsFile = $"{directory}/sharpidious.json";

if (!File.Exists(settingsFile))
{
    throw new Exception($"Couldn't find settings file in directory '{directory}'");
}

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, collection) => CodeQualityChecker.ServiceCollection.AddServices(collection, settingsFile))
    .UseSerilog((hostContext, _, logger) => logger.MinimumLevel.Debug().WriteTo.Console()).Build();

var generator = host.Services.GetRequiredService<ReportGenerator>();
var checker = host.Services.GetRequiredService<Checker>();
var reports = await generator.GetReportsForDirectoryAsync(directory);
var suggestions = await checker.GetSuggestionsAsync(reports);

foreach (var suggestion in suggestions)
{
    Log.Logger.Warning(suggestion);
}

sw.Stop();

Log.Logger.Debug($"Finished in {sw.Elapsed.TotalMilliseconds}ms with {suggestions.Count} suggestions");
Environment.Exit(suggestions.Count);