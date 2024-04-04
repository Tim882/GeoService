using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text.Json;
using GeoService.DataServices.Interfaces;
using GeoService.Models;
using GeoService.Models.Requests;
using GeoService.Models.Responses;

namespace GeoService.DataServices.DataServices
{
	public class GeoPositionDataService: IGeoPositionDataService
	{
        private string coordinatesURL = "https://nominatim.openstreetmap.org/search?";
		private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GeoPositionDataService> _logger;

		public GeoPositionDataService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<GeoPositionDataService> logger)
		{
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
		}

        public List<CoordinatesResponse> GetCoordinates(string country, string city, string street)
        {
            var adress = new AdressRequest(country, city, street);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");

            string uri = $"{coordinatesURL}country={country}&city={city}&street={street}&format={adress.format}&limit={adress.limit}";

            _logger.Log(LogLevel.Information, $"Request: {uri}");

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = client.SendAsync(request).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            var result = JsonSerializer.Deserialize<List<CoordinatesResponse>>(responseBody);
            response.EnsureSuccessStatusCode();

            return result;
        }

        public AdressResponse GetAdress(string lon, string lat)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://suggestions.dadata.ru/suggestions/api/4_1/rs/geolocate/address");
            request.Headers.Add("Accept", "application/json");
            var token = _configuration.GetValue(typeof(string), "Bearer");
            request.Headers.Add("Authorization", $"Token {token}");
            var content = new StringContent("{ \"lat\": 55.878, \"lon\": 37.653 }", null, "application/json");
            request.Content = content;
            _logger.Log(LogLevel.Information, $"Request: {request}");
            var response = client.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            var list = JsonSerializer.Deserialize<AdressResponse>(result);

            if (list.suggestions == null) throw new Exception("Не удалось получить список адресов.");

            return list;//list.suggestions.Select(s => s.value).ToList();
        }
    }
}

