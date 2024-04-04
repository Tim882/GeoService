using System;
namespace GeoService.Models.Requests
{
	public class AdressRequest : Adress
    {
        public AdressRequest(string country, string city, string street) : base(country, city, street)
        {
        }

        public string format { get; set; } = "json";
        public int limit { get; set; } = 10;
	}
}

