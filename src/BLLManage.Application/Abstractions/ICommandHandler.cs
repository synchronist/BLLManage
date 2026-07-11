using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLManage.Application.Abstractions
{
    public interface ICommandHandler<in TCommand, out TResult>
    {
        TResult Handle(TCommand command);
    }
}
