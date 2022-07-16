using Microsoft.EntityFrameworkCore;
using ScafoldDb.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region ioc
#region di-db
//register db client instance
var connectionStringTestDb = builder.Configuration.GetConnectionString("TestDbCs");
builder.Services.AddDbContext<MyTestContext>(
    options => options.UseSqlServer(connectionStringTestDb, builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));
#endregion di-db
#endregion ioc

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
