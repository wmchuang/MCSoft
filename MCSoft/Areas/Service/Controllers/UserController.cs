using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Application.Service;
using MCSoft.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MCSoft.Areas.Service.Controllers
{
    [Area("Service")]
    public class UserController : MCBaseController
    {
        private readonly UserAppService _userAppService;

        public UserController(UserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Search(SearchInput input)
        {
            var paged = await _userAppService.Search(input);
            return Json(new { code = 0, count = paged.TotalCount, data = paged.Items });
        }
    }
}