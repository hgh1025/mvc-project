using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HuniMVC.Data;
using HuniMVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using HuniMVC.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HuniMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HuniMVCContext") ?? throw new InvalidOperationException("Connection string 'HuniMVCContext' not found.")));

// Add services to the container.
builder.Services.AddScoped<IOrderRepository, OrderRepositoryIM>(); // 의존성 등록
builder.Services.AddScoped<HuniMVC.ActionResults.StandardJsonResult>();// 의존성 등록
builder.Services.AddScoped<HuniMVC.Views.Sign.LogInModel>();// 의존성 등록
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<HuniMVCContext>() // AddEntityFrameworkStores 사용을 위해 Microsoft.AspNetCore.Identity.EntityFrameworkCore 설치필요
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

 //Newtonsoft.Json을 사용하도록 다음 Web API, MVC 및 Razor Pages 기능을 구성합니다.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var app = builder.Build();

//SeedData추가
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
using (var scope = app.Services.CreateScope())
{
    var services2 = scope.ServiceProvider; //변수를 다르게 해야함 

    SnackSeedData.Initialize(services2);
}

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
    pattern: "{controller=Sign}/{action=LogIn}/{id?}");

app.Run();
