using HR_System.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HrSysContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("hrcon")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
//builder.Services.AddSession(Option => {
//    Option.IdleTimeout = TimeSpan.FromMinutes(15);
//    });


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
