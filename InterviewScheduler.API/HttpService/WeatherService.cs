using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;

namespace InterviewScheduler.API.HttpService
{
    public interface IWeatherService
    {
        Task<string> Get(string city);
    }

    public class WeatherService : IWeatherService
    {
        private HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Get(string city)
        {
            var apiKey = "8905343336314176a33204337242302";
            string apiurl = $"?q={city}&key={apiKey}";
            var response = await _httpClient.GetAsync(apiurl);
            return await response.Content.ReadAsStringAsync();
            throw new NotImplementedException();
        }
    }
}
