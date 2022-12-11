
using System.Collections.Generic;

namespace VintriBeers.Domain.Model
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<UserRating> UserRatings { get; set; }
    }
}
