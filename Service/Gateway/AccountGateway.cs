using BudgetService.Data;
using BudgetService.Properties.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Services.Gateway;

public class AccountGateway : IAccountGateway
{
    private readonly AppDbContext _context;

    public AccountGateway(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetAccountByIdAsync(Guid accountId)
    {
        return await _context.Accounts
            .Include(a => a.Budgets)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);
    }

    public async Task<bool> AccountExistsAsync(Guid accountId)
    {
        return await _context.Accounts.AnyAsync(a => a.AccountId == accountId);
    }
}