using BLLManage.Application.Interfaces;
using BLLManage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLLManage.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository : ICompanyRepository
{
    private readonly BLLManageDbContext _context;

    public CompanyRepository(BLLManageDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Company company, CancellationToken cancellationToken)
    {
        await _context.Companies.AddAsync(company, cancellationToken);
    }

    public async Task<Company?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Company>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Companies
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public void Update(Company company)
    {
        _context.Companies.Update(company);
    }
    public void Remove(Company company)
    {
        _context.Companies.Remove(company);
    }
}