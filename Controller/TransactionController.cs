using BudgetService.Properties.Data;
using BudgetService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService;

[ApiController]
[Route("transection/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(TransactionService transactionService, ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
    {
        try
        {
            var success = await _transactionService.ProcessTransactionAsync(transaction);
            if (!success) return NotFound(new { message = "No active budget found for this account." });

            return Ok(new { message = "Transaction processed and budget updated." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing transaction.");
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetTransactionsByAccountId(Guid accountId)
    {
        try
        {
            var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
            if (transactions == null || !transactions.Any())
            {
                return NotFound(new { message = "No transactions found for this account." });
            }

            return Ok(transactions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving transactions.");
            return StatusCode(500, new { message = ex.Message });
        }
    }

}
