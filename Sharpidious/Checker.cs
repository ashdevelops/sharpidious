namespace Sharpidious;

public class Checker(Settings settings)
{
    public Task<List<string>> GetSuggestionsAsync(List<FileReport> reports)
    {
        var suggestions = new List<string>();
        
        foreach (var report in reports)
        {
            if (settings.IgnoredClassNames.Contains(report.Name))
            {
                continue;
            }

            var rules = settings.Rules;

            if (settings.OverrideRules.TryGetValue(report.Name, out var overrideRules))
            {
                rules = overrideRules;
            }
            
            if (rules.TryGetValue("class.maxNameLength", out var maxNameLength) && report.Name.Length > (int)(long) maxNameLength)
            {
                suggestions.Add($"Class {report.Name} has too long of a name");
            }
            
            if (rules.TryGetValue("class.maxLineCount", out var maxLineCount) && report.Lines.Length > (int)(long) maxLineCount)
            {
                suggestions.Add($"Class {report.Name} has too many lines");
            }
            
            if (rules.TryGetValue("class.maxLineLength", out var maxLineLength))
            {
                var x = 0;
                
                foreach (var line in report.Lines)
                {
                    x++;
                    
                    if (line.Length > (int)(long) maxLineLength)
                    {
                        suggestions.Add($"Class {report.Name} line {x} is too long");
                    }
                }
            }

            if (rules.TryGetValue("method.maxLineCount", out var maxMethodLineCount))
            {
                foreach (var method in report.Methods)
                {
                    if (method.LineCount > (int)(long) maxMethodLineCount)
                    {
                        suggestions.Add($"Class {report.Name} method {method.Name} has too many lines");
                    }
                }
            }
        }

        return Task.FromResult(suggestions);
    }
}