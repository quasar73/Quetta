using Quetta.Data.Models;
using Quetta.Logic.HostedServices;
using Quetta.Logic.Interfaces;
using Quetta.Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;
using Quetta.Web.Middlewares;
using MediatR;
using Quetta.Logic.Handlers.Queries;
using Quetta.Logic.Hubs;
using FluentValidation.AspNetCore;
using Quetta.Common.Validators.Commands;
using Quetta.Data.Mapping;
using Quetta.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Serilog set up

builder.Logging.ClearProviders();

var logger = new LoggerConfiguration().MinimumLevel
    .Override(
        "Microsoft.EntityFrameworkCore.Database.Command",
        Serilog.Events.LogEventLevel.Warning
    )
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.AddSerilog(logger);

#endregion

#region ASP.NET Identity and Database set up

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<QuettaDbContext>(options =>
{
    options.UseNpgsql(
        connectionString,
        b => b.MigrationsAssembly(typeof(QuettaDbContext).GetTypeInfo().Assembly.FullName)
    );
});

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<QuettaDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Authentication set up

builder.Services
    .AddAuthentication(options =>
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Jwt:Secret"])
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                var path = context.HttpContext.Request.Path;
                if (
                    !string.IsNullOrEmpty(accessToken)
                    && (
                        path.StartsWithSegments("/invite")
                        || path.StartsWithSegments("/message")
                        || path.StartsWithSegments("/read")
                        || path.StartsWithSegments("/sidebar")
                    )
                )
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });

#endregion

builder.Services.AddMediatR(typeof(RefreshTokenHandler).GetTypeInfo().Assembly);

builder.Services.AddAutoMapper(typeof(InviteProfile).Assembly);

builder.Services.AddSignalR();

builder.Services.AddCors();

builder.Services
    .AddControllers()
    .AddFluentValidation(
        fv => fv.RegisterValidatorsFromAssemblyContaining<AuthenticateGoogleUserValidator>()
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quetta API", Version = "v1" });
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
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
                new List<string>()
            }
        }
    );
});

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IBaseEncryptingService, AESEnctyptingService>();

builder.Services.AddHostedService<TokenCleanerHostedService>();

builder.Services.AddCors(
    options =>
        options.AddPolicy(
            "CorsPolicy",
            builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials();
            }
        )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<InviteHub>("/invite");
    endpoints.MapHub<MessageHub>("/message");
    endpoints.MapHub<ReadHub>("/read");
    endpoints.MapHub<SidebarHub>("/sidebar");
});

using (var scope = app.Services.CreateScope())
{
    await DataInitializer.Seed(scope);
}

app.Run();
