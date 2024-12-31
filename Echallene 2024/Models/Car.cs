using System.ComponentModel.DataAnnotations;

namespace Echallene_2024.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
