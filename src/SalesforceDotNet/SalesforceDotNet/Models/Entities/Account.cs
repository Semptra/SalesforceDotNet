using Newtonsoft.Json;

namespace Semptra.SalesforceDotNet.Models.Entities
{
    public class Account : Entity
    {
        [JsonProperty("IsDeleted")]
        public bool? IsDeleted { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Sponsor_Abbr__c")]
        public string SponsorAbbreviation { get; set; }
    }
}
