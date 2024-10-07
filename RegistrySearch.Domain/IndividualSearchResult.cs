using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrySearch.Domain
{
    [Table("IndividualSearchResult")]
    public class IndividualSearchResult
    {
        [Key]
        public Guid RequestedId { get; set; }
        public string CorrelationId { get; set; }
        public string IndividualRequest { get; set; }
        public string IndividualResponse { get; set; }
        public string IndividualStatus { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class IndividualSearchResultMapping : IEntityTypeConfiguration<IndividualSearchResult>
    {
        public void Configure(EntityTypeBuilder<IndividualSearchResult> builder)
        {
           builder.HasKey(x => x.CorrelationId);
        }
    }
}
