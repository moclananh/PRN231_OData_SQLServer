
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestClient.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public decimal Price { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

    }
}
