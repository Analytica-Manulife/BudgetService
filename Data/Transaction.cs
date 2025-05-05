using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetService.Properties.Data
{
    [Table("transactions")]
    public class Transaction
    {
        [Key]
        [Column("transaction_id")]
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Column("transaction_type")]
        public string TransactionType { get; set; } = string.Empty;

        [Column("amount", TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column("details")]
        public string? Details { get; set; }

        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}