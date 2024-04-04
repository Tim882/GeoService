using System;
namespace GeoService.Models
{
	public class Adress
	{
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }

		public Adress(string country, string city, string street)
		{
			Country = country;
			City = city;
			Street = street;
		}
	}
}

