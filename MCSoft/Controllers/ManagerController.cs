using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Dto;
using MCSoft.Application.Dto.Manager;
using MCSoft.Application.Service;
using MCSoft.Domain.Models;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Controllers
{
    public class ManagerController : AbpController
    {
        private readonly ManagerAppService _managerAppService;
        private readonly RoleAppService _roleAppService;
        public ManagerController(ManagerAppService managerAppService, RoleAppService roleAppService)
        {
            _managerAppService = managerAppService;
            _roleAppService = roleAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Item(Guid? id)
        {
            var role = await _roleAppService.GetList();
            var managerRole = new ManagerRoleDto();
            if (id.HasValue)
            {
                managerRole = await _managerAppService.Get(id.Value);
            }
            ViewBag.Role = role;
            return View(managerRole);
        }

        [HttpPost]

        public async Task<IActionResult> Save([FromBody]ManagerSaveDto dto)
        {
            await _managerAppService.SaveAsync(dto);
            return Json(ResultBase.Success());
        }

        public async Task<JsonResult> Search(SearchInput input)
        {
            var paged = await _managerAppService.Search(input);
            return Json(new { code = 0, count = paged.TotalCount, data = paged.Items });
        }

        public async Task<JsonResult> BatchDelete([FromBody]List<Guid> ids)
        {
            await _managerAppService.BatchDeleteAsync(ids);
            return Json(ResultBase.Success());
        }
    }
}