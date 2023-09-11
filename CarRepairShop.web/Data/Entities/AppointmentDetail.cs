using System.ComponentModel.DataAnnotations;

namespace CarRepairShop.web.Data.Entities
{
    public class AppointmentDetail : IEntity
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Vehicle Vehicle { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }

        public decimal Value => Price * (decimal)Quantity;

    }
}
