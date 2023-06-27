using System.ComponentModel.DataAnnotations;

namespace CarRepairShop.web.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Display(Name = "Car Plate")]
        public string CarPlate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }   

        public int Year { get; set; }       
    }
}
