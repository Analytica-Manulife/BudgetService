using BudgetService.Data;
using BudgetService.Properties.Data;

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
}
