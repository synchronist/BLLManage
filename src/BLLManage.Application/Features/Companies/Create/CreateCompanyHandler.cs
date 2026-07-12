using BLLManage.Application.Interfaces;
using BLLManage.Domain.Entities;
using MediatR;


namespace BLLManage.Application.Features.Companies.Create;

public sealed class CreateCompanyCommandHandler
    : IRequestHandler<CreateCompanyCommand, Guid>
{
    private readonly ICompanyRepository _repository;

    public CreateCompanyCommandHandler(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByEmailAsync(request.Email, cancellationToken))
            throw new InvalidOperationException("Company email already exists.");

        var company = new Company(
            request.Name,
            request.Email,
            request.Phone);

        await _repository.AddAsync(company, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
