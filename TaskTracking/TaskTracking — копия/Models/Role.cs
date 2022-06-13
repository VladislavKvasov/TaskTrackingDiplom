using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracking.Models
{
    enum Role
    {
        [Display(Name = "Администратор")]
        ADMIN = 1,

        [Display(Name = "Пользователь")]
        USER = 2
    }
}
