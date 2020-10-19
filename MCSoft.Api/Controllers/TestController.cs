using System.Collections.Generic;
using System.Threading.Tasks;
using MCSoft.Application.Service;
using MCSoft.Domain.Models;
using MCSoft.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace MCSoft.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : AbpController
    {
        private readonly HandleLogService _handleLogService;

        public TestController(HandleLogService handleLogService)
        {
            _handleLogService = handleLogService;
        }
        
        [HttpGet]
        public async Task<IActionResult> AddAsync()
        {
            await _handleLogService.AddAsync();
            return Json(ResultBase.Success());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var data = await _handleLogService.GetListAsync();
            return Json(Result<List<HandleLog>>.Success(data));
        }
    }
}