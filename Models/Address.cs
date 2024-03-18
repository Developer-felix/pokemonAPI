using System.ComponentModel.DataAnnotations.Schema;

namespace PocumanAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Location { get; set; } 
        public string Latitude { get; set; }
        public string Longitude { get; set; } 
        public string CountryId {  get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } 

    }
}
