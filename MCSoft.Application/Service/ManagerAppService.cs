using MCSoft.Application.Dto;
using MCSoft.Application.Dto.Manager;
using MCSoft.Domain.Models;
using MCSoft.Domain.Service;
using MCSoft.Infrastructure.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Application.Service
{
    public class ManagerAppService : CrudAppService<Manager, ManagerDto, Guid, ManagerUpdateDto, ManagerDto, ManagerUpdateDto>
    {
        private readonly ManagerRoleService _managerRoleService;
        public ManagerAppService(IRepository<Manager, Guid> repository, ManagerRoleService managerRoleService)
        : base(repository)
        {
            _managerRoleService = managerRoleService;
        }

        public async Task<ManagerRoleDto> Get(Guid guid)
        {
            var manager = await Repository.GetAsync(guid);
            var roleList = _managerRoleService.GetManagerRole(manager);
            return new ManagerRoleDto
            {
                ManagerDto = ObjectMapper.Map<Manager, ManagerDto>(manager),
                RoleList = ObjectMapper.Map<List<Role>, List<RoleDto>>(roleList.ToList()),
            };
        }

        public async Task<ManagerDto> SaveAsync(ManagerSaveDto input)
        {
            ManagerDto dto;
            if (input.Id.HasValue)
            {
                var manager = ObjectMapper.Map<ManagerSaveDto, ManagerUpdateDto>(input);
                dto = await base.UpdateAsync(input.Id.Value, manager);
            }
            else
            {

                if (Repository.FirstOrDefault(x => x.UserName == input.UserName) != null)
                {
                    throw new BusinessException("已经存在此用户名,添加失败！");
                }

                var manager = ObjectMapper.Map<ManagerSaveDto, ManagerDto>(input);
                dto = await base.CreateAsync(manager);
            }

            await _managerRoleService.AddRoleToManager(dto.Id, input.RoleIds.ToList());
            return dto;
        }

        public async Task BatchDeleteAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                await base.DeleteAsync(id);
            }
        }

        public async Task<PagedResultDto<ManagerDto>> Search(SearchInput dto)
        {
            var repository = Repository.WhereIf(!string.IsNullOrWhiteSpace(dto.Keyword), x => x.UserName.Contains(dto.Keyword));

            var query = await repository.CountAsync();

            var list = await repository
                .OrderByDescending(g => g.CreationTime)
                .PageBy(dto)
                .ToListAsync();

            var items = ObjectMapper.Map<List<Manager>, List<ManagerDto>>(list);
            return new PagedResultDto<ManagerDto>
            {
                TotalCount = query,
                Items = items
            };
        }

        public async Task<Result<ManagerDto>> Login(string userName, string password)
        {
            var manager = await Repository.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (manager == null)
            {
                return Result<ManagerDto>.Error("登录失败，用户名无效!");
            }
            if (!manager.VerifyPassword(password))
            {
                return Result<ManagerDto>.Error("登录失败，密码错误!");
            }
            return Result<ManagerDto>.Success(ObjectMapper.Map<Manager, ManagerDto>(manager));
        }

        public async Task<IEnumerable<Menu>> GetCurrentManagerMenuAsync()
        {
            return await _managerRoleService.GetCurrentManagerMenuAsync();
        }
    }
}
