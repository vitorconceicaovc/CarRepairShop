using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarRepairShop.web.Data.Entities
{
    public class Service : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

    }
}
