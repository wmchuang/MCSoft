using System;
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
        public async Task<IActionResult> Save([FromBody]HeadSaveDto dto)
        {
            await _headAppService.SaveAsync(dto);
            return Json(ResultBase.Success());
        }
    }
}