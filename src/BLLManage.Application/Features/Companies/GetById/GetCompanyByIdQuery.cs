using BLLManage.Application.Features.Companies.GetAll;
using MediatR;

namespace BLLManage.Application.Features.Companies.GetById;

public sealed record GetCompanyByIdQuery(Guid Id)
    : IRequest<CompanyResponse?>;