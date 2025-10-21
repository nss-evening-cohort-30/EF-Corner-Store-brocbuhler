using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Product
{
  public int Id { get; set; }
  [Required]
  public string ProductName { get; set; }
  [Required]
  public decimal Price { get; set; }
  [Required]
  public string brand { get; set; }
  [Required]
  public int CategoryId { get; set; }
  public Category Category { get; set; }
}
