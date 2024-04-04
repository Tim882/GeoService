using System;
namespace GeoService.Models.Responses
{
	public class AdressResponse
	{
        public class Suggestion { public string value { get; set; } }

        public List<Suggestion> suggestions { get; set; }
    }
}

