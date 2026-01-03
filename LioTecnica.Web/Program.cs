using LioTecnica.Web.Infrastructure.ApiClients;
using LioTecnica.Web.Services;
using RhPortal.Web.Infrastructure.ApiClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IGestoresLookupService, GestoresLookupService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Endpoints:RhApi"]!);
});

builder.Services.AddHttpClient<UnitsApiClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Endpoints:RhApi"]!);
});
builder.Services.AddHttpClient<ManagersApiClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Endpoints:RhApi"]!);
});
builder.Services.AddHttpClient<DepartmentsApiClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Endpoints:RhApi"]!);
});
builder.Services.AddHttpClient<AreasApiClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Endpoints:RhApi"]!);
});
builder.Services.AddHttpClient<VagasApiClient>(http =>
{
    http.BaseAddress = new Uri(builder.Configuration["Endpoints:RhApi"]!);
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
