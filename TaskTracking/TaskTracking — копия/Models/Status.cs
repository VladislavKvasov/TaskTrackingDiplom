using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracking.Models
{
    internal enum Status
    {
        [Display(Name = "Беклог")]
        BACKLOG = 1,

        [Display(Name = "В работе")]
        IN_WORK = 2,

        [Display(Name = "Завершена")]
        READY = 3,

        [Display(Name = "Отменена")]
        CANCEL = 4
    }
}
