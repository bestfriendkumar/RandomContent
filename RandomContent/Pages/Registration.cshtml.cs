using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RandomContent.Entities;

namespace RandomContent.Pages
{
    public class RegistrationModel : PageModel
    {
        //public string Message { get; set; }

        public void OnGet()
        {
            //Message = "Fill out the form below to be registered with the application.";
        }

        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role")]
        public Role Role { get; set; }
    }
}
