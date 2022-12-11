using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VintriBeers.Domain.Model;

namespace VintriBeers.Domain.Data
{
    public interface IUserRatingsContext
    {
        List<UserRating> UserRatings { get; set; }

        void AddUserRating(UserRating userRating);
    }
}
