using System;
using System.ComponentModel.DataAnnotations;

namespace CarRepairShop.web.Data.Entities
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Service Service { get; set; }

        [Display(Name = "AppointmentDate")]
        public DateTime? AppointmentDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        public string Observation { get; set; }
    }
}
