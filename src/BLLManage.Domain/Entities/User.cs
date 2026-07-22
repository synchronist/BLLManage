using BLLManage.Domain.Exceptions;

namespace BLLManage.Domain.Entities;

public sealed class User
{
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public Company Company { get; private set; } = null!;

    public string Name { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string Role { get; private set; } = "User";

    private User() { }

    public User(
        Guid companyId,
        string name,
        string email,
        string passwordHash,
        string role = "User")
    {
        if (companyId == Guid.Empty)
            throw new DomainException("Company is required.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password is required.");

        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}