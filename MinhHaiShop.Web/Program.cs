using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MinhHaiShop.Data;
using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Data.Repositories;
using MinhHaiShop.Model.Models;
using MinhHaiShop.Service;
using MinhHaiShop.Web.Mappings;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IErrorRepository, ErrorRepository>();
builder.Services.AddScoped<IErrorService, ErrorService>();
builder.Services.AddScoped<IPostCategoryService, PostCategoryService>();
builder.Services.AddScoped<IPostCategotyRepository, PostCategoryRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));

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
/*app.UseAuthentication();*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
