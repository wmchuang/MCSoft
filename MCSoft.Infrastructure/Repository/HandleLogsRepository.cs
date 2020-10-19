using System;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using MCSoft.Infrastructure.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MCSoft.Infrastructure.Repository
{
    public class HandleLogsRepository : EfCoreRepository<MCSoftDbContext, HandleLog, Guid>, IHandleLogsRepository
    {
        public HandleLogsRepository(IDbContextProvider<MCSoftDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}