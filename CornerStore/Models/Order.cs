
using System.ComponentModel.DataAnnotations;

namespace CornerStore.Models;

public class Order
{
  public int Id { get; set; }
  [Required]
  public int CashierId { get; set; }
  public Cashier cashier { get; set; }
  public int Total { get; set; } // must compute
  public DateTime? PaidOnDate { get; set; }

  public List<OrderProduct> OrderProducts { get; set; }
}
