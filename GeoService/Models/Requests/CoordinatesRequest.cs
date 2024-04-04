using System;
namespace GeoService.Models.Requests
{
	public class CoordinatesRequest: Coordinates
	{
		public CoordinatesRequest(string lat, string lon)
		{
			this.lat = lat;
			this.lon = lon;
		}
	}
}

