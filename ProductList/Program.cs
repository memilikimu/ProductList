using ProductList.Data.Concretes;
using ProductList.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISaleRepository, SaleRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();
IWebHostEnvironment env = app.Services.GetRequiredService<IWebHostEnvironment>();
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.ContentRootPath, "Rotativa/bin");

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
    pattern: "{controller=Sales}/{action=Index}/{id?}");

app.Run();
