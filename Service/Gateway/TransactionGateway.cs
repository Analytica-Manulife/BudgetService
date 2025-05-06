using BudgetService.Data;
using BudgetService.Properties.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Services.Gateway;

public class TransactionGateway : ITransactionGateway
{
    private readonly AppDbContext _context;
    public TransactionGateway(AppDbContext context) => _context = context;

    public async Task AddTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId)
    {
        var param = new SqlParameter("@account_id", accountId);

        return await _context.Transactions
            .FromSqlRaw("EXEC GetTransactionsByAccountID @account_id", param)
            .ToListAsync();
    }
}
