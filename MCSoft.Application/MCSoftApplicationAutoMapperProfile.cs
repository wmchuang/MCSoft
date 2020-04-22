using AutoMapper;
using MCSoft.Application.Dto;
using MCSoft.Application.Dto.Head;
using MCSoft.Application.Dto.Product;
using MCSoft.Application.Dto.User;
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

            CreateMap<Manager, ManagerDto>().ReverseMap();
            CreateMap<ManagerSaveDto, ManagerDto>();
            CreateMap<ManagerSaveDto, ManagerUpdateDto>();
            CreateMap<ManagerUpdateDto, Manager>();


            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleUpdateDto, Role>();

            #endregion

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserSimpleDto>().ReverseMap();

            CreateMap<Head, HeadDto>().ForMember(d => d.UserDto, opt =>
            {
                opt.MapFrom(s => s.User);
            }).ReverseMap();
            CreateMap<HeadSaveDto, HeadDto>();
            CreateMap<HeadSaveDto, HeadUpdateDto>();
            CreateMap<HeadUpdateDto, Head>();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductSaveDto, ProductDto>();
            CreateMap<ProductSaveDto, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, SmallProductDto>();

        }
    }
}
