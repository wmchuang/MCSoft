using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCSoft.Application.Dto;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using Senparc.Weixin.MP.AdvancedAPIs.Semantic;
using Volo.Abp.Application.Services;

namespace MCSoft.Application.Service
{
    public class HandleLogService : ApplicationService
    {
        private readonly IHandleLogsRepository _handleLogsRepository;

        public HandleLogService(IHandleLogsRepository handleLogsRepository)
        {
            _handleLogsRepository = handleLogsRepository;
        }

        public async Task AddAsync()
        {
            var handle = new HandleLog("Info", "测试数据");
            await _handleLogsRepository.InsertAsync(handle);
        }

        public async Task<List<HandleLog>> GetListAsync()
        {
            return await _handleLogsRepository.GetListAsync();
        }
    }
}