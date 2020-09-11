using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.Commands;
using Householder.Server.Models;
using Householder.Server.Queries;
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
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;

        public ResidentsController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor, ILogger<ResidentsController> logger)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Resident>>> GetResidents()
        {
            var query = new GetAllResidentsQuery();
            var results = await queryProcessor.ProcessAsync(query);

            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> AddResident(Householder.Server.Residents.AddResidentCommand command)
        {
            await commandProcessor.ProcessAsync(command);

            return Created(nameof(GetResident), new { command.Id});   
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Resident>> GetResident(int id)
        {
            var query = new GetResidentQuery(id);
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
    }
}