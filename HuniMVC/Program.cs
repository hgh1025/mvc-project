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
using HuniMVC.Repository;
using Microsoft.AspNetCore.Session;
using HuniMVC.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HuniMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HuniMVCContext") ?? throw new InvalidOperationException("Connection string 'HuniMVCContext' not found.")));

// Add services to the container.
builder.Services.AddScoped<IOrderRepository, OrderRepositoryIM>(); // 의존성 등록
builder.Services.AddScoped<IFoodsRepository, FoodsRepository>(); // 의존성 등록
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

//2023-09-12 세션 서비스 추가
//builder.Services.AddDistributedMemoryCache(); //서비스의 수명 주기 문제를 해결하기위해 분산 메모리 캐시를 추가
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1); // 세션 타임아웃을 설정합니다. 필요에 따라 조정할 수 있습니다.
    options.Cookie.HttpOnly = true; //XSS 공격 방지
    options.Cookie.IsEssential = true; //사용자 동의없이 쿠키 정보 읽기 불가능
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

app.UseSession(); //2023-09-12 세션 미들웨어 추가, Routing 다음에 추가 그 이유는 라우팅 미들웨어가 요청을 처리하고 적절한 엔드포인트로 전달하기 전에 세션 데이터에 액세스할 수 있어야 하기 때문입니다.

app.UseEndpoints(endpoints => 
{
    // UseSession은 UseEndpoints 이전에 호출되어야 합니다. 그 이유는 엔드포인트에서 처리된 후에는 세션 변경 사항을 커밋할 수 없기 때문입니다.
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sign}/{action=LogIn}/{id?}");

app.Run();
