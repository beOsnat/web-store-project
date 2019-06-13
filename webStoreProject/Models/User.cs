using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreProject.Models
{
   
    public class User
    {
        [HiddenInput]
        public int UserId { get; set; }
        [Required (ErrorMessage ="Please enter firstame")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Please enter lastname")]
        public string LastName { get; set; }
        [Required]
   
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Re-type Password")]
        [Compare(nameof(Password), ErrorMessage ="Please Type Again")]
        public string PasswordConfirm { get; set; }


    }




}
