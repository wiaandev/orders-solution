using System.ComponentModel.DataAnnotations.Schema;
using assessment.Domain.Enum;
using RefactoringExercise.Enum;

namespace assessment.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }

    public decimal Total { get; set; }

    public OrderStatus Status { get; set; }

    public PayMethod PaymentMethod { get; set; }
    
    public List<Product> Products { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }

}