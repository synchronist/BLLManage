using BLLManage.Application.Common.Pagination;
using MediatR;

namespace BLLManage.Application.Features.Companies.GetAll;

public sealed record GetAllCompaniesQuery(
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedResult<CompanyResponse>>;