using Echallene_2024.Models;

namespace Echallene_2024.Services
{
    public interface ICarsService
    {
        public ICollection<Car> GetCars();
        public ICollection<Car> GetCarsByMaker(string Maker);
        public Car GetCarById(int id);
        public void Add(Car car);
        public void Remove(int id);
        public void Update(Car car);
    }
}
