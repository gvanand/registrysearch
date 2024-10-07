namespace RegistrySearch.BusinessService.Dtos
{
    public class IndividualResultDto
    {
        public string SearchStatus { get; set; }
        public PeopleName Name { get; set; }
        public string DateOfBirth { get; set; }
        public List<IdSSN> Id { get; set; }
        public List<MisConduct> MisConducts { get; set; }
        public string DeterminationComment { get; set; }
    }

    public class MisConduct
    {
        public string Description { get; set; }
        public string ReportableConductDate { get; set; }
        public string FinalDeterminationDate { get; set; }
        public string Comments { get; set; }

    }
    public class PeopleName
    {
        public string First { get; set; }
        public string Last { get; set; }
    }
    public class IdSSN
    {
        public string IdNumber { get; set; }
        public string IdType { get; set; }
    }
    public enum SearchStatus
    {
        Found = 1,
        Pending = 2,
        Error = 3,
        PotentialMatch = 4
    }
}