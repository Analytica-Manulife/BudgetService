using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetService.Properties.Data;

public class Account
{
    [Key]
    [Column("account_id")]
    public Guid AccountId { get; set; } = Guid.NewGuid();

    [Column("name")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Column("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [Column("balance")]
    [Required]
    public decimal Balance { get; set; } = 0.00M;
    
    public ICollection<Budget> Budgets { get; set; }

}