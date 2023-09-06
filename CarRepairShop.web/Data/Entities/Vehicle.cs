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

        public string CarModel { get; set; }

        public string Color { get; set; }   

        public int Year { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string ImageFullPath
        {

            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }

                return $"https://localhost:44397{ImageUrl.Substring(1)}";
            }

        }

        public User User { get; set; }
    }
}
