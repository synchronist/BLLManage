using BLLManage.Application.Interfaces;
using MediatR;

namespace BLLManage.Application.Features.Companies.Update;

public sealed class UpdateCompanyHandler
    : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _repository;

    public UpdateCompanyHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (company is null)
            throw new InvalidOperationException("Company not found.");

        company.Update(
            request.Name,
            request.Email,
            request.Phone);

        _repository.Update(company);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}