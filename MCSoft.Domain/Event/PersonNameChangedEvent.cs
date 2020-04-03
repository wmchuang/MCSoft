using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Domain.Event
{
    public class PersonNameChangedEvent
    {
        public Person Person { get; set; }

        public string OldName { get; set; }
    }
}
