using CQRS.Command.Abstractions;
using CQRS.Query.Abstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Householder.Server.Users;
using Householder.Server.Authentication;
using Microsoft.AspNetCore.Authorization;

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
            try 
            {
                await commandProcessor.ExecuteAsync(command);

                return Created("yes", new { command.Id });   
            }
            catch (ValidationErrorException e)
            {
                return BadRequest(new { message = e.Message });
            }
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

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUsers([FromBody] GetAllUsersQuery query)
        {
            var results = await queryProcessor.ExecuteAsync(query);

            return Ok(results);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUserById([FromRoute] GetUserByIdQuery query)
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

        [HttpPost("validate")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult ValidateToken() 
        {
            return Ok();
        }
    }
}