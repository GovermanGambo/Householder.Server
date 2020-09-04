using System.Collections.Generic;
using System.Threading.Tasks;
using Householder.Server.Models;
using Householder.Server.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Householder.Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentController : ControllerBase
    {
        private readonly ILogger<ResidentController> logger;
        private readonly IQueryProcessor queryProcessor;

        public ResidentController(IQueryProcessor queryProcessor, ILogger<ResidentController> logger)
        {
            this.queryProcessor = queryProcessor;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Resident>> GetResidents()
        {
            var query = new GetAllResidentsQuery();
            var results = await queryProcessor.ProcessAsync(query);

            return results;
        }
    }
}