using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.Repositories.Users
{
    public class GetUserList
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
