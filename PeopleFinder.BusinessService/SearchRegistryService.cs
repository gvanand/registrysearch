using Microsoft.VisualBasic;
using PeopleFinder.BusinessService.Dtos;
using PeopleFinder.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleFinder.BusinessService
{
    public class SearchRegistryService : ISearchRegistryService
    {
        private PeopleFinderDbContext dbContext { get; set; }
        public SearchRegistryService(PeopleFinderDbContext dbContext)
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
            throw new NotImplementedException();
        }


        private async Task<IEnumerable<IndividualResultDto>> SearchRegistry(IndividualPayLoad payLoad)
        {
            var registry = this.dbContext.Registry.Where(f=>f.FirstName.Contains(payLoad.FirstName) || f.LastName.Contains(payLoad.LastName)).ToList();
            string misConductComment = "Certification number: {0}\r\n\r\nDrivers license number: {1}\r\n\r\nCertification status: {2}\r\n\r\nEmployer/Facility: {3}\r\n\r\nCase number: {4}";
            return registry.Select(s => new IndividualResultDto
            {
                SearchStatus = payLoad.FirstName == s.FirstName && s.LastName == payLoad.LastName ? SearchStatus.Found.ToString():SearchStatus.PotentialMatch.ToString(),
                DateOfBirth = s.DOB.ToString(),
                DeterminationComment =null,
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
