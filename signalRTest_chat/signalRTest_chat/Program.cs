using CoreLayer.Services.Chats;
using CoreLayer.Services.Chats.ChatGroups;
using CoreLayer.Services.Roles;
using CoreLayer.Services.Users;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using signalRTest_chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//ConfigureServices
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<ChatContext>(x => x.UseSqlServer(connectionString));

//di
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatGroupService, ChatGroupService>();
//signalR
builder.Services.AddSignalR();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}
).AddCookie(option =>
{
    option.LoginPath = "/auth";
    option.LogoutPath = "/auth/logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//signalR
app.MapHub<ChatHub>("/chat");
app.Run();
