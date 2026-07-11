using BLLManage.Application.Abstractions;
using BLLManage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLManage.Application.Companies.CreateCompany
{

    public sealed class CreateCompanyHandler
        : ICommandHandler<CreateCompanyCommand, Company>
    {
        public Company Handle(CreateCompanyCommand command)
        {
            return new Company(
                command.Name,
                command.Email,
                command.Phone);
        }
    }
}
