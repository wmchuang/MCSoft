using MCSoft.Application.Dto.Head;
using MCSoft.Application.Dto.User;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using MCSoft.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MCSoft.Application.Service
{
    public class UserAppService : ApplicationService
    {
        private readonly IUserRepository _userRepository;

        private readonly UserService _userService;

        public UserAppService(IUserRepository userRepository, UserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<UserDto> Authorize(string wxOpenId, Guid? headId)
        {
            var user = await _userRepository.Authorize(wxOpenId, headId);
            return ObjectMapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> wxAuthorize(string nickName, string headImg)
        {
            var user = await _userRepository.wxAuthorize(nickName, headImg);
            return ObjectMapper.Map<User, UserDto>(user);
        }

        /// <summary>
        /// 修改自提点
        /// </summary>
        /// <param name="headId"></param>
        /// <returns></returns>
        public async Task ChangeHead(Guid headId)
        {
            await _userService.ChangeHead(headId);
        }

        /// <summary>
        /// 当前自提点
        /// </summary>
        /// <returns></returns>
        public async Task<HeadDto> CurrentHead()
        {
            var head = await _userService.CurrentHead();
            return ObjectMapper.Map<Head, HeadDto>(head);
        }
    }
}
