using Newtonsoft.Json;

namespace Sharpidious;

public class Settings
{
    public Dictionary<string, object> Rules { get; set; } = [];
    public List<string> IgnoredClassNames { get; set; } = [];
    [JsonProperty("rules.override")]
    public Dictionary<string, Dictionary<string, object>> OverrideRules { get; } = [];
}