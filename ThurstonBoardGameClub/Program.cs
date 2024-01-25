using ThurstonBoardGameClub.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using ThurstonBoardGameClub.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddTransient<IMessageRepository, MessageRepository>();

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("SmarterASP.NET");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await SeedUsers.CreateUsers(scope.ServiceProvider);
    var context = scope.ServiceProvider
      .GetRequiredService<AppDbContext>();
    SeedData.Seed(context, scope.ServiceProvider);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
/*app.UseDefaultFiles();*/
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();