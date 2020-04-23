using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Dto.Evaluate;
using MCSoft.Application.Service;
using MCSoft.Controllers;
using MCSoft.Domain.Specification;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace MCSoft.Areas.Service.Controllers
{
    [Area("Service")]
    public class EvaluateController : MCBaseController
    {
        private readonly EvaluateAppService _evaluateAppService;

        public EvaluateController(EvaluateAppService evaluateAppService)
        {
            _evaluateAppService = evaluateAppService;
        }

        public IActionResult Index(Guid? headId)
        {
            ViewBag.HeadId = headId ?? Guid.Empty;
            return View();
        }

        public async Task<JsonResult> Search(EvaluateSearchInputDto input)
        {
            var paged = await _evaluateAppService.Search(input, new EvaluateSpecification(input.HeadId));
            return Json(new { code = 0, count = paged.TotalCount, data = paged.Items });
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            await _evaluateAppService.Delete(id);
            return Json(ResultBase.Success());
        }
    }
}