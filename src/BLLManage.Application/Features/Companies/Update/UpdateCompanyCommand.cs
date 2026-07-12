using MediatR;

namespace BLLManage.Application.Features.Companies.Update;

public sealed record UpdateCompanyCommand(
    Guid Id,
    string Name,
    string Email,
    string Phone
) : IRequest;