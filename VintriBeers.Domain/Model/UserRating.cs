
namespace VintriBeers.Domain.Model
{
    public class UserRating
    {
        public int BeerId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
