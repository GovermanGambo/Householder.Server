using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using Householder.Server.Settlements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Householder.Server.Host.Settlements
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettlementsController : ControllerBase
    {
        private readonly ILogger<SettlementsController> logger;
        private readonly IQueryExecutor queryProcessor;
        private readonly ICommandExecutor commandProcessor;

        public SettlementsController(IQueryExecutor queryProcessor, ICommandExecutor commandProcessor, ILogger<SettlementsController> logger)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SettlementDTO>>> GetSettlements([FromQuery] GetSettlementsQuery query)
        {
            var results = await queryProcessor.ExecuteAsync(query);

            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SettlementDTO>> GetSettlementById([FromRoute] GetSettlementByIdQuery query)
        {
            var result = await queryProcessor.ExecuteAsync(query);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateSettlementStatus([FromBodyAndRoute] UpdateSettlementStatusCommand command)
        {
            await commandProcessor.ExecuteAsync(command);
            var rowsAffected = command.RowsAffected;

            if (rowsAffected > 0)
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