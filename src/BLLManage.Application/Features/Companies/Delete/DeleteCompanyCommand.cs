using MediatR;

namespace BLLManage.Application.Features.Companies.Delete;

public sealed record DeleteCompanyCommand(Guid Id) : IRequest;