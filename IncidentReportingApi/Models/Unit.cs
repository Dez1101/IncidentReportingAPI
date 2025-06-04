namespace IncidentReportingApi.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Block { get; set; } = null!;
        public string Number { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();
    }

}
