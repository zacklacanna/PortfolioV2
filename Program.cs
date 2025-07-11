using ZackV2.Components;
using ZackV2.Services;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + browser (WASM-style) interactivity
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<ExperienceModalService>();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.IdleTimeout = TimeSpan.FromDays(14);

});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

var app = builder.Build();
app.UseCookiePolicy();
app.UseSession();
app.Use(async delegate (HttpContext context, Func<Task> Next)
{
    if (!context.Request.Cookies.ContainsKey("user_uid"))
    {
        var uid = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddYears(1)
        };
        context.Response.Cookies.Append("user_uid", uid, cookieOptions);
    }

    await Next(); //continue on with the request
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRouting();

// Serve the WASM framework files from wwwroot/_framework
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseWebSockets();
app.UseStaticFiles();
app.UseAntiforgery();

// Map your root component for prerender + browser activation
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();