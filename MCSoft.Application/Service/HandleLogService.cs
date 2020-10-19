using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCSoft.Application.Dto;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using Senparc.Weixin.MP.AdvancedAPIs.Semantic;
using Volo.Abp.Application.Services;
using Volo.Abp.MultiTenancy;

namespace MCSoft.Application.Service
{
    public class HandleLogService : ApplicationService
    {
        private readonly IHandleLogsRepository _handleLogsRepository;
        private readonly ICurrentTenant _currentTenant;

        public HandleLogService(IHandleLogsRepository handleLogsRepository, ICurrentTenant currentTenant)
        {
            _handleLogsRepository = handleLogsRepository;
            _currentTenant = currentTenant;
        }

        public async Task AddAsync()
        {
            var handle = new HandleLog("Info", "测试数据", _currentTenant.Id);
            await _handleLogsRepository.InsertAsync(handle);
        }

        public async Task<List<HandleLog>> GetListAsync()
        {
            return await _handleLogsRepository.GetListAsync();
        }
    }
}