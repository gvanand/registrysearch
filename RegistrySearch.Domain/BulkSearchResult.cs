using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrySearch.Domain
{
    [Table("BulkSearchResult")]
    public class BulkSearchResult
    {
        [Key]
        public string CorrelationId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string RequestedByUser { get; set; }
        public string RequestedByAgency { get; set; }
        public string Status { get; set; }
        public int NoOfRequest { get; set; }
        public int NoOfRequestCompleted { get; set; }
    }

    public class BulkSearchResultMapping : IEntityTypeConfiguration<BulkSearchResult>
    {
        public void Configure(EntityTypeBuilder<BulkSearchResult> builder)
        {
           builder.HasKey(x => x.CorrelationId);
        }
    }
}
