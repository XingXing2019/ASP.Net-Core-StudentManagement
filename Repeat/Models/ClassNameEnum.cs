using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repeat.Models
{
    public enum ClassNameEnum
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Year One")]
        YearOne,
        [Display(Name = "Year Two")]
        YearTwo,
        [Display(Name = "Year Three")]
        YearThree
    }
}
