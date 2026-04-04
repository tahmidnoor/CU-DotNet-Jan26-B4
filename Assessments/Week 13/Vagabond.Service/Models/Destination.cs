namespace Vagabond.Service.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
        public DateTime LastVisited { get; set; }
    }
}