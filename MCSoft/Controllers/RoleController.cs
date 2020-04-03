using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Dto;
using MCSoft.Application.Dto.Role;
using MCSoft.Application.Service;
using MCSoft.Infrastructure;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MCSoft.Controllers
{
    public class RoleController : AbpController
    {
        private readonly RoleAppService _roleAppService;
        public RoleController(RoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Item(Guid? id)
        {
            var menu = MenuConfigSingle.CreatInstance().GetMenu();
            var roleMenuDto = new RoleMenuDto();
            if (id.HasValue)
            {
                roleMenuDto = await _roleAppService.Get(id.Value);
            }
            ViewBag.Menu = menu;
            return View(roleMenuDto);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]RoleSaveDto dto)
        {
            if (dto.Id.HasValue && dto.Id.Value != Guid.Empty)
            {
                await _roleAppService.UpdateAsync(dto);
            }
            else
            {
                await _roleAppService.CreateAsync(dto);
            }
            return Json(ResultBase.Success());
        }

        public async Task<JsonResult> Search(SearchInput input)
        {
            var paged = await _roleAppService.Search(input);
            return Json(new { code = 0, count = paged.TotalCount, data = paged.Items });
        }

    }
}