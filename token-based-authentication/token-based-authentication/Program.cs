using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using token_based_authentication.Data;
using token_based_authentication.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // configure SwaggerDoc and others

    // add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//di
//builder.Services.AddScoped(UserManager<ApplicationUser>, UserManager<ApplicationUser>);
//builder.Services.AddScoped(RoleManager<IdentityRole>, RoleManager<IdentityRole>);
//builder.Services.AddScoped(IConfiguration, IConfiguration)

var tokenValidationParameter = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),

    ValidateIssuer = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],

    ValidateAudience = true,
    ValidAudience = builder.Configuration["Jwt:Audience"],

    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
};

builder.Services.AddSingleton(tokenValidationParameter);


//add identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//add authentication
{
    //add authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    //add JWT Bearer
    .AddJwtBearer(options =>
     {
         options.SaveToken = true;
         options.RequireHttpsMetadata = false;
         options.TokenValidationParameters = tokenValidationParameter;

         options.Events = new JwtBearerEvents
         {
             OnAuthenticationFailed = (context) =>
             {
                 var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>(); 
                 var logger = loggerFactory.CreateLogger("Startup");

                 if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                 {
                     logger.LogInformation("Token-Expired");
                 }
                 else {
                     logger.LogInformation(context.Exception.GetType().ToString());

                 }
                 return System.Threading.Tasks.Task.CompletedTask;
             }
         };
     });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//add authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
