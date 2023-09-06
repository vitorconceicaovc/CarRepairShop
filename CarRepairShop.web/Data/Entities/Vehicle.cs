using System.ComponentModel.DataAnnotations;

namespace CarRepairShop.web.Data.Entities
{
    public class Vehicle : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "The field {0} can contain {1} characters length.")]
        [Display(Name = "Car Plate")]
        public string CarPlate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }   

        public int Year { get; set; }       
    }
}
