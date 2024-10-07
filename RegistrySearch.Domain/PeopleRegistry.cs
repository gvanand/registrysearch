using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrySearch.Domain
{
    [Table("Registry")]
    public class PeopleRegistry
    {
        [Key]
        public int Id   { get; set; }
        public string? CertificationNo { get; set; }
        public string? DriversLicenseNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string? CertificateStatus { get; set; }
        public DateTime? FinalDeterminationDate { get; set; }
        public string ConductDescription1 { get; set; }
        public string ConductDescription2 { get; set; }
        public string ConductDescription3 { get; set; }
        public string ConductDescription4 { get; set; }
        public string EmployerFacility { get; set; }
        public string CaseNo { get; set; }

    }

    public class PeopleRegistryMapping : IEntityTypeConfiguration<PeopleRegistry>
    {
        public void Configure(EntityTypeBuilder<PeopleRegistry> builder)
        {
           builder.HasKey(x => x.Id);
        }
    }
}
