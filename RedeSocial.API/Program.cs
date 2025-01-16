using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RedeSocial.Application;
using RedeSocial.Infrastructure;
using RedeSocial.Infrastructure.Context;
using RedeSocial.Infrastructure.Profiles;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RedeSocial.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Alef David, Caio Gaspar, Henrique Pereira, Wesley Silva",
            Url = new Uri("https://github.com/alefdavid/RedeSocial.API")
        }
    });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Context
var connectionString = builder.Configuration.GetConnectionString("DBRedeSocial");
builder.Services.AddDbContext<RedeSocialDbContext>(options => options.UseSqlServer(connectionString));

// Dependencies
builder.Services.RegisterApplicationDependencies();
builder.Services.RegisterInfrastrutureDependencies();

// Mapping
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.AddControllers();

// Authentication and Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure exception handling
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext context) =>
{
    context.Response.StatusCode = 500;
    return context.Response.WriteAsync("An error occurred while processing your request.");
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();