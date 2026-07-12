using MediatR;

namespace BLLManage.Application.Features.Companies.GetAll;

public sealed record GetAllCompaniesQuery()
    : IRequest<List<CompanyResponse>>;