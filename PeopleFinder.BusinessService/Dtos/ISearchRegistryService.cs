using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleFinder.BusinessService.Dtos
{
    public interface ISearchRegistryService
    {
        Task<IEnumerable<IndividualResultDto>> SearchIndividual(string CorrelationId, SearchRequestDto searchRequestDto);
        Task<BulkSearchResultDto> SearchBulk(string CorrelationId, BulkSearchRequestDto searchRequestDto);
    }
}
