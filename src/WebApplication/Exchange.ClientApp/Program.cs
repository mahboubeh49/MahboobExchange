using Exchange.ClientApp.ExternalServices;
using Exchange.ClientApp.ExternalServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IExchangeClient, ExchangeClient>();
builder.Services.AddScoped<IAccessToken, AccessToken>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("ExchangeApi", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("ExchangeApiUrl").Value);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
