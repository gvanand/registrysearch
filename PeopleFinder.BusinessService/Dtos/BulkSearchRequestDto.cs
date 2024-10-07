using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PeopleFinder.BusinessService.Dtos
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
