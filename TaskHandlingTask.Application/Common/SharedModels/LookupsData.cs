using TicketsHandling.Application.Features.LookupData.Query;

namespace TicketsHandling.Application.Common.SharedModels
{
    public class LookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GovernorateId { get; set; } // Nullable to indicate it is optional
        public int? CityId { get; set; } // Nullable to indicate it is optional
    }
    public class LookupsData
    {
        public List<LookupItem> Governorates { get; set; }
        public List<LookupItem> Cities { get; set; }
        public List<LookupItem> Districts { get; set; }
    }

}
