using System.Linq;
using System.Threading.Tasks;
using CarRepairShop.web.Data.Entities;

namespace CarRepairShop.web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Vehicles.Any())
            {
                AddVehicle("28-24-GH", "Opel", "Corsa", "Yellow", 2003);
                AddVehicle("23-VH-90", "Nisan", "Susano", "Red", 2006);
                AddVehicle("12-29-TT", "Mercedes", "Ben10", "Gray", 2012);
                AddVehicle("21-FF-R3", "Trator", "Supra", "Black", 1995);
                AddVehicle("63-29-Y7", "Granda", "Carro", "Green", 2022);

                await _context.SaveChangesAsync();
            }
        }

        private void AddVehicle(string plate, string brand, string model, string color, int year)
        {
            _context.Vehicles.Add(new Vehicle
            {
                CarPlate = plate,
                Brand = brand,
                Model = model,
                Color = color,
                Year = year
            });
        }
    }
}
