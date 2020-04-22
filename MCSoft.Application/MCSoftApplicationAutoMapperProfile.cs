using AutoMapper;
using MCSoft.Application.Dto;
using MCSoft.Application.Dto.Category;
using MCSoft.Application.Dto.Product;
using MCSoft.Domain;
using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application
{
    public class MCSoftApplicationAutoMapperProfile : Profile
    {
        public MCSoftApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            #region RBAC
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();

            CreateMap<Manager, ManagerDto>().ReverseMap();
            CreateMap<ManagerSaveDto, ManagerDto>();
            CreateMap<ManagerSaveDto, ManagerUpdateDto>();
            CreateMap<ManagerUpdateDto, Manager>();


            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleUpdateDto, Role>();

            #endregion

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategorySaveDto, CategoryDto>();
            CreateMap<CategorySaveDto, CategoryUpdateDto>();
            CreateMap<CategoryUpdateDto, Category>();


            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductSaveDto, ProductDto>();
            CreateMap<ProductSaveDto, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>();

        }
    }
}
