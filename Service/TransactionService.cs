using BudgetService.Properties.Data;
using BudgetService.Services.Gateway;

namespace BudgetService.Services;

public class TransactionService
{
    private readonly IBudgetGateway _budgetGateway;
    private readonly ITransactionGateway _transactionGateway;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(IBudgetGateway budgetGateway, ITransactionGateway transactionGateway, ILogger<TransactionService> logger)
    {
        _budgetGateway = budgetGateway;
        _transactionGateway = transactionGateway;
        _logger = logger;
    }

    public async Task<bool> ProcessTransactionAsync(Transaction transaction)
    {
        var budget = await _budgetGateway.GetActiveBudgetAsync(transaction.AccountId);
        if (budget == null) return false;

        var category = transaction.TransactionType.ToLowerInvariant();

        switch (category)
        {
            case "entertainment":
                budget.EntertainmentBudget -= transaction.Amount;
                break;
            case "education":
                budget.EducationBudget -= transaction.Amount;
                break;
            case "investment":
                budget.InvestmentBudget -= transaction.Amount;
                break;
            case "dailyneeds":
                budget.DailyNeedsBudget -= transaction.Amount;
                break;
            case "housing":
                budget.HousingBudget -= transaction.Amount;
                break;
            case "utilities":
                budget.UtilitiesBudget -= transaction.Amount;
                break;
            case "transportation":
                budget.TransportationBudget -= transaction.Amount;
                break;
            case "healthcare":
                budget.HealthcareBudget -= transaction.Amount;
                break;
            case "savings":
                budget.SavingsGoal -= transaction.Amount;
                break;
            case "travel":
                budget.TravelBudget -= transaction.Amount;
                break;
            default:
                throw new ArgumentException("Invalid transaction type.");
        }

        budget.UpdatedAt = DateTime.UtcNow;

        await _budgetGateway.UpdateBudgetAsync(budget);
        await _transactionGateway.AddTransactionAsync(transaction);
        return true;
    }
}
