using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarRepairShop.web.Data.Entities
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Appointment date")]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public User User { get; set; }

        public IEnumerable<AppointmentDetail> Items { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity => Items == null ? 0 : Items.Sum(i => i.Quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);
    }
}
