using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application.Dto.Head
{
    public class HeadSaveDto : HeadUpdateDto
    {
        public new Guid? Id { get; set; }
    }
}
