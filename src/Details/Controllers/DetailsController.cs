using System.Threading.Tasks;
using Details.Models;
using Details.Services;
using Microsoft.AspNetCore.Mvc;

namespace Details.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailsController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<BookDetails>> Get([FromServices] IDetailsService details, int id)
        {
            return await details.GetDetails(id);
        }
    }
}