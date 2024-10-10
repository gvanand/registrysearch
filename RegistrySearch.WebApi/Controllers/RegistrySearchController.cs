

using Microsoft.AspNetCore.Mvc;
using RegistrySearch.BusinessService;
using RegistrySearch.BusinessService.Dtos;

namespace RegistrySearch.WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegistryController : Controller
    {
        private readonly ISearchRegistryService service;
        public RegistryController(ISearchRegistryService service)
        {
            this.service = service;
        }
        [HttpGet("person/individual")]
        public async Task<IEnumerable<IndividualResultDto>> getRegistry(string correlationId)
        {
            return new List<IndividualResultDto>();
        }
        [HttpPost("person/individual")]
        public async Task<IEnumerable<IndividualResultDto>> SearchRegistry([FromQuery] string correlationId, SearchRequestDto searchRequest)
        {
            var result = await this.service.SearchIndividual(correlationId, searchRequest);
            return result;
        }
        [HttpGet("person/bulk")]
        public async Task<BulkSearchResultDto> GetBulkRegistry(string correlationId)
        {
            var result = await this.service.GetSearchBulk(correlationId);
            return result;
        }

        [HttpPost("person/bulk")]
        public async Task BulkSearchRegistry([FromQuery] string correlationId, BulkSearchRequestDto bulkSearchRequest)
        {
            var requiestIds = bulkSearchRequest.PayLoad.Select(s => s.IndividualRequestId).ToArray();
            if (requiestIds.Count() != requiestIds.Distinct().Count())
            {
                throw new ArgumentException("Individual Request Id has duplicate values");

            }
            var result =  this.service.SearchBulk(correlationId, bulkSearchRequest);
        }

    }
}
