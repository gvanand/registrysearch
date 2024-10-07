namespace RegistrySearch.BusinessService.Dtos
{
    public class BulkSearchResultDto
    {
        public string Status { get; set; }
        public int RecordsProccessed { get; set; }
        public Dictionary<string, List<IndividualResultDto>> Results { get; set; }
    }
}
