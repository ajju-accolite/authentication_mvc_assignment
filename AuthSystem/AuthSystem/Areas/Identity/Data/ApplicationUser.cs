using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthSystem.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string LinkedinProfileUrl { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string InstagramProfileUrl { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string FacebookProfileUrl { get; set; }

        [PersonalData]
        public List<AddProfile>? AddProfiles { get; set; }
    }
}
