using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using Householder.Server.Models;
using Householder.Server.Expenses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Householder.Server.Host.Expenses
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> logger;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public ExpensesController(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor, ILogger<ExpensesController> logger)
        {
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses([FromQuery] GetExpensesQuery query)
        {
            var results = await queryExecutor.ExecuteAsync(query);

            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Expense>> GetExpense([FromRoute] GetExpenseQuery query)
        {
            var result = await queryExecutor.ExecuteAsync(query);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<long>> AddExpense([FromBody] AddExpenseCommand command)
        {
            await commandExecutor.ExecuteAsync(command);

            return Created(nameof(GetExpense), new { command.Id });  
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> PatchExpense([FromBodyAndRoute] UpdateExpenseCommand command)
        {
            await commandExecutor.ExecuteAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteExpense([FromRoute] DeleteExpenseCommand command)
        {
            await commandExecutor.ExecuteAsync(command);

            if (command.RowsAffected == 1)
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