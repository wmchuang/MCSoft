using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Dto.Evaluate;
using MCSoft.Application.Dto.Product;
using MCSoft.Application.Service;
using MCSoft.Domain.Models;
using MCSoft.Domain.Specification;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Api.Controllers
{
    /// <summary>
    /// 评价
    /// </summary>
    public class EvaluateController : MCApiBaseController
    {

        private readonly EvaluateAppService _evaluateAppService;

        public EvaluateController(EvaluateAppService evaluateAppService)
        {
            _evaluateAppService = evaluateAppService;
        }

        /// <summary>
        /// 添加评价
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EvaluateAddInputDto dto)
        {
            await _evaluateAppService.Add(dto);
            return Json(ResultBase.Success());
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(EvaluateSearchInputDto input)
        {
            var result = new Result<PagedResultDto<EvaluateDto>>();
            result.data = await _evaluateAppService.Search(input, new EvaluateSpecification(input.HeadId));
            return Json(result);
        }
    }
}