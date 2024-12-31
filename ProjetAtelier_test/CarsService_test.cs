using Echallene_2024.Models;
using Echallene_2024.Services;
using Xunit;

namespace ProjetAtelier_test
{
    public class CarsService_test
    {
        private readonly CarsService _service;

        public CarsService_test()
        {
            _service = new CarsService();
        }

        [Fact]
        public void GetCars_ShouldReturnEmptyList_WhenNoCarsAdded()
        {
            // Act
            var result = _service.GetCars();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Add_ShouldAddCarToList()
        {
            // Arrange
            var car = new Car { Id = 1, Maker = "Renault", Model = "Clio", Year = 2020 };

            // Act
            _service.Add(car);

            // Assert
            Assert.Single(_service.GetCars());
            Assert.Contains(car, _service.GetCars());
        }

        [Fact]
        public void GetCarById_ShouldReturnCar_WhenCarExists()
        {
            // Arrange
            var car = new Car { Id = 1, Maker = "Renault", Model = "Clio", Year = 2020 };
            _service.Add(car);

            // Act
            var result = _service.GetCarById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(car, result);
        }

        [Fact]
        public void GetCarById_ShouldReturnNull_WhenCarDoesNotExist()
        {
            // Act
            var result = _service.GetCarById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Remove_ShouldRemoveCar_WhenCarExists()
        {
            // Arrange
            var car = new Car { Id = 1, Maker = "Renault", Model = "Clio", Year = 2020 };
            _service.Add(car);

            // Act
            _service.Remove(1);

            // Assert
            Assert.Empty(_service.GetCars());
        }

        [Fact]
        public void Remove_ShouldNotRemoveCar_WhenCarDoesNotExist()
        {
            // Arrange
            var car = new Car { Id = 1, Maker = "Renault", Model = "Clio", Year = 2020 };
            _service.Add(car);

            // Act
            _service.Remove(2);

            // Assert
            Assert.Single(_service.GetCars());
            Assert.Contains(car, _service.GetCars());
        }

        [Fact]
        public void Update_ShouldUpdateCar_WhenCarExists()
        {
            // Arrange
            var car = new Car { Id = 1, Maker = "Renault", Model = "Clio", Year = 2020 };
            _service.Add(car);

            var updatedCar = new Car { Id = 1, Maker = "Renault", Model = "Megane", Year = 2022 };

            // Act
            _service.Update(updatedCar);

            // Assert
            var result = _service.GetCarById(1);
            Assert.Equal("Megane", result.Model);
            Assert.Equal(2022, result.Year);
        }

        [Fact]
        public void Update_ShouldNotUpdateCar_WhenCarDoesNotExist()
        {
            // Arrange
            var car = new Car { Id = 1, Maker = "Renault", Model = "Clio", Year = 2020 };

            // Act
            _service.Update(car);

            // Assert
            Assert.Empty(_service.GetCars());
        }
    }
}
