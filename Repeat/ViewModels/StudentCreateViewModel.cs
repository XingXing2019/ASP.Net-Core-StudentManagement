using Microsoft.AspNetCore.Http;
using Repeat.Models;
using System.ComponentModel.DataAnnotations;

namespace Repeat.ViewModels
{
    public class StudentCreateViewModel
    {
        [Required(ErrorMessage = "Please enter first name"), MaxLength(50, ErrorMessage = "Name length cannot over 50")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name"), MaxLength(50, ErrorMessage = "Name length cannot over 50")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-z0-9-]+\.[a-zA-z0-9-.]+$", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select class name")]
        [Display(Name = "Class Name")]
        public ClassNameEnum? ClassName { get; set; }

        [Required(ErrorMessage = "Please select photo")]
        [Display(Name = "Photo")]
        public IFormFile Photo { get; set; }
    }
}
