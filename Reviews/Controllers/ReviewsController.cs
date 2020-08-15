using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reviews.Models;
using Reviews.Services;

namespace Reviews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviews;

        public ReviewsController(IReviewsService reviews)
        {
            _reviews = reviews;
        }
        
        [HttpGet]
        [Route("{productId:int}")]
        public async Task<ActionResult<IEnumerable<Review>>> Get(int productId)
        {
            var reviews = await _reviews.GetReviews(productId);
            return reviews.Any() 
                ? Ok(await _reviews.GetReviews(productId))
                : (ActionResult<IEnumerable<Review>>) NoContent();
        }
    }
}