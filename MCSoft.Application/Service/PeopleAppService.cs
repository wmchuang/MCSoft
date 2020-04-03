using MCSoft.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MCSoft.Application
{
    public class PeopleAppService : CrudAppService<Person, PersonDto, Guid>
    {
        public PeopleAppService(IRepository<Person, Guid> repository)
            : base(repository)
        {

        }



        public async Task<ListResultDto<PhoneDto>> GetPhones(Guid id)
        {
            var phones = (await GetEntityByIdAsync(id)).Phones
                .ToList();

            return new ListResultDto<PhoneDto>(
                ObjectMapper.Map<List<Phone>, List<PhoneDto>>(phones)
            );
        }

        public async Task<PhoneDto> AddPhone(Guid id, PhoneDto phoneDto)
        {
            try
            {
                var person = await Repository.GetAsync(id);
                var phone = new Phone(person.Id, phoneDto.Number, phoneDto.Type);

                person.Phones.Add(phone);
                await Repository.UpdateAsync(person);
                return ObjectMapper.Map<Phone, PhoneDto>(phone);
            }
            catch (Exception e)
            {
                var m = e.Message;
                return new PhoneDto();
            }
        }

        public async Task RemovePhone(Guid id, string number)
        {
            var person = await GetEntityByIdAsync(id);
            person.Phones.RemoveAll(p => p.Number == number);
            await Repository.UpdateAsync(person);
        }

        public async Task Add(Person person)
        {
            await Repository.InsertAsync(person);
        }

        public async Task ChangeName(Guid id)
        {
            var person = await GetEntityByIdAsync(id);
            string name = "沫初";
            person.ChangeName(name);
        }

        public Task GetWithAuthorized()
        {
            throw new NotImplementedException();
        }

        //[Authorize]
        //public Task GetWithAuthorized()
        //{
        //    return Task.CompletedTask;
        //}

    }
}
