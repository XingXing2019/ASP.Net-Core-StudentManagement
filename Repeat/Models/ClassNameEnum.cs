using System.ComponentModel.DataAnnotations;

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
