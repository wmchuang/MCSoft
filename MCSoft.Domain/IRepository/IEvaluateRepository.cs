using MCSoft.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Domain.IRepository
{
    public interface IEvaluateRepository : IRepository<Evaluate, Guid>
    {
        IQueryable<Evaluate> QueryInclude();
    }
}
