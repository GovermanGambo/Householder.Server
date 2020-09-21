using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.Residents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Householder.Server.Host.Residents
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentsController : ControllerBase
    {
        private readonly ILogger<ResidentsController> logger;
        private readonly IQueryExecutor queryProcessor;
        private readonly ICommandExecutor commandProcessor;

        public ResidentsController(IQueryExecutor queryProcessor, ICommandExecutor commandProcessor, ILogger<ResidentsController> logger)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResidentDTO>>> GetResidents([FromQuery] GetResidentsQuery query)
        {
            var results = await queryProcessor.ExecuteAsync(query);

            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<object>> AddResident([FromBody] AddResidentCommand command)
        {
            await commandProcessor.ExecuteAsync(command);

            return Created(nameof(GetResident), new { command.Id });   
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResidentDTO>> GetResident([FromRoute] GetResidentByIdQuery query)
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
    }
}