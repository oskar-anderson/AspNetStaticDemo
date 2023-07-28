using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Pages.Models;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IWebHostEnvironment _environment;
    public Metadata[] Projects;

    public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
        
        var project1Path = Path.Combine(_environment.ContentRootPath, "wwwroot", "static", "content", "Project1.md");
        using var project1Reader = new StreamReader(project1Path);
        var project1Metadata = Metadata.ParseMetadata(project1Reader.ReadToEnd());
            
        var project2Path = Path.Combine(_environment.ContentRootPath, "wwwroot", "static", "content", "Project2.md");
        using var project2Reader = new StreamReader(project2Path);
        var project2Metadata = Metadata.ParseMetadata(project2Reader.ReadToEnd());
        
        Projects = new[] { project1Metadata, project2Metadata };
    }

    public void OnGet()
    {
    }
}