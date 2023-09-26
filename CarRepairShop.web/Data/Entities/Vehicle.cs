using System;
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
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty

           ? "https://carrepairshopweb.azurewebsites.net/images/noimage.jpg"
           : $"https://carrepairshopcontainer.blob.core.windows.net/vehicles/{ImageId}";

        public User User { get; set; }
    }
}
