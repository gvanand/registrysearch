namespace RegistrySearch.BusinessService.Dtos
{
    public interface ISearchRegistryService
    {
        Task<IEnumerable<IndividualResultDto>> SearchIndividual(string CorrelationId, SearchRequestDto searchRequestDto);
        Task<BulkSearchResultDto> SearchBulk(string CorrelationId, BulkSearchRequestDto searchRequestDto);
    }
}
