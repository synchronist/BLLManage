using BLLManage.Application.Common.Pagination;
using BLLManage.Application.Interfaces;
using MediatR;

namespace BLLManage.Application.Features.Companies.GetAll;

public sealed class GetAllCompaniesHandler
    : IRequestHandler<GetAllCompaniesQuery, PagedResult<CompanyResponse>>
{
    private readonly ICompanyRepository _repository;

    public GetAllCompaniesHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<CompanyResponse>> Handle(
    GetAllCompaniesQuery request,
    CancellationToken cancellationToken)
    {
        var companies = await _repository.GetAllAsync(cancellationToken);

        var totalItems = companies.Count();

        var items = companies
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(company => new CompanyResponse(
                company.Id,
                company.Name,
                company.Email,
                company.Phone))
            .ToList();

        return new PagedResult<CompanyResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems
        };
    }
}