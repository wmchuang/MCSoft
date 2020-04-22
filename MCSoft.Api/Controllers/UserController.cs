using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application.Dto.Head;
using MCSoft.Application.Service;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace MCSoft.Api.Controllers
{
    public class UserController : MCApiBaseController
    {
        private readonly UserAppService _userAppService;

        public UserController(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 切换团长/自提点
        /// </summary>
        /// <param name="headId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultBase> ChangeHead(Guid headId)
        {
            await _userAppService.ChangeHead(headId);
            return ResultBase.Success();
        }

        /// <summary>
        /// 用户当前自提点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<HeadDto>> CurrentHead()
        {
            var result = new Result<HeadDto>()
            {
                data = await _userAppService.CurrentHead()
            };
            return result;
        }

    }
}