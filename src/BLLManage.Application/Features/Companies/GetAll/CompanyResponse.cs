
namespace BLLManage.Application.Features.Companies.GetAll;

public sealed record CompanyResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone);