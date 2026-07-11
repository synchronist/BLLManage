using BLLManage.Application.Companies.CreateCompany;
using Microsoft.AspNetCore.Mvc;

namespace BLLManage.Api.Controllers;

[ApiController]
[Route("api/companies")]
public sealed class CompaniesController : ControllerBase
{
    private readonly CreateCompanyHandler _handler;

    public CompaniesController(CreateCompanyHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public IActionResult Create(CreateCompanyCommand command)
    {
        var company = _handler.Handle(command);

        return Created($"/api/companies/{company.Id}", company);
    }
}