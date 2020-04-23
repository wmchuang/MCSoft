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
    public class EvaluateRepository : EfCoreRepository<MCSoftDbContext, Evaluate, Guid>, IEvaluateRepository
    {
        private ICurrentUser _currentUser { get; }

        public EvaluateRepository(IDbContextProvider<MCSoftDbContext> dbContextProvider, ICurrentUser currentUser)
    : base(dbContextProvider)
        {
            _currentUser = currentUser;
        }

        public IQueryable<Evaluate> QueryInclude()
        {
            return DbContext.Evaluates.Include(x => x.Head).Include(x => x.User);
        }
    }
}
