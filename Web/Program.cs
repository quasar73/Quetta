using Data;
using Data.Models;
using Logic.Interfaces;
using Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

#region Serilog set up

builder.Logging.ClearProviders();

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.AddSerilog(logger);

#endregion

#region ASP.NET Identity and Database set up

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<QuettaDbContext>(options =>
{
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<QuettaDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Authentication set up

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Jwt:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });

#endregion

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(it => it.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await DataInitializer.Seed(scope);
}

app.Run();
