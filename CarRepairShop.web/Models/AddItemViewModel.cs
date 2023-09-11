using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRepairShop.web.Models
{
    public class AddItemViewModel
    {
        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a vehicle.")]
        public int VehicleId { get; set; }

        [Display(Name = "Service")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a service.")]
        public int ServiceId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "The quantity must be a positive number.")]
        public double Quantity { get; set; }

        public IEnumerable<SelectListItem> Vehicles { get; set; }

        public IEnumerable<SelectListItem> Services { get; set; }
    }
}

