using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TinyBlog.Objects;

namespace TinyBlog.Models
{
    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public class SetupViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }

        [Required]
        public string SiteTitle { get; set; }

        public string AlternateFeedUrl { get; set; }
        public string PostFooterScript { get; set; }
        public string PageFooterScript { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }

    }
}