using MCSoft.Application.Dto.Product;
using MCSoft.Domain.IRepository;
using MCSoft.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
    public class ProductAppService : CrudAppService<Product, ProductDto, Guid, SearchInput, ProductDto, ProductUpdateDto>
    {

        private ICurrentUser _currentUser { get; }
        public ProductAppService(IRepository<Product, Guid> repository, ICurrentUser currentUser)
           : base(repository)
        {
            _currentUser = currentUser;
        }

        public async Task<PagedResultDto<T>> Search<T>(SearchInput dto, ISpecification<Product> spec)
        {
            try
            {
                var repository = Repository.Where(spec.ToExpression());

                var count = await repository.CountAsync();

                var list = await repository
                    .OrderByDescending(g => g.CreationTime)
                    .PageBy(dto)
                    .ToListAsync();

                var items = ObjectMapper.Map<List<Product>, List<T>>(list);

                return new PagedResultDto<T>
                {
                    TotalCount = count,
                    Items = items
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<PagedResultDto<SmallProductDto>> SearchApi(SearchInput dto, ISpecification<Product> spec)
        {

            var repository = Repository.Where(spec.ToExpression());

            var count = await repository.CountAsync();

            var list = await repository
                .OrderByDescending(g => g.CreationTime)
                .PageBy(dto)
                .ToListAsync();


            var items = ObjectMapper.Map<List<Product>, List<SmallProductDto>>(list);

            return new PagedResultDto<SmallProductDto>
            {
                TotalCount = count,
                Items = items
            };

        }

        public async Task<ProductDto> SaveAsync(ProductSaveDto input)
        {
            ProductDto dto;
            try
            {
                if (input.Id.HasValue)
                {
                    var updateDto = ObjectMapper.Map<ProductSaveDto, ProductUpdateDto>(input);
                    dto = await base.UpdateAsync(input.Id.Value, updateDto);
                }
                else
                {
                    var userId = _currentUser.Id.Value;
                    var productDto = ObjectMapper.Map<ProductSaveDto, ProductDto>(input);
                    productDto.HeadId = userId;
                    productDto.IsEnable = true;
                    productDto.ContentImg = productDto.ContentImg.TrimEnd(',');
                    dto = await base.CreateAsync(productDto);
                }
                return dto;
            }
            catch (Exception e)
            {
                Log.Error(e.StackTrace);
                throw new BusinessException("提交失败！");
            }
        }

        /// <summary>
        /// 商品上下架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task ChangEnable(Guid id, bool status)
        {
            var product = await Repository.GetAsync(x => x.Id == id);

            if (status)
                product.Enable();
            else
                product.Disable();

            await Repository.UpdateAsync(product);
        }
    }
}
