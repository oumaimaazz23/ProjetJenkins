using Echallene_2024.Models;
using Echallene_2024.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Echallene_2024.Controllers
{
    [Route("atelier")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarsService service { get; set; }
        public CarsController(ICarsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            ICollection<Car> list = service.GetCars();
            if (list.Count == 0)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("{id}")] // /GetCar/5
        public IActionResult GetCar(int id)
        {
            Car car = service.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
        [HttpGet("search")] // /search?Renault
        public IActionResult GetCar([FromQuery] string maker)
        {
            ICollection<Car> cars = service.GetCarsByMaker(maker);
            if (cars.Count == 0)
            {
                return NotFound();
            }
            return Ok(cars);
        }

        [HttpPost]
        public IActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid) // Annotations
            {
                return UnprocessableEntity();
            }
            service.Add(car);
            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public IActionResult PutCar(int id,Car car)
        {
            if(id != car.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) { 
                return UnprocessableEntity();
            }
            service.Update(car);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            if (service.GetCarById(id) == null)
            {
                return NotFound();
            }
            service.Remove(id);
            return NoContent();
        }

    }
}

// MiddleWare pour la gestion des Ecepeiton Générale try catch() { 500 }

// Tests Unitaire XUnit .. 