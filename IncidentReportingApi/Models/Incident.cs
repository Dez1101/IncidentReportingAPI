using IncidentReportingApi.Enums;

namespace IncidentReportingApi.Models
{
    public class Incident
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int IncidentTypeId { get; set; }
        public IncidentType IncidentType { get; set; } = null!;

        public string Description { get; set; } = null!;
        public IncidentStatus Status { get; set; } = IncidentStatus.Pending;
        public IncidentPriority Priority { get; set; } = IncidentPriority.Low;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateResolved { get; set; }

        public ICollection<IncidentMedia> Media { get; set; } = new List<IncidentMedia>();
    }
}
