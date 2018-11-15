using System.Collections.Generic;
using Newtonsoft.Json;

namespace Semptra.SalesforceDotNet.Models.Results
{
    public class UpdateResult
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Success")]
        public bool? Success { get; set; }

        [JsonProperty("Errors")]
        public ICollection<object> Errors { get; set; }
    }
}
