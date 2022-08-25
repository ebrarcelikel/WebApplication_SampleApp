using Microsoft.EntityFrameworkCore;
using WebApplication_SampleApp;
using WebApplication_SampleApp.Models.Context;
using WebApplication_SampleApp.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache()
    .AddSession(options =>
    {
        options.Cookie.Name = "services.session";
        options.IdleTimeout = TimeSpan.FromMinutes(20);
    }
    );

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
    

    



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

AppSettings appSettings= app.Configuration.GetSection("AppSettings").Get<AppSettings>();
DatabaseInitializer databaseInitializer = new DatabaseInitializer(new DatabaseContext(), appSettings);
databaseInitializer.Seed();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


