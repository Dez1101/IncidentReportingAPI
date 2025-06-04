namespace IncidentReportingApi.Models
{
    public class IncidentMedia
    {
        public int Id { get; set; }

        public int IncidentId { get; set; }
        public Incident Incident { get; set; } = null!;

        public string FilePath { get; set; } = null!;
        public string FileType { get; set; } = null!; // "image" or "video"
    }

}
