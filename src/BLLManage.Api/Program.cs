using BLLManage.Api.Middlewares;
using BLLManage.Application.Behaviors;
using BLLManage.Application.Features.Authentication.Login;
using BLLManage.Application.Features.Companies.Create;
using BLLManage.Application.Features.Companies.Delete;
using BLLManage.Application.Features.Companies.GetAll;
using BLLManage.Application.Features.Companies.GetById;
using BLLManage.Application.Features.Companies.Update;
using BLLManage.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Services
builder.Services.AddScoped<CreateCompanyCommandHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Informe o token JWT no formato: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
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

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
builder.Services
    .AddHealthChecks()
    .AddNpgSql(
        builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateCompanyCommandHandler).Assembly);
});
builder.Services.AddValidatorsFromAssemblyContaining<CreateCompanyValidator>();

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));


var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapHealthChecks("/health");
app.MapPost("/companies",
    async (
        CreateCompanyCommand command,
        IMediator mediator,
        CancellationToken cancellationToken) =>
    {
        var id = await mediator.Send(command, cancellationToken);

        return Results.Created($"/companies/{id}", id);
    });

app.MapGet("/companies",
    async (
        int page,
        int pageSize,
        IMediator mediator) =>
    {
        var result = await mediator.Send(
            new GetAllCompaniesQuery(page, pageSize));

        return Results.Ok(result);
    }).RequireAuthorization()
      .WithTags("Companies");
      

app.MapGet("/companies/{id:guid}", async (
    Guid id,
    IMediator mediator) =>
{
    var company = await mediator.Send(new GetCompanyByIdQuery(id));

    return company is null
        ? Results.NotFound()
        : Results.Ok(company);
});

app.MapPut("/companies/{id:guid}", async (
    Guid id,
    UpdateCompanyCommand command,
    IMediator mediator) =>
{
    if (id != command.Id)
        return Results.BadRequest();

    await mediator.Send(command);

    return Results.NoContent();
})
.WithName("UpdateCompany");

app.MapDelete("/companies/{id:guid}", async (
    Guid id,
    IMediator mediator) =>
{
    await mediator.Send(new DeleteCompanyCommand(id));

    return Results.NoContent();
})
.WithName("DeleteCompany");

app.MapPost("/login",
    async (
        LoginCommand command,
        IMediator mediator) =>
    {
        var response = await mediator.Send(command);

        return Results.Ok(response);
    }).WithTags("Authentication");

app.Run();
