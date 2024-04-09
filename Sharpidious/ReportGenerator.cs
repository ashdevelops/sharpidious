using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sharpidious;

public class ReportGenerator
{
    public async Task<List<FileReport>> GetReportsForDirectoryAsync(string directory)
    {
        var reports = new List<FileReport>();
        var codeFiles = Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories);

        foreach (var file in codeFiles)
        {
            reports.Add(await GetReportForFileAsync(file));
        }

        return reports;
    }

    private async Task<FileReport> GetReportForFileAsync(string file)
    {
        var name = file.Split("/").Last().Replace(".cs", "");
        var path = file;
        
        var code = await File.ReadAllTextAsync(file);
        var tree = CSharpSyntaxTree.ParseText(code);
        var root = await tree.GetRootAsync();
        
        var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
        var properties = root.DescendantNodes().OfType<PropertyDeclarationSyntax>();

        var reportMethods = methods.Select(x =>
            new FileReportMethod(x.Identifier.ToFullString(), x.ToFullString())
        ).ToList();

        var reportProperties = properties.Select(x => 
            new FileReportProperty(x.Identifier.ToFullString())
        ).ToList();
        
        return new FileReport(name, path, reportMethods, reportProperties, code);
    }
}