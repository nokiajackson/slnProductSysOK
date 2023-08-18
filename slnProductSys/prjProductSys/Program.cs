using Microsoft.AspNetCore.Authentication.Cookies;
using prjProductSys.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddDbContext<DbProductContext>();


//�W�[���Ҥ覡�A�ϥ� cookie ����
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    //�s��������cookie �u��g��HTTP(S) ��w�Ӧs��
    options.Cookie.HttpOnly = true;
    //���n�J�ɷ|�۰ʾɨ�n�J��
    options.LoginPath = new PathString("/Home/Login");
    //���v�������ڵ��X�ݷ|�۰ʾɨ즹���|
    options.AccessDeniedPath = new PathString("/Home/NoAuthorization");
});

var app = builder.Build();

app.UseStaticFiles();          //�ϥ��R�A�ɸ귽
app.UseCookiePolicy();      //�ϥ�Cookie Policy
app.UseAuthentication();    //�ϥΨ�������    
app.UseAuthorization();      //�ϥΨ������v

//app.MapControllerRoute(name: "default",
//    pattern: "{Controller=Admin}/{Action=Index}/{id?}");

app.MapControllerRoute(name: "default",
    pattern: "{Controller=Admin}/{Action=Index}/{id?}");

//app.MapDefaultControllerRoute();  //Home/Index

//app.MapGet("/", () => "Hello World!");

app.Run();
