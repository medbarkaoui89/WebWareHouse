using Microsoft.EntityFrameworkCore;
using WebWareHouse.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string cnn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<WareHouseContext>(x => x.UseSqlServer(cnn));

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
    pattern: "{controller=Consumers}/{action=Index}/{id?}");


app.Run();