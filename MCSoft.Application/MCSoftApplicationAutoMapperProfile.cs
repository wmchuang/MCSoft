using AutoMapper;
using MCSoft.Application.Dto;
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

            CreateMap<Person, PersonDto>().ReverseMap();

            CreateMap<Phone, PhoneDto>().ReverseMap();

            CreateMap<Manager, ManagerDto>().ReverseMap();
            CreateMap<ManagerSaveDto, ManagerDto>();
            CreateMap<ManagerSaveDto, ManagerUpdateDto>();
            CreateMap<ManagerUpdateDto, Manager>();


            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleUpdateDto, Role>();
        }
    }
}
