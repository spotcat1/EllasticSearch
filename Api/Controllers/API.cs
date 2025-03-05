using Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/data")]
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;

        public DataController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetData([FromRoute]string id)
        {
            var result = await _dataService.GetHsCodeById(id);
            if (result == null)
            {
                return NotFound("Data not found");
            }

            return Ok(result) ;
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _dataService.SearchBySlug(query);
            return Ok(results);
        }

    }

}
