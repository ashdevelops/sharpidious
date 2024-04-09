namespace Sharpidious;

public class FileReport(
    string name, 
    string path, 
    List<FileReportMethod> methods, 
    List<FileReportProperty> properties,
    string content)
{
    public string Name { get; set; } = name;
    public string Path { get; set; } = path;
    public List<FileReportMethod> Methods { get; set; } = methods;
    public List<FileReportProperty> Properties { get; } = properties;
    public string Content { get; set; } = content;
    public string[] Lines = content.Split(Environment.NewLine);
}