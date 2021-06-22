using System.ComponentModel.DataAnnotations;

namespace ValVenalEstimatorApi.Models
{
    public class Place
    {
        [Key]
        public long Id { get; set; }
        public string Prefecture { get; set; }
        public string District { get; set; }
        public double PricePerMeterSquare { get; set; }
    }
}