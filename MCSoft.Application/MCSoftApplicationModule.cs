using MCSoft.Application.Dto;
using MCSoft.Domain;
using MCSoft.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MCSoft.Application
{
    [DependsOn(typeof(MCSoftDomainModule))]
    [DependsOn(typeof(MCSoftInfrastructureModule))]
    public class MCSoftApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                //options.Configurators.Add(ctx =>
                //{
                //    ctx.MapperConfiguration.CreateMap<Person, PersonDto>().ReverseMap();
                //    ctx.MapperConfiguration.CreateMap<Phone, PhoneDto>().ReverseMap();
                //    ctx.MapperConfiguration.CreateMap<Test, TestDto>().ReverseMap();
                //});
                options.AddProfile<MCSoftApplicationAutoMapperProfile>();
                options.AddMaps<MCSoftApplicationModule>();
            });
        }
    }
}
