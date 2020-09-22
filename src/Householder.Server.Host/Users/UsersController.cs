using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using System.Threading.Tasks;
using Householder.Server.Residents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Householder.Server.Users;
using Householder.Server.Authentication;

namespace Householder.Server.Host.Residents
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IQueryExecutor queryProcessor;
        private readonly ICommandExecutor commandProcessor;

        public UsersController(IQueryExecutor queryProcessor, ICommandExecutor commandProcessor, ILogger<UsersController> logger)
        {
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<object>> RegisterUser([FromBody] RegisterUserCommand command)
        {
            await commandProcessor.ExecuteAsync(command);

            return Created("yes", new { command.Id });   
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            try
            {
                await commandProcessor.ExecuteAsync(command);
                return Ok(new { token = command.Token });
            }
            catch (AuthenticationFailedException e)
            {
                return Unauthorized(new { message = e.Message });
            }
            


        }
    }
}