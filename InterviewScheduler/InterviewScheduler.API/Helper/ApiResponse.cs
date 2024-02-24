using Newtonsoft.Json;

namespace InterviewScheduler.API.Helper
{
    public class ApiResponse
    {
        public Location? location { get; set; }
        public Current? current { get; set; }
        public Forecast? forecast { get; set; }
    }

    public class Forecast
    {
        [JsonProperty]
        public IList<Forecastday>? forecastday { get; private set; }
    }

    public class Forecastday
    {
        public DateTime date { get; set; }
        public Day? day { get; set; }
    }

    public class Day
    {
        [JsonProperty("maxtemp_c")]
        public float Maxtempc { get; set; }
        [JsonProperty("maxtemp_f")]
        public float MaxtempF { get; set; }
        [JsonProperty("mintemp_c")]
        public float MintempC { get; set; }
        [JsonProperty("mintemp_f")]
        public float MintempF { get; set; }
        [JsonProperty("avgtemp_c")]
        public float AvgtempC { get; set; }
        [JsonProperty("avgtemp_f")]
        public float AvgtempF { get; set; }
        [JsonProperty("avghumidity")]
        public float AvgHumidity { get; set; }
        [JsonProperty("Daily_will_it_rain")]
        public bool DailyWillItRain { get; set; }
        [JsonProperty("daily_will_it_snow")]
        public bool DailyWillItSnow { get; set; }
    }

    public class Current
    {
        [JsonProperty("temp_c")]
        public float TempC { get; set; }
        [JsonProperty("temp_f")]
        public float TempF { get; set; }
        [JsonProperty("wind_mph")]
        public float WindMph { get; set; }
        [JsonProperty("wind_kph")]
        public float WindKph { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }

    }
    public class Location
    {
        public string Name { get; set; } = "";
        public string Region { get; set; } = "";
        public string country { get; set; } = "";
    }
}
