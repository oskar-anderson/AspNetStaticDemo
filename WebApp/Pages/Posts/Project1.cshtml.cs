using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Pages.Models;

namespace WebApp.Pages.Posts;

public class Project1 : PageModel
{
    public BasePostPage PostPage { get; set; }
    
    private readonly IWebHostEnvironment _environment;

    public Project1(IWebHostEnvironment environment)
    {
        _environment = environment;
        var path = Path.Combine(_environment.ContentRootPath, "wwwroot", "static", "content", "Project1.md");
        PostPage = new BasePostPage(path);
    }
    
    public void OnGet()
    {
        
    }
}