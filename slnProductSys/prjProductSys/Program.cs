using Microsoft.AspNetCore.Authentication.Cookies;
using prjProductSys.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddDbContext<DbProductContext>();


//增加驗證方式，使用 cookie 驗證
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    //瀏覽器限制cookie 只能經由HTTP(S) 協定來存取
    options.Cookie.HttpOnly = true;
    //未登入時會自動導到登入頁
    options.LoginPath = new PathString("/Home/Login");
    //當權限不夠拒絕訪問會自動導到此路徑
    options.AccessDeniedPath = new PathString("/Home/NoAuthorization");
});

var app = builder.Build();

app.UseStaticFiles();          //使用靜態檔資源
app.UseCookiePolicy();      //使用Cookie Policy
app.UseAuthentication();    //使用身份驗證    
app.UseAuthorization();      //使用身份授權

//app.MapControllerRoute(name: "default",
//    pattern: "{Controller=Admin}/{Action=Index}/{id?}");

app.MapControllerRoute(name: "default",
    pattern: "{Controller=Admin}/{Action=Index}/{id?}");

//app.MapDefaultControllerRoute();  //Home/Index

//app.MapGet("/", () => "Hello World!");

app.Run();
