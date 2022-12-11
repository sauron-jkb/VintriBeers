
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VintriBeers.Domain.Model;

namespace VintriBeers.Externalities.ExternalServices.PunkApi
{
    public interface IPunkApiService
    {
        Task<bool> GetIsValidBeerId(int beerId);
        Task<List<Beer>> GetBeers(string beerName);
    }
}
