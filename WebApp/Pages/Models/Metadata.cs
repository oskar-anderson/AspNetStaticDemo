using System.Text.Json;

namespace WebApp.Pages.Models;

public class Metadata
{
    public string Title { get; set; } = "";
    
    public string Description { get; set; } = "";
    
    public string[] TechStack { get; set; } = Array.Empty<string>();
        
    public Link[] Links { get; set; } = Array.Empty<Link>();

    public PostImage Image { get; set; } = new PostImage();

    public string ReadMoreLink { get; set; } = "";

    public static Metadata ParseMetadata(string fileContent)
    {
        ParseSplitMarkdownContent(fileContent, out var metadata, out _);
        return metadata;
    }

    public static string ParseContent(string fileContent)
    {
        ParseSplitMarkdownContent(fileContent, out _, out var pageContent);
        return pageContent;
    }
    
    public static void ParseSplitMarkdownContent(string fileContent, out Metadata metadata, out string pageContent)
    {
        var firstSeparatorIndexStart = fileContent.IndexOf("---", StringComparison.Ordinal);
        var firstSeparatorIndexEnd = firstSeparatorIndexStart + 3;
        var secondSeparatorIndexStart = fileContent.IndexOf("---", firstSeparatorIndexEnd, StringComparison.Ordinal);
        var secondSeparatorIndexEnd = secondSeparatorIndexStart + 3;


        // Extract the JSON front matter
        var json = fileContent.Substring(firstSeparatorIndexEnd, secondSeparatorIndexStart - firstSeparatorIndexEnd).Trim();
        var content = fileContent.Substring(secondSeparatorIndexEnd).Trim();
        pageContent = content;
        try
        {
            var _ = JsonSerializer.Deserialize<Metadata>(json) ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            metadata = new Metadata();
        }
        metadata = JsonSerializer.Deserialize<Metadata>(json)!;
    }
}

public class Link
{
    public string Display { get; set; } = "";
    public string Href { get; set; } = "";
}

public class PostImage
{
    public string Src { get; set; } = "";
    public string Alt { get; set; } = "";
}