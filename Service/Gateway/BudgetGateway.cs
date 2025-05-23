using BudgetService.Data;
using BudgetService.Properties.Data;
using Microsoft.EntityFrameworkCore;


    public class BudgetGateway : IBudgetGateway
    {
        private readonly AppDbContext _context;

        public BudgetGateway(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Budget?> GetActiveBudgetAsync(Guid accountId)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Budgets
                .Where(b => b.AccountId == accountId && b.PeriodStart <= today && b.PeriodEnd >= today)
                .FirstOrDefaultAsync();
        }

        public async Task<Budget?> GetBudgetByIdAsync(Guid budgetId)
        {
            return await _context.Budgets.FindAsync(budgetId);
        }

        public async Task CreateBudgetAsync(Budget budget)
        {   
            var account = await _context.Accounts.FindAsync(budget.AccountId);
            budget.Account = account;
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
        }

        
        public async Task UpdateBudgetAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Budget?> GetBudgetByAccountIdAsync(Guid accountId)
        {
            return await _context.Budgets
                .Where(b => b.AccountId == accountId)
                .OrderByDescending(b => b.CreatedAt)
                .FirstOrDefaultAsync();
        }

    }
