using BioBank.Shared.Database;
using BioBank.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BioBank.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly IDbService _dbService;
        public SamplesController(IDbService dbService)
        {
            _dbService = dbService;
        }
        // GET api/<SamplesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult>  Get(int id)
        {
            try
            {
                List<Samples> samplesServices = _dbService.GetSamples(id);
                return Ok(samplesServices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        // POST api/<SamplesController>
        [HttpPost]
        public async Task<IActionResult>  Post([FromBody] Samples samplesService)
        {
            var success = _dbService.AddNewSample(samplesService);
            if (success)
            {
                return Ok("Data saved successfully!");
            }
            else
            {
                return BadRequest("Error saving data.");
            }
        }
    }
}
