using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Sharpidious;
using ServiceCollection = Sharpidious.ServiceCollection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .MinimumLevel.Debug()
    .CreateLogger();

var directory = args.Length != 0 ? args[0] : "";

if (args.Length < 1)
{
    Log.Logger.Debug($"No directory specified, using '{Directory.GetCurrentDirectory()}'...");
    directory = Directory.GetCurrentDirectory();
}

var sw = Stopwatch.StartNew();
var settingsFile = $"{directory}/sharpidious.json";

if (!File.Exists(settingsFile))
{
   Log.Logger.Error($"Couldn't find settings file (sharpidious.json)");
   return;
}

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, collection) => ServiceCollection.AddServices(collection, settingsFile))
    .Build();

var generator = host.Services.GetRequiredService<ReportGenerator>();
var checker = host.Services.GetRequiredService<Checker>();
var reports = await generator.GetReportsForDirectoryAsync(directory);
var suggestions = await checker.GetSuggestionsAsync(reports);

foreach (var suggestion in suggestions)
{
    Log.Logger.Warning(suggestion);
} 

sw.Stop();

Log.Logger.Debug($"Done in {sw.Elapsed.TotalMilliseconds}ms, {suggestions.Count} suggestions");
Environment.Exit(suggestions.Count);