using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
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
    /// 商品
    /// </summary>
    public class ProductController : MCApiBaseController
    {

        private readonly ProductAppService _productAppService;

        public ProductController(ProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]ProductSaveDto dto)
        {
            await _productAppService.SaveAsync(dto);
            return Json(ResultBase.Success());
        }


        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid productId)
        {
            var result = new Result<ProductDto>()
            {
                data = await _productAppService.GetAsync(productId)
            };
            return Json(result);
        }

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Search(ProductSearchInput input)
        {
            var result = new Result<PagedResultDto<SmallProductDto>>();
            result.data = await _productAppService.SearchApi(input, new ProductSpecification(input.Keyword, input.HeadId, input.IsEnable));
            return Json(result);
        }

        /// <summary>
        /// 上下架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> ChangEnable(Guid id, bool status)
        {
            await _productAppService.ChangEnable(id, status);
            return Json(ResultBase.Success());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> Delete(Guid id)
        {
            await _productAppService.DeleteAsync(id);
            return Json(ResultBase.Success());
        }
    }
}