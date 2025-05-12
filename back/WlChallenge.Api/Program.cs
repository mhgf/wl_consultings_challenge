using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using WlChallenge.Api.Endpoints.Auth;
using WlChallenge.Api.Endpoints.User;
using WlChallenge.Api.Shared;
using WlChallenge.Api.Shared.Response;
using WlChallenge.Application;
using WlChallenge.Application.Exceptions;
using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Exceptions;
using WlChallenge.Domain.ValueObjects;
using WlChallenge.Infra;
using WlChallenge.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
if (connection is null || connection.Length == 0)
    throw new ArgumentNullException(nameof(WlChallenge.Domain.Configuration.Database.ConnectionString));
WlChallenge.Domain.Configuration.Database.ConnectionString = connection;

Settings.Jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>() ?? new JwtSettings();
if (Settings.Jwt.Key.Length == 0)
    throw new ArgumentNullException(nameof(Settings.Jwt));
;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer(((document, _, _) =>
    {
        document.Info = new OpenApiInfo()
        {
            Title = "Desafio técnico | v1",
            Version = "v1",
            Description = "Documentação da api.",
        };

        var requirements = new Dictionary<string, OpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // "bearer" refers to the header name here
                In = ParameterLocation.Header,
                BearerFormat = "Json Web Token"
            }
        };
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = requirements;
        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
        {
            operation.Value.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] =
                    Array.Empty<string>()
            });
        }

        return Task.CompletedTask;
    }));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var key = Encoding.ASCII.GetBytes(Settings.Jwt.Key);
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });
builder.Services.AddAuthorization();

// Add DI
builder.Services.AddApplication();
builder.Services.AddInfra();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("api/doc.json");
    app.MapScalarApiReference(opt =>
        opt.WithOpenApiRoutePattern("api/doc.json")
            .WithTitle("Desafio Wl Consulting")
            .WithSidebar(true)
            .WithTheme(ScalarTheme.Solarized)
            .WithDarkMode(true)
            .WithPreferredScheme(JwtBearerDefaults.AuthenticationScheme)
            .AddHttpAuthentication(JwtBearerDefaults.AuthenticationScheme,
                scheme => { scheme.Description = "JWT Token"; }));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddUserEndpoints();
app.AddAuthEndpoints();

app.UseExceptionHandler(exHandler => exHandler.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>();
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

    if (error is null) return;

    switch (error.Error)
    {
        case ValidationException validationException:
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(
                Response.Fail(validationException.Errors.Select(x => x.ErrorMessage)));
            return;
        case DomainException domainException:
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(Response.Fail([domainException.Message]));
            return;
        default:
            await context.Response.WriteAsJsonAsync(new
            {
                msg = "An internal server error occured"
            });
            break;
    }
}));

await using (var scope = app.Services.CreateAsyncScope())
await using (var db = scope.ServiceProvider.GetRequiredService<AppDbContext>())
{
    await db.Database.EnsureCreatedAsync();

    List<string> cpfsCnpjs =
    [
        "84917477700",
        "52520867000171",
        "41835446299",
        "82618117000106",
        "94271438944",
        "09208424000110",
        "34441165943",
        "11640977000190",
        "54487124522",
        "34776179000195"
    ];

    var count = await db.Users.CountAsync();
    if (count == 0)
    {
        foreach (var value in cpfsCnpjs.Select((x, i) => new { Index = ++i, CpfCnpj = x }))
        {
            var user = User.Create($"Usuario {value.Index}",
                Email.Create($"user{value.Index}@email.com"),
                Password.Create("123456789012"),
                value.CpfCnpj.Length == Cpf.MinLength ? Cpf.Create(value.CpfCnpj) : Cnpj.Create(value.CpfCnpj));
            await db.Users.AddAsync(user);
        }

        await db.SaveChangesAsync();
    }
}

app.Run();