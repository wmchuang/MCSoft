using MCSoft.Application.Dto.Head;
using MCSoft.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application.Dto.Evaluate
{
    public class EvaluateSearchInputDto : SearchInput
    {

        public Guid HeadId { get; set; }
    }
}
