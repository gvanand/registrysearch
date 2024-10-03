using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleFinder.BusinessService.Dtos
{
    public class SearchRequestDto
    {
        public IndividualMetaData MetaData { get; set; }
        public IndividualPayLoad PayLoad { get; set; }

    }
    public class IndividualMetaData
    {
        public string Agency { get; set; }
        public string UserId { get; set; }
    }

    public class IndividualPayLoad
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string SSN { get; set; }

    }
}
