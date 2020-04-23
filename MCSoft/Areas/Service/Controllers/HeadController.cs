using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Dto.Head;
using MCSoft.Application.Service;
using MCSoft.Controllers;
using MCSoft.Domain.Models;
using MCSoft.Domain.Specification;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace MCSoft.Areas.Service.Controllers
{
    [Area("Service")]
    public class HeadController : MCBaseController
    {
        private readonly HeadAppService _headAppService;

        public HeadController(HeadAppService headAppService)
        {
            _headAppService = headAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Search(SearchInput input)
        {
            var paged = await _headAppService.Search<HeadDto>(input, new HeadSpecification(input.Keyword));
            return Json(new { code = 0, count = paged.TotalCount, data = paged.Items });
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="headId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<JsonResult> ChangeStatus(Guid headId, Status status)
        {
            await _headAppService.ChangeStatus(headId, status);
            return Json(ResultBase.Success());
        }
    }
}