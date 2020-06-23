using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using MCSoft.Infrastructure.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;

namespace MCSoft.Infrastructure.Repository
{
    public class UserRepository : EfCoreRepository<MCSoftDbContext, User, Guid>, IUserRepository
    {
        private ICurrentUser _currentUser { get; }
        private readonly ILogger<UserRepository> _logger;

        private readonly IRepository<Head, Guid> _headRepository;
        public UserRepository(IDbContextProvider<MCSoftDbContext> dbContextProvider, ICurrentUser currentUser, IRepository<Head, Guid> headRepository, ILogger<UserRepository> logger)
    : base(dbContextProvider)
        {
            _currentUser = currentUser;
            _headRepository = headRepository;
            _logger = logger;
        }

        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <param name="wxOpenId"></param>
        /// <param name="nickName"></param>
        /// <param name="headImg"></param>
        /// <returns></returns>
        public async Task<User> Authorize(string wxOpenId, Guid? headId)
        {
            try
            {
                var user = DbContext.Users.Where(x => x.WxOpenId == wxOpenId).FirstOrDefault();
                if (user == null)
                {
                    //指定第一个团长为用户的默认团长
                    var tempheadId = headId.HasValue ? headId.Value : _headRepository.FirstOrDefault()?.Id ?? Guid.NewGuid();
                    user = new User(wxOpenId, "", "", tempheadId);
                    DbContext.Users.Add(user);
                }
                else if (headId.HasValue)
                {
                    user.BelongHeadId = headId.Value;
                }

                await DbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _logger.LogError(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 微信授权登录获取昵称和头像等信息
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="headImg"></param>
        /// <returns></returns>
        public async Task<User> wxAuthorize(string nickName, string headImg)
        {
            try
            {
                var user = await DbContext.Users.FindAsync(_currentUser.Id.Value);
                user.NickName = nickName;
                user.HeadImg = headImg;

                await DbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User GetCurrentUser()
        {
            var userId = _currentUser.Id.Value;
            return DbContext.Users.First(x => x.Id == userId);
        }

        /// <summary>
        /// 获取粉丝量
        /// </summary>
        /// <param name="headId"></param>
        /// <returns></returns>
        public int GetHeadFansCount(Guid headId)
        {
            return DbContext.Users.Count(x => x.BelongHeadId == headId);
        }
    }
}
