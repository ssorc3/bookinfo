using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ratings.Services;

namespace Ratings.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService _ratings;

        public RatingsController(IRatingsService ratings)
        {
            _ratings = ratings;
        }
        
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, int>>> Get()
        {
            return Ok(await _ratings.Get());
        }
    }
}