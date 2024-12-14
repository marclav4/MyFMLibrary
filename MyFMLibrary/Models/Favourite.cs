namespace MyFMLibrary.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Guid StationUuid { get; set; }
    }
}
