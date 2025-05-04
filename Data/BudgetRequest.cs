namespace BudgetService.Data;

public class BudgetRequest
{
    public decimal Income { get; set; }
    public decimal EMI { get; set; }
    public decimal MedicalExpense { get; set; }
    public decimal EducationExpense { get; set; }
}