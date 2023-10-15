using System.ComponentModel.DataAnnotations;
using CarRepairShop.web.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CarRepairShop.web.Models
{
    public class VehicleViewModel : Vehicle
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }

}
