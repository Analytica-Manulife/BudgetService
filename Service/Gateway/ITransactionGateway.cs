using BudgetService.Properties.Data;

namespace BudgetService.Services.Gateway;

public interface ITransactionGateway
{
    Task AddTransactionAsync(Transaction transaction);
}