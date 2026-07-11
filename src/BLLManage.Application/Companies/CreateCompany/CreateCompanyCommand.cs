using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLManage.Application.Companies.CreateCompany
{
    public sealed record CreateCompanyCommand(
        string Name,
        string Email,
        string Phone);
}
