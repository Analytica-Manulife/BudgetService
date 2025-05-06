public interface IBudgetGateway
{
    Task<Budget?> GetActiveBudgetAsync(Guid accountId);
    Task<Budget?> GetBudgetByIdAsync(Guid budgetId);
    Task CreateBudgetAsync(Budget budget);
    Task UpdateBudgetAsync(Budget budget);
    Task<Budget?> GetBudgetByAccountIdAsync(Guid accountId);

}