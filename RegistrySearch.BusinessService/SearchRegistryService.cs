using RegistrySearch.BusinessService.Dtos;
using RegistrySearch.DataAccess;
using RegistrySearch.Domain;

namespace RegistrySearch.BusinessService
{
    public class SearchRegistryService : ISearchRegistryService
    {
        private RegistryDbContext dbContext { get; set; }
        public SearchRegistryService(RegistryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<IndividualResultDto>> SearchIndividual(string CorrelationId, SearchRequestDto searchRequestDto)
        {
            var registry = await SearchRegistry(searchRequestDto.PayLoad);

            return registry;
        }


        public async Task<BulkSearchResultDto> SearchBulk(string CorrelationId, BulkSearchRequestDto searchRequestDto)
        {
            var bulkSearch = this.AddBulkSearchResult(CorrelationId, searchRequestDto);
            BulkSearchResultDto bulkSearchResultDto = new BulkSearchResultDto();
            bulkSearchResultDto.Results = new Dictionary<string, List<IndividualResultDto>>();
            foreach (var reqDto in searchRequestDto.PayLoad)
            {
                var registry = await SearchRegistry(reqDto);
                bulkSearch.NoOfRequestCompleted += 1;
                this.dbContext.Update<BulkSearchResult>(bulkSearch);
                if(bulkSearch.NoOfRequestCompleted == bulkSearch.NoOfRequest)
                {
                    bulkSearch.CompletedAt = DateTime.UtcNow;
                    bulkSearch.Status = "Completed";
                }
                this.dbContext.SaveChanges();
                bulkSearchResultDto.RecordsProccessed = registry.Count();
                bulkSearchResultDto.Results.Add(reqDto.IndividualRequestId, registry.ToList());
            }
            return bulkSearchResultDto;
        }
        private BulkSearchResult AddBulkSearchResult(string CorrelationId, BulkSearchRequestDto searchRequestDto)
        {
            BulkSearchResult bulkSearchResult = new BulkSearchResult
            {
                CorrelationId = CorrelationId,
                NoOfRequest = searchRequestDto.PayLoad.Count(),
                NoOfRequestCompleted =0,
                SubmittedAt = DateTime.UtcNow,
                RequestedByAgency = searchRequestDto.MetaData.Agency,
                RequestedByUser = searchRequestDto.MetaData.UserId,
                 Status= "InProgress"
            };
            try
            {
                this.dbContext.Add(bulkSearchResult);
                this.dbContext.SaveChanges();
                return bulkSearchResult;
            }
            catch (Exception ex) {

                return null;
            }
        }

        private async Task<IEnumerable<IndividualResultDto>> SearchRegistry(IndividualPayLoad payLoad)
        {
            var registry = this.dbContext.Registry.Where(f => f.FirstName.Contains(payLoad.FirstName) || f.LastName.Contains(payLoad.LastName)).ToList();
            string misConductComment = "Certification number: {0}\r\n\r\nDrivers license number: {1}\r\n\r\nCertification status: {2}\r\n\r\nEmployer/Facility: {3}\r\n\r\nCase number: {4}";
            return registry.Select(s => new IndividualResultDto
            {
                SearchStatus = payLoad.FirstName == s.FirstName && s.LastName == payLoad.LastName ? SearchStatus.Found.ToString() : SearchStatus.PotentialMatch.ToString(),
                DateOfBirth = s.DOB.ToString(),
                DeterminationComment = null,
                Name = new PeopleName { First = s.FirstName, Last = s.LastName },
                Id = new List<IdSSN> { new IdSSN { IdNumber = s.DriversLicenseNo, IdType = "DriversLicenseNumber" } },
                MisConducts = new List<MisConduct> {
                    new MisConduct { Description = s.ConductDescription1 , FinalDeterminationDate=s.FinalDeterminationDate.ToString(),
                                    Comments  = string.Format(misConductComment,s.CertificationNo,s.DriversLicenseNo,s.CertificateStatus,s.EmployerFacility,s.CaseNo)  } ,
                    new MisConduct { Description = s.ConductDescription2 , FinalDeterminationDate=s.FinalDeterminationDate.ToString(),
                                    Comments  = string.Format(misConductComment,s.CertificationNo,s.DriversLicenseNo,s.CertificateStatus,s.EmployerFacility,s.CaseNo)  },
                    new MisConduct { Description = s.ConductDescription3 , FinalDeterminationDate=s.FinalDeterminationDate.ToString(),
                                    Comments  = string.Format(misConductComment,s.CertificationNo,s.DriversLicenseNo,s.CertificateStatus,s.EmployerFacility,s.CaseNo)  },
                    new MisConduct { Description = s.ConductDescription4 , FinalDeterminationDate=s.FinalDeterminationDate.ToString(),
                                     Comments  = string.Format(misConductComment,s.CertificationNo,s.DriversLicenseNo,s.CertificateStatus,s.EmployerFacility,s.CaseNo)  }

                }

            }).ToList();
        }
        /*
commenr - certno , dlno , certstats, employer fac, case no
*/
    }
}
