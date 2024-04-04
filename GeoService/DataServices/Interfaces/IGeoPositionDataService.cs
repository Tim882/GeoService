using System;
using System.Threading.Tasks;
using GeoService.Models;
using GeoService.Models.Responses;

namespace GeoService.DataServices.Interfaces
{
	public interface IGeoPositionDataService
    {
		public List<CoordinatesResponse> GetCoordinates(string country, string city, string street);
		public AdressResponse GetAdress(string lon, string lat);
	}
}

