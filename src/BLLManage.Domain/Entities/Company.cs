using BLLManage.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLManage.Domain.Entities
{
    public sealed class Company : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public string Email { get; private set; } = null!;

        public string Phone { get; private set; } = null!;

        public Company(
            string name,
            string email,
            string phone)
        {
            SetName(name);
            SetEmail(email);
            SetPhone(phone);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Company name is required.");

            Name = name.Trim();
            MarkAsUpdated();
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Company email cannot be empty.");

            Email = email.Trim().ToLowerInvariant();
            MarkAsUpdated();
        }

        public void SetPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Company phone cannot be empty.");

            Phone = phone.Trim();
            MarkAsUpdated();
        }
        public void Update(
        string name,
        string email,
        string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
