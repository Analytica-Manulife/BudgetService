using System;
using System.Threading.Tasks;
using BudgetService.Properties.Data;
using Microsoft.Extensions.Logging;

namespace BudgetService.Services
{
    public class BudgetService
    {
        private readonly IBudgetGateway _budgetGateway;
        private readonly IAccountGateway _accountGateway;
        private readonly ILogger<BudgetService> _logger;

        public BudgetService(IBudgetGateway budgetGateway, IAccountGateway accountGateway, ILogger<BudgetService> logger)
        {
            _budgetGateway = budgetGateway;
            _accountGateway = accountGateway;
            _logger = logger;
        }

        public async Task<(bool IsRequired, Budget Budget)> CheckIfBudgetRequiredAsync(Guid accountId)
        {
            var budget = await _budgetGateway.GetBudgetByIdAsync(accountId);

            if (budget == null || budget.PeriodEnd < DateTime.UtcNow.Date)
                return (true, null);

            return (false, budget);
        }

       public Budget GenerateBudget(Guid accountId, decimal income, decimal emi, decimal education, decimal medical)
{
    var available = income - (emi + education + medical);
    if (available < 0) available = 0;

    return new Budget
    {
        BudgetId = Guid.NewGuid(),
        AccountId = accountId,
        PeriodStart = DateTime.UtcNow.Date,
        PeriodEnd = DateTime.UtcNow.Date.AddMonths(1).AddDays(-1), // example: full month
        CreatedAt = DateTime.UtcNow,

        EntertainmentBudget = available * 0.1m,
        EducationBudget = education,
        InvestmentBudget = available * 0.1m,
        DailyNeedsBudget = available * 0.3m,
        HousingBudget = available * 0.15m,
        UtilitiesBudget = available * 0.05m,
        TransportationBudget = available * 0.1m,
        HealthcareBudget = medical,
        SavingsGoal = available * 0.1m,
        TravelBudget = available * 0.1m
    };
}


public async Task CreateOrUpdateBudgetAsync(Budget budgetInput)
{
    var now = DateTime.UtcNow.Date;
    var endOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

    var existingBudget = await _budgetGateway.GetActiveBudgetAsync(budgetInput.AccountId);

    if (existingBudget != null)
    {
        // Update existing budget
        existingBudget.EntertainmentBudget = budgetInput.EntertainmentBudget;
        existingBudget.EducationBudget = budgetInput.EducationBudget;
        existingBudget.InvestmentBudget = budgetInput.InvestmentBudget;
        existingBudget.DailyNeedsBudget = budgetInput.DailyNeedsBudget;
        existingBudget.HousingBudget = budgetInput.HousingBudget;
        existingBudget.UtilitiesBudget = budgetInput.UtilitiesBudget;
        existingBudget.TransportationBudget = budgetInput.TransportationBudget;
        existingBudget.HealthcareBudget = budgetInput.HealthcareBudget;
        existingBudget.SavingsGoal = budgetInput.SavingsGoal;
        existingBudget.TravelBudget = budgetInput.TravelBudget;

        existingBudget.UpdatedAt = DateTime.UtcNow;

        await _budgetGateway.UpdateBudgetAsync(existingBudget);
    }
    else
    {
        // Create new budget
        budgetInput.BudgetId = Guid.NewGuid();
        budgetInput.PeriodStart = now;
        budgetInput.PeriodEnd = endOfMonth;
        budgetInput.CreatedAt = DateTime.UtcNow;
        budgetInput.UpdatedAt = null;

        await _budgetGateway.CreateBudgetAsync(budgetInput);
    }
}

public async Task<Budget?> GetBudgetByAccountIdAsync(Guid accountId)
{
    return await _budgetGateway.GetBudgetByAccountIdAsync(accountId);
}


    }
}
