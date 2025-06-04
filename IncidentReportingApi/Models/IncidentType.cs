namespace IncidentReportingApi.Models
{
    public class IncidentType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // e.g., "Noise", "Safety", "Damage"

        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }

}
