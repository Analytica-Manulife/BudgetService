using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetService.Properties.Data;

[Table("budgets")]
public class Budget
{
    [Key]
    [Column("budget_id")]
    public Guid BudgetId { get; set; }

    [ForeignKey(nameof(Account))]
    [Column("account_id")]
    public Guid AccountId { get; set; }

    [Column("period_start", TypeName = "date")]
    public DateTime PeriodStart { get; set; }

    [Column("period_end", TypeName = "date")]
    public DateTime PeriodEnd { get; set; }

    [Column("entertainment_budget", TypeName = "decimal(18,2)")]
    public decimal EntertainmentBudget { get; set; } = 0.00M;

    [Column("education_budget", TypeName = "decimal(18,2)")]
    public decimal EducationBudget { get; set; } = 0.00M;

    [Column("investment_budget", TypeName = "decimal(18,2)")]
    public decimal InvestmentBudget { get; set; } = 0.00M;

    [Column("daily_needs_budget", TypeName = "decimal(18,2)")]
    public decimal DailyNeedsBudget { get; set; } = 0.00M;

    [Column("housing_budget", TypeName = "decimal(18,2)")]
    public decimal HousingBudget { get; set; } = 0.00M;

    [Column("utilities_budget", TypeName = "decimal(18,2)")]
    public decimal UtilitiesBudget { get; set; } = 0.00M;

    [Column("transportation_budget", TypeName = "decimal(18,2)")]
    public decimal TransportationBudget { get; set; } = 0.00M;

    [Column("healthcare_budget", TypeName = "decimal(18,2)")]
    public decimal HealthcareBudget { get; set; } = 0.00M;

    [Column("savings_goal", TypeName = "decimal(18,2)")]
    public decimal SavingsGoal { get; set; } = 0.00M;

    [Column("travel_budget", TypeName = "decimal(18,2)")]
    public decimal TravelBudget { get; set; } = 0.00M;

    [Column("created_at", TypeName = "datetime2")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at", TypeName = "datetime2")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AccountId")]
    [NotMapped]
    public Account Account { get; set; }
}
