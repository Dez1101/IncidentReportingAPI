using IncidentReportingApi.Enums;

namespace IncidentReportingApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }

        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    }

}
