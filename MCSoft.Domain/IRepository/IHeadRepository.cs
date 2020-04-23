using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Domain.IRepository
{
    public interface IHeadRepository : IRepository<Head, Guid>
    {
        Task ChangeStatus(Guid headId, Status status);

        Task<Head> GetIncludeAsync(Guid headId);
    }
}
