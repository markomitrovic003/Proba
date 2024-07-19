using Microsoft.EntityFrameworkCore.Query;

namespace PrikazKorisnika.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataEnd { get; set; }
        public string Description { get; set; }
    }
}
