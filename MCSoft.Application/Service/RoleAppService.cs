using MCSoft.Application.Dto;
using MCSoft.Application.Dto.Role;
using MCSoft.Domain.Models;
using MCSoft.Domain.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Application.Service
{
    public class RoleAppService : CrudAppService<Role, RoleDto, Guid, RoleDto, RoleDto, RoleUpdateDto>
    {
        private readonly RoleMenuService _roleMenuService;
        public RoleAppService(IRepository<Role, Guid> repository, RoleMenuService roleMenuService)
     : base(repository)
        {
            _roleMenuService = roleMenuService;

        }

        public async Task<List<RoleDto>> GetList()
        {
            var roleList = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Role>, List<RoleDto>>(roleList);
        }

        public async Task<PagedResultDto<RoleDto>> Search(SearchInput dto)
        {
            var repository = Repository.WhereIf(!string.IsNullOrWhiteSpace(dto.Keyword), x => x.Name.Contains(dto.Keyword));

            var query = await repository.CountAsync();

            var list = await repository
                .OrderByDescending(g => g.CreationTime)
                .PageBy(dto)
                .ToListAsync();

            var items = ObjectMapper.Map<List<Role>, List<RoleDto>>(list);
            return new PagedResultDto<RoleDto>
            {
                TotalCount = query,
                Items = items
            };
        }

        public async Task<RoleMenuDto> Get(Guid guid)
        {
            var role = await Repository.GetAsync(guid);
            var roleMenu = _roleMenuService.GetRoleMenu(role);
            return new RoleMenuDto
            {
                RoleDto = ObjectMapper.Map<Role, RoleDto>(role),
                MenuNames = roleMenu.Select(x => x.Name).ToList()
            };
        }
        public async Task CreateAsync(RoleSaveDto roleSaveDto)
        {
            var roleDto = new RoleDto
            {
                Name = roleSaveDto.Name,
                Description = roleSaveDto.Desc
            };
            if ((await Repository.FindAsync(x => x.Name == roleDto.Name)) != null)
            {
                throw new BusinessException("已存在相同名称的角色名，提交失败");
            }
            var dto = await base.CreateAsync(roleDto);
            var role = ObjectMapper.Map<RoleDto, Role>(dto);
            await _roleMenuService.AddPermissionsToRole(role, roleSaveDto.MenuNames.ToList());
        }

        public async Task UpdateAsync(RoleSaveDto roleSaveDto)
        {
            var roleDto = new RoleUpdateDto
            {
                Id = roleSaveDto.Id.Value,
                Name = roleSaveDto.Name,
                Description = roleSaveDto.Desc
            };

            var dto = await base.UpdateAsync(roleSaveDto.Id.Value, roleDto);
            var role = ObjectMapper.Map<RoleDto, Role>(dto);
            await _roleMenuService.AddPermissionsToRole(role, roleSaveDto.MenuNames.ToList());
        }
    }
}
