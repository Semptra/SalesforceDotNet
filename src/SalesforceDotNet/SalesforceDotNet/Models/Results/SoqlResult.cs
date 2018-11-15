using System.Collections.Generic;
using Newtonsoft.Json;
using Semptra.SalesforceDotNet.Models.Entities;

namespace Semptra.SalesforceDotNet.Models.Results
{
    public class SoqlResult<T> where T : Entity
    {
        [JsonProperty("totalSize")]
        public int Total { get; set; }

        public bool Done { get; set; }

        public ICollection<T> Records { get; set; }
    }
}
