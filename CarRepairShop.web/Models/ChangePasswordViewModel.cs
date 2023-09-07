using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRepairShop.web.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
    }
}
