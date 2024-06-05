using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using System.Net.Http.Formatting;


namespace BusinessLayer.Concrete
{
    public class RecommendationManager : IRecommendationService
    {
        private readonly HttpClient _client;

        public RecommendationManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<BookRecommendation[]> GetRecommendations(string bookName, int numRecommendations = 5)
        {

			var response = await _client.GetAsync($"http://localhost:5000/api/recommendations?book_name={bookName}&num_recommendations={numRecommendations}");
			response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var recommendations = JsonConvert.DeserializeObject<BookRecommendation[]>(content);
            return recommendations;
        }

    }
}
