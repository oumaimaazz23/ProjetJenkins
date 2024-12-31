using Echallene_2024.Models;

namespace Echallene_2024.Services
{
    public class CarsService : ICarsService
    {
        public ICollection<Car> Cars;
        public CarsService()
        {
            this.Cars = new List<Car>();
        }

        public ICollection<Car> GetCars() => this.Cars;
        

        public ICollection<Car> GetCarsByMaker(string Maker) => this.Cars.Where(c => c.Maker == Maker).ToList();

        public Car GetCarById(int id) => this.Cars.FirstOrDefault(c => c.Id == id);

        public void Add(Car car)
        {
            this.Cars.Add(car);
        }

        public void Remove(int id)
        {
            Car car = GetCarById(id);
            if (car != null)
            {
                this.Cars.Remove(car);
            }
        }
        public void Update(Car car)
        {
            Car exitedCar = GetCarById(car.Id);
            if (exitedCar != null)
            {
                exitedCar.Year = car.Year;
                exitedCar.Maker = car.Maker;
                exitedCar.Model = car.Model;
            }
        }
    }
}
