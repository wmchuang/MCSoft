using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
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

        protected ICurrentUser _currentUser { get; }
        public UserService(IRepository<User, Guid> userRepository, IHeadRepository headRepository, ICurrentUser currentUser)
        {

            _userRepository = userRepository;
            _headRepository = headRepository;
            _currentUser = currentUser;
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
            var userId = _currentUser.Id.Value;
            var user = await _userRepository.GetAsync(userId);

            var head = await _headRepository.GetIncludeAsync(user.BelongHeadId.Value);
            head.AddBrowseCount();
            return head;
        }

    }
}
