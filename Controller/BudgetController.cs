namespace BudgetService;

using System;
using System.Threading.Tasks;
using BudgetService.Data;
using BudgetService.Services;
using BudgetService.Properties.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("budget/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetService _budgetService;
        private readonly ILogger<BudgetController> _logger;

        public BudgetController(BudgetService budgetService, ILogger<BudgetController> logger)
        {
            _budgetService = budgetService;
            _logger = logger;
        }

        [HttpGet("{accountId}/require-budget")]
        public async Task<IActionResult> CheckBudgetRequired(Guid accountId)
        {
            var (isRequired, currentBudget) = await _budgetService.CheckIfBudgetRequiredAsync(accountId);
            return Ok(new { isRequired, currentBudget });
        }

        [HttpPost("{accountId}/generate")]
        public IActionResult GenerateBudget(Guid accountId, [FromBody] BudgetRequest request)
        {
            var budget = _budgetService.GenerateBudget(accountId,request.Income, request.EMI, request.EducationExpense, request.MedicalExpense);
            return Ok(budget);
        }

        [HttpPost("{accountId}/create")]
        public async Task<IActionResult> CreateBudget(Guid accountId, [FromBody] Budget budget)
        {
            await _budgetService.CreateOrUpdateBudgetAsync(budget);
            return Ok(new { message = "Budget created or updated successfully." });
        }
    }

