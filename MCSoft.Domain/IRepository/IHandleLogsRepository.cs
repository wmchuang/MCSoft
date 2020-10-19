using System;
using MCSoft.Domain.Models;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Domain.IRepository
{
    public interface IHandleLogsRepository : IRepository<HandleLog, Guid>
    {

    }
}