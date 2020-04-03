using MCSoft.Domain.Event;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace MCSoft.Domain.Handler
{
    public class MyEventHandler :
          ILocalEventHandler<PersonNameChangedEvent>,
          ILocalEventHandler<EntityCreatedEventData<Person>>,
          ITransientDependency
    {
        public int EntityChangedEventCount { get; set; }
        public int EntityCreatedEventCount { get; set; }

        public Task HandleEventAsync(PersonNameChangedEvent eventData)
        {
            Log.Information("HandleEventAsync");
            EntityChangedEventCount++;
            return Task.CompletedTask;
        }

        public Task HandleEventAsync(EntityCreatedEventData<Person> eventData)
        {
            EntityCreatedEventCount++;
            return Task.CompletedTask;
        }
    }
}
