using MCSoft.Application.Dto.Head;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using MCSoft.Infrastructure.Repository;
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
using Volo.Abp.Specifications;
using Volo.Abp.Users;

namespace MCSoft.Application.Service
{
    public class HeadAppService : CrudAppService<Head, HeadDto, Guid, SearchInput, HeadDto, HeadUpdateDto>
    {
        private ICurrentUser _currentUser { get; }

        private readonly IHeadRepository _headRepository;
        private readonly IUserRepository _userRepository;

        public HeadAppService(IRepository<Head, Guid> repository, ICurrentUser currentUser, IHeadRepository headRepository, IUserRepository userRepository)
         : base(repository)
        {
            _currentUser = currentUser;
            _headRepository = headRepository;
            _userRepository = userRepository;
        }

        public async Task<HeadDto> SaveAsync(HeadSaveDto input)
        {

            HeadDto dto;

            if (input.Id.HasValue)
            {
                var updateDto = ObjectMapper.Map<HeadSaveDto, HeadUpdateDto>(input);
                dto = await base.UpdateAsync(input.Id.Value, updateDto);
            }
            else
            {
                var headDto = ObjectMapper.Map<HeadSaveDto, HeadDto>(input);
                var userId = _currentUser.Id.Value;
                if (Repository.FirstOrDefault(x => x.User.Id == userId) != null)
                {
                    throw new BusinessException("您已提交过信息，不必重复提交");
                }

                //设置为团长Id和用户Id一样
                headDto.Id = userId;
                headDto.HeadStatus = Status.Enable;

                await CheckCreatePolicyAsync();
                var entity = MapToEntity(headDto);
                TryToSetTenantId(entity);
                entity.User = await _userRepository.GetAsync(x => x.Id == userId);
                await Repository.InsertAsync(entity, autoSave: true);

                return MapToGetOutputDto(entity);
            }
            return dto;

        }

        public async Task<PagedResultDto<T>> Search<T>(SearchInput dto, ISpecification<Head> spec)
        {
            try
            {
                var repository = Repository.Where(spec.ToExpression());

                var count = await repository.CountAsync();

                var list = await repository
                    .OrderByDescending(g => g.CreationTime)
                    .PageBy(dto)
                    .ToListAsync();

                var items = ObjectMapper.Map<List<Head>, List<T>>(list);

                return new PagedResultDto<T>
                {
                    TotalCount = count,
                    Items = items
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task ChangeStatus(Guid headId, Status status)
        {
            await _headRepository.ChangeStatus(headId, status);
        }

        public async Task<HeadDto> GetAsync()
        {
            var headId = _currentUser.Id.Value;
            var head = await _headRepository.GetIncludeAsync(headId);
            var headDto = ObjectMapper.Map<Head, HeadDto>(head);
            headDto.FansCount = _userRepository.GetHeadFansCount(headId);
            return headDto;
        }

        public bool IsHead()
        {
            var headId = _currentUser.Id.Value;
            var head =  _headRepository.FirstOrDefault(x => x.Id == headId);
            return head != null;
        }
    }
}
