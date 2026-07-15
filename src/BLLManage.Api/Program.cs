using BLLManage.Api.Middlewares;
using BLLManage.Application.Behaviors;
using BLLManage.Application.Features.Companies.Create;
using BLLManage.Application.Features.Companies.Delete;
using BLLManage.Application.Features.Companies.GetAll;
using BLLManage.Application.Features.Companies.GetById;
using BLLManage.Application.Features.Companies.Update;
using BLLManage.Infrastructure;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddScoped<CreateCompanyCommandHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
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
    });

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

app.Run();
