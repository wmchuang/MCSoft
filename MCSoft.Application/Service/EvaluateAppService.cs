using MCSoft.Application.Dto.Evaluate;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MCSoft.Application.Service
{
    public class EvaluateAppService : ApplicationService
    {

        private readonly IEvaluateRepository _repository;

        public EvaluateAppService(IEvaluateRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(EvaluateAddInputDto dto)
        {
            var userId = CurrentUser.Id.Value;
            var evaluate = new Evaluate(userId, dto.HeadId, dto.Content);
            await _repository.InsertAsync(evaluate);
        }

        public async Task<PagedResultDto<EvaluateDto>> Search(SearchInput dto, ISpecification<Evaluate> spec)
        {

            var repository = _repository.QueryInclude().Where(spec.ToExpression());

            var count = await repository.CountAsync();

            var list = await repository
                .OrderByDescending(g => g.CreationTime)
                .PageBy(dto)
                .ToListAsync();


            var items = ObjectMapper.Map<List<Evaluate>, List<EvaluateDto>>(list);

            return new PagedResultDto<EvaluateDto>
            {
                TotalCount = count,
                Items = items
            };
        }

        public async Task Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
