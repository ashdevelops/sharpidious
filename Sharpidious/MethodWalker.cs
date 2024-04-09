using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sharpidious;

internal class MethodWalker : CSharpSyntaxWalker
{
    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        var lineSpan = node.GetLocation().GetLineSpan();
        var start = lineSpan.StartLinePosition.Line;
        var end = lineSpan.EndLinePosition.Line;
        var count = end - start + 1; // including declaration

        Console.WriteLine($"Method found: {node.Identifier.ValueText}, number of lines: {count}");

        base.VisitMethodDeclaration(node);
    }
}