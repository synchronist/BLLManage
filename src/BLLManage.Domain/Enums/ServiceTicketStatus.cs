using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLManage.Domain.Enums;

public enum ServiceTicketStatus
{
    Open = 1,
    InProgress = 2,
    WaitingCustomer = 3,
    Completed = 4,
    Cancelled = 5
}