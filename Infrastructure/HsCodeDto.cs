namespace Infrastructure
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class HsCodeDto
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;

        public string ReferenceId { get; set; } = string.Empty;

        public int SeasonId { get; set; }
        public int CategoryId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public int ImportDuty { get; set; }
        public int CustomDuty { get; set; }
        public string CommercialBenefit { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;
        public string KeyWords { get; set; } = string.Empty;

        public string PreferentialCountryCode { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public string ImportantInfoTags { get; set; } = string.Empty;
        public int ProductCategory { get; set; }
        public int CurrencyDedicationStatus { get; set; }
        public int ImportStatus { get; set; }

        public string InfoDescription { get; set; } = string.Empty;
        public string AssertDescription { get; set; } = string.Empty;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }

}
