namespace RegistrySearch.BusinessService.Dtos
{
    public class BulkSearchRequestDto
    {
        public IndividualMetaData MetaData { get; set; }
        public BulkPayLoad[] PayLoad { get; set; }
    }

    public class BulkPayLoad : IndividualPayLoad
    {
        public string IndividualRequestId { get; set; }
    }
}
