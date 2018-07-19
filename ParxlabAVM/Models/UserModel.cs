using Microsoft.AspNet.Identity.EntityFramework;
using ParxlabAVM.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParxlabAVM.Models
{
    //Identity register ve login için kullanılan model 
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RoleEditModel    //http get ile gelecek memberları foreach ile açmak için
    {
        public IdentityRole Role{ get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleUpdateModel //http post ile gelecek güncellemeyi yapmak için
    {   
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
} 