using Microsoft.EntityFrameworkCore;
using WebApplicationExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
{
    builder.Services.AddRazorPages();

    var connectionString = builder.Configuration.GetConnectionString("AppDb");
    builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connectionString));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    //    app.MapControllers();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

