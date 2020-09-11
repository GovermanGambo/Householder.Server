using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.Commands;
using Householder.Server.Models;
using Householder.Server.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Householder.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> logger;
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;

        public ExpensesController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, ILogger<ExpensesController> logger)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAllExpenses(int? limit, int? status, int? resident)
        {
            var query = new GetAllExpensesQuery(limit, status, resident);

            var results = await queryProcessor.ProcessAsync(query);

            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var query = new GetExpenseByIdQuery(id);

            var result = await queryProcessor.ProcessAsync(query);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> AddExpense(Expense expense)
        {
            var command = new AddExpenseCommand(expense);

            var resultId = await commandProcessor.ProcessAsync(command);

            if (resultId == -1)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetExpense), new { id = resultId }, expense);
        }

        [HttpPut("{expenseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateExpense(int expenseId, Expense expense)
        {
            var command = new UpdateExpenseDetailsCommand(expenseId, expense);
            var resultId = await commandProcessor.ProcessAsync(command);

            if (resultId)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        } 
    }
}