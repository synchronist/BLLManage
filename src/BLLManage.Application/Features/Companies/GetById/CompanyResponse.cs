namespace BLLManage.Application.Features.Companies.GetById;

public sealed record CompanyResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone);