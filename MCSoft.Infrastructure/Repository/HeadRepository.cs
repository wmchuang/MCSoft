using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using MCSoft.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;

namespace MCSoft.Infrastructure.Repository
{
    public class HeadRepository : EfCoreRepository<MCSoftDbContext, Head, Guid>, IHeadRepository
    {
        private ICurrentUser _currentUser { get; }

        public HeadRepository(IDbContextProvider<MCSoftDbContext> dbContextProvider, ICurrentUser currentUser)
    : base(dbContextProvider)
        {
            _currentUser = currentUser;
        }

        public async Task ChangeStatus(Guid headId, Status status)
        {
            var head = await DbContext.Heads.SingleAsync(x => x.Id == headId);
            head.ChangeStatus(status);
            await DbContext.SaveChangesAsync();
        }
    }
}
