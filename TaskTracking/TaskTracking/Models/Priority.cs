using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracking.Models
{
    public enum Priority
    {
        [Display(Name = "Низкий")]
        LOW = 1,

        [Display(Name = "Нормальный")]
        NORMAL = 2,

        [Display(Name = "Срочный")]
        HIGH = 3

    }
}
