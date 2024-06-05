using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Book_Sales_Project.Controllers
{
    [Route("api/recommendation")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<ActionResult<BookRecommendation[]>> GetRecommendations(string bookName, int numRecommendations = 5)
        {
            var recommendations = await _recommendationService.GetRecommendations(bookName, numRecommendations);
            return Ok(recommendations);
        }
      
    }
}
