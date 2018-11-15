using System;
using Newtonsoft.Json;

namespace Semptra.SalesforceDotNet.Models.Entities
{
    public class Case : Entity
    {
        [JsonProperty("IsDeleted")]
        public bool? IsDeleted { get; set; }

        [JsonProperty("CaseNumber")]
        public string CaseNumber { get; set; }

        [JsonProperty("AccountId")]
        public string AccountId { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Priority")]
        public string Priority { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("IsClosed")]
        public bool? IsClosed { get; set; }

        [JsonProperty("IsEscalated")]
        public bool? IsEscalated { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("CreatedById")]
        public string CreatedById { get; set; }

        [JsonProperty("Sub_Type_level_1__c")]
		public string SubTypeLevel1 { get; set; }

        [JsonProperty("Sub_Type_level_2__c")]
        public string SubTypeLevel2 { get; set; }

        [JsonProperty("Change_Needed__c")]
        public string ChangeNeeded { get; set; }

        [JsonProperty("Specific_Reason_for_Change__c")]
        public string SpecificReasonForChange { get; set; }

        [JsonProperty("DM_LS__c")]
        public string DmLs { get; set; }

        [JsonProperty("Study__c")]
        public string Study { get; set; }

        [JsonProperty("Site_Number__c")]
        public string Site { get; set; }

        [JsonProperty("SVID__c")]
        public string SVID { get; set; }
    }
}
