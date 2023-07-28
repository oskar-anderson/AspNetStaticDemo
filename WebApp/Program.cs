using AspNetStatic;

var builder = WebApplication.CreateBuilder(args);

var postsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "Posts");
var relativePaths = Directory.GetFiles(postsDirectoryPath, "*.cshtml", SearchOption.AllDirectories)
    .Select(x => Path.GetRelativePath(postsDirectoryPath, x))
    .ToList();
var relativePathsNormalized = relativePaths.Select(x => x
    .Replace("\\", "/")
    .Replace(".cshtml", "")
).ToList();
var postPageInfo = relativePathsNormalized.Select(x => 
    new PageInfo("/Posts/" + x)
).ToList();
var pageInfoList = new List<PageInfo> { new("/index") };
pageInfoList.AddRange(postPageInfo);
builder.Services.AddSingleton<IStaticPagesInfoProvider>(
    new StaticPagesInfoProvider(pageInfoList)
);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
app.UsePathBase("/AspNetStaticDemo");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.GenerateStaticPages(
    app.Environment.WebRootPath,
    dontOptimizeContent: true, 
    alwaysDefaultFile: true,
    exitWhenDone: args.Contains("exit")
);

app.Run();