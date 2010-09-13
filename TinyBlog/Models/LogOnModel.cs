using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TinyBlog.Models
{
    public class LogOnModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}