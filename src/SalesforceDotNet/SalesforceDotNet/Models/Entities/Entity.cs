using Newtonsoft.Json;

namespace Semptra.SalesforceDotNet.Models.Entities
{
    public abstract class Entity
    {
        public EntityAttributes Attributes { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}
