using BudgetService.Properties.Data;

public interface IAccountGateway
{
    Task<Account?> GetAccountByIdAsync(Guid accountId);
    Task<bool> AccountExistsAsync(Guid accountId);
}