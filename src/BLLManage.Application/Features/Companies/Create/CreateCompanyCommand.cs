using MediatR;

namespace BLLManage.Application.Features.Companies.Create;

public sealed record CreateCompanyCommand(
    string Name,
    string Email,
    string Phone
) : IRequest<Guid>;