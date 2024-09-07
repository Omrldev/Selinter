var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

var name = "ReverseProxy";
var proxy = builder.Configuration.GetSection(name);
builder.Services.AddReverseProxy().LoadFromConfig(proxy);

var app = builder.Build();

#region
// Configure the HTTP request pipeline.
/*if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();*/
#endregion

app.MapReverseProxy();

app.Run();
