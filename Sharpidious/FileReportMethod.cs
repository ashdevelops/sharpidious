namespace CodeQualityChecker;

public class FileReportMethod(string name, string content)
{
    public string Name { get; } = name;
    public string Content { get; } = name;
    public int LineCount { get; } = content.Split(Environment.NewLine).Length;
}