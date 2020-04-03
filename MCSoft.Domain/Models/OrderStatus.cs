using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MCSoft.Domain.Models
{
    public enum OrderStatus
    {
        [Display(Name = "待付款")]
        NoPay = 0,
        [Display(Name = "待发货")]
        Payed = 1,
        [Display(Name = "配送中")]
        Delivered = 2,
        [Display(Name = "待取货")]
        WaitingTaken = 3,
        [Display(Name = "已完成")]
        Finish = 4
    }
}
