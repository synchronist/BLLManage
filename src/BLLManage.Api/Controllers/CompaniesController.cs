using BLLManage.Application.Features.Companies.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BLLManage.Api.Controllers;

[ApiController]
[Route("api/companies")]
public sealed class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyCommand command)
    {
        var id = await _mediator.Send(command);

        return Created($"/api/companies/{id}", id);
    }
}