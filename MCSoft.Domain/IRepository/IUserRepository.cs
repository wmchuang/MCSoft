using MCSoft.Domain.Models;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Domain.IRepository
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> Authorize(string wxOpenId, Guid? headId);

        Task<User> wxAuthorize(string nickName, string headImg);

        int GetHeadFansCount(Guid headId);
    }
}
