using custom_Authentication_scheme.Authentication;
using custom_Authentication_scheme.Authentication.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // configure SwaggerDoc and others

    //// add JWT Authentication
    //var securityScheme = new OpenApiSecurityScheme
    //{
    //    Name = "JWT Authentication",
    //    Description = "Enter JWT Bearer token **_only_**",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "bearer", // must be lower case
    //    BearerFormat = "JWT",
    //    Reference = new OpenApiReference
    //    {
    //        Id = JwtBearerDefaults.AuthenticationScheme,
    //        Type = ReferenceType.SecurityScheme
    //    }
    //};
    //c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {securityScheme, new string[] { }}
    //});

    // add Basic Authentication
    //var basicSecurityScheme = new OpenApiSecurityScheme
    //{
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "basic",
    //    Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
    //};
    //c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {basicSecurityScheme, new string[] { }}
    //});

    // add Token Authentication

    var tokenSecurityScheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Scheme = AuthSchemeConstants.MyAuthScheme,
        Description = $"Enter {AuthSchemeConstants.MyAuthScheme} Your_token **_only_**",
        Reference = new OpenApiReference { Id = AuthSchemeConstants.MyAuthScheme, Type = ReferenceType.SecurityScheme }
    };
    c.AddSecurityDefinition(tokenSecurityScheme.Reference.Id, tokenSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {tokenSecurityScheme, new string[] { }}
    });

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
}
);

builder.Services.AddAuthentication(
    options => options.DefaultScheme = AuthSchemeConstants.MyAuthScheme)
    .AddScheme<MyAuthSchemeOptions, MyAuthHandler>(
        AuthSchemeConstants.MyAuthScheme, options => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHttpsRedirection();
}

//app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
