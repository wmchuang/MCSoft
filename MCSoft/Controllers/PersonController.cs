using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCSoft.Application;
using MCSoft.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
using Volo.Abp.AspNetCore.Mvc;

namespace MCSoft.Controllers
{
    public class PersonController : AbpController
    {
        private readonly PeopleAppService _peopleAppService;
        public PersonController(PeopleAppService peopleAppService)
        {
            _peopleAppService = peopleAppService;
        }

        [HttpGet]
        public async Task<JsonResult> Create()
        {
            try
            {
                var uniquePersonName = Guid.NewGuid().ToString();
                await _peopleAppService.CreateAsync(new PersonDto
                {
                    Name = uniquePersonName,
                    Age = 25
                });
            }
            catch (Exception e)
            {
                var s = e.ToString();
            }
            return Json("成功");
        }

        public async Task<JsonResult> Add()
        {
            var person = new Person(Guid.NewGuid(), "王闯", 26);
            await _peopleAppService.Add(person);
            return Json("123");
        }

        public async Task<JsonResult> AddPhone(Guid guid)
        {
            var phoneDto = new PhoneDto()
            {
                Number = "18339831700",
                Type = PhoneType.Mobile
            };
            await _peopleAppService.AddPhone(guid, phoneDto);
            return Json("123");
        }

        public async Task<JsonResult> GetPhone(Guid guid)
        {
            Log.Information("GetPhone");
            var list = await _peopleAppService.GetPhones(guid);
            return Json(list);
        }

        public async Task<JsonResult> ChangeName(Guid guid)
        {
            Log.Information("ChangeName");
            await _peopleAppService.ChangeName(guid);
            return Json("成功");
        }
    }
}