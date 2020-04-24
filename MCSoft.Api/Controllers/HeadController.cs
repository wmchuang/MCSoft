﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application.Dto.Head;
using MCSoft.Application.Service;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;


namespace MCSoft.Api.Controllers
{
    public class HeadController : MCApiBaseController
    {
        private readonly HeadAppService _headAppService;

        public HeadController(HeadAppService headAppService)
        {
            _headAppService = headAppService;
        }

        /// <summary>
        /// 申请成为团长
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<HeadDto>> Save([FromBody]HeadSaveDto dto)
        {
            var result = new Result<HeadDto>
            {
                data = await _headAppService.SaveAsync(dto)
            };
            return result;
        }


        /// <summary>
        /// 获取我的店铺
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<HeadDto>> Get()
        {
            var result = new Result<HeadDto>
            {
                data = await _headAppService.GetAsync()
            };
            return result;
        }

        /// <summary>
        /// 获取是不是店铺
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result<bool> IsHead()
        {
            var result = new Result<bool>
            {
                data =  _headAppService.IsHead()
            };
            return result;
        }
    }
}