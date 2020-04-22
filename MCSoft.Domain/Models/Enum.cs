using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCSoft.Domain.Models
{
    public enum Status
    {
        [Display(Name = "启用")]
        Enable = 1,
        [Display(Name = "禁用")]
        Prohibit = 0,
      
    }
}
