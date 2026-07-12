using BLLManage.Domain.Entities;

namespace BLLManage.Application.Interfaces;

public interface ICompanyRepository
{
    Task AddAsync(Company company, CancellationToken cancellationToken);

    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<List<Company>> GetAllAsync(CancellationToken cancellationToken);
    void Update(Company company);
    void Remove(Company company);
}