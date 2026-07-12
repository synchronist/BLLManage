using BLLManage.Application.Interfaces;
using MediatR;

namespace BLLManage.Application.Features.Companies.GetById;

public sealed class GetCompanyByIdHandler
    : IRequestHandler<GetCompanyByIdQuery, CompanyResponse?>
{
    private readonly ICompanyRepository _repository;

    public GetCompanyByIdHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<CompanyResponse?> Handle(
        GetCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (company is null)
            return null;

        return new CompanyResponse(
            company.Id,
            company.Name,
            company.Email,
            company.Phone);
    }
}