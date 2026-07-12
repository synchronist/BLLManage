using BLLManage.Application.Interfaces;
using MediatR;

namespace BLLManage.Application.Features.Companies.Delete;

public sealed class DeleteCompanyHandler
    : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _repository;

    public DeleteCompanyHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DeleteCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (company is null)
            throw new InvalidOperationException("Company not found.");

        _repository.Remove(company);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}