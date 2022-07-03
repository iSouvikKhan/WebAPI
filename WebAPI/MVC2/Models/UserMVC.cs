
using System.ComponentModel.DataAnnotations;

namespace MVC2.Models
{
    public class UserMVC
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 characters only")]
        [Display(Name = "Contact Number")]
        public string Contact { get; set; }
    }
}