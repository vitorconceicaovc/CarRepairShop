using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRepairShop.web.Models
{
    public class CityViewModel
    {
        public int CountryId { get; set; }

        public int CityId { get; set; }

        [Required]
        [Display(Name = "City")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characteres.")]
        public string Name { get; set; }
    }
}
