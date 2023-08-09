using Expenses.Data;
using Expenses.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext:
var connectionString = builder.Configuration.GetConnectionString("ExpensesContext");
var serverVersion = new MySqlServerVersion(new Version(7, 0, 0));
builder.Services.AddDbContext<ExpensesContext>(options =>
                    options.UseMySql(connectionString, serverVersion));

// Register services:
builder.Services.AddScoped<MovementService>();
builder.Services.AddScoped<SeedService>();

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
    pattern: "{controller=Movements}/{action=Index}/{id?}");

app.Run();
