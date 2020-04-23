using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Dto.Product;
using MCSoft.Application.Service;
using MCSoft.Controllers;
using MCSoft.Domain.Specification;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace MCSoft.Areas.Service.Controllers
{
    [Area("Service")]
    public class ProductController : MCBaseController
    {

        private readonly ProductAppService _productAppService;

        public ProductController(ProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public IActionResult Index(Guid? headId)
        {
            ViewBag.HeadId = headId ?? Guid.Empty;
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Item(Guid? id)
        {
            if (id.HasValue)
            {
                var dto = await _productAppService.GetAsync(id.Value);
                return View(dto);
            }
            return View();
        }


        public async Task<JsonResult> Search(ProductSearchInput input)
        {
            var paged = await _productAppService.Search<ProductDto>(input, new ProductSpecification(input.Keyword, input.HeadId, input.IsEnable));
            return Json(new { code = 0, count = paged.TotalCount, data = paged.Items });
        }

        public async Task<JsonResult> ChangEnable(Guid id, bool status)
        {
            await _productAppService.ChangEnable(id, status);
            return Json(ResultBase.Success());
        }
    }
}