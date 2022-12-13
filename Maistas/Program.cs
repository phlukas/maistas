using Maistas.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("FoodDbContextConnection") ?? throw new InvalidOperationException("Connection string 'FoodDbContextConnection' not found.");

var connectionString = builder.Configuration.GetValue<string>("SqlConnectionString");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FoodDbContext>(
          options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

builder.Services.AddDefaultIdentity<MaistasUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<FoodDbContext>();

//Atkomentuoti kai veiks api
//builder.Services.AddTransient<IEmailSender, EmailService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
