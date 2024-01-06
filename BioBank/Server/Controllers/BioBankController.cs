using BioBank.Shared.Database;
using BioBank.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BioBank.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioBankController : ControllerBase
    {
        private readonly IDbService _dbService;
        public BioBankController(IDbService dbService)
        {
            _dbService = dbService;
        }
        // GET: api/<BioBankController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Shared.Models.Tissue> bioBankServices = _dbService.GetBioBankCollections();
                return Ok(bioBankServices);
            }
            catch (Exception ex)
            {
                // Exception should be logged
                return BadRequest("Error loading data");
            }
        }
        // POST api/<BioBankController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Shared.Models.Tissue bioBankService)
        {
            var success =  _dbService.AddNewCollection(bioBankService);

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
