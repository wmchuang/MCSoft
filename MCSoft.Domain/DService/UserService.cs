using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace MCSoft.Domain.Service
{
    public class UserService : DomainService
    {
        #region Ctor
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IHeadRepository _headRepository;
        private readonly ILogger<UserService> _logger;
        protected ICurrentUser _currentUser { get; }
        public UserService(IRepository<User, Guid> userRepository, IHeadRepository headRepository, ICurrentUser currentUser, ILogger<UserService> logger)
        {

            _userRepository = userRepository;
            _headRepository = headRepository;
            _currentUser = currentUser;
            _logger = logger;
        }

        #endregion

        public async Task ChangeHead(Guid headId)
        {
            var userId = _currentUser.Id.Value;

            var user = await _userRepository.GetAsync(x => x.Id == userId);
            user.ChangeHead(headId);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<Head> CurrentHead()
        {
            try
            {
                _logger.LogInformation("begin");
                var userId = _currentUser.Id.Value;
                _logger.LogInformation(userId.ToString());
                var user = await _userRepository.GetAsync(userId);
                _logger.LogInformation(user.ToString());

                var head = await _headRepository.GetIncludeAsync(user.BelongHeadId.Value);
                _logger.LogInformation(head.ToString());

                head.AddBrowseCount();

                _logger.LogInformation("end");
                return head;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }

        }

    }
}
