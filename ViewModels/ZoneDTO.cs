using ValVenalEstimatorApi.Enums;

namespace ValVenalEstimatorApi.ViewModels
{
    public class ZoneDTO
    {
        public string Name { get; set; }
        public int ZoneNum { get; set; }
        public string Code { get; set; }
        public ZoneType  Type { get; set; }
        public double PricePerMeterSquare { get; set; }
        public long PrefectureId { get; set; }
   
    }
}