using BLLManage.Application.Interfaces;
using MediatR;

namespace BLLManage.Application.Features.Companies.GetAll;

public sealed class GetAllCompaniesHandler
    : IRequestHandler<GetAllCompaniesQuery, List<CompanyResponse>>
{
    private readonly ICompanyRepository _repository;

    public GetAllCompaniesHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CompanyResponse>> Handle(
        GetAllCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var companies = await _repository.GetAllAsync(cancellationToken);

        return companies
            .Select(x => new CompanyResponse(
                x.Id,
                x.Name,
                x.Email,
                x.Phone))
            .ToList();
    }
}