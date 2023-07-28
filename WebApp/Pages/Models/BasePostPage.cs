using Markdig;

namespace WebApp.Pages.Models;

public class BasePostPage
{
    public string PageContent { get; set; }
    public Metadata Metadata { get; set; }
    

    public BasePostPage(string path)
    {
        using var reader = new StreamReader(path);
        var fileContent = reader.ReadToEnd();

        Metadata = Metadata.ParseMetadata(fileContent);
        var pageContentString = Metadata.ParseContent(fileContent);
        
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();
        PageContent = Markdown.ToHtml(pageContentString, pipeline);
    }
}