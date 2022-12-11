
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using VintriBeers.Domain.Model;

namespace VintriBeers.Externalities.ExternalServices.PunkApi
{
    public class PunkApiService : IPunkApiService
    {
        public PunkApiService()
        {

        }

        public async Task<bool> GetIsValidBeerId(int beerId)
        {
            bool isValidBeerId = false;
            
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.punkapi.com/v2/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(string.Format("beers/{0}", beerId.ToString()));
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    var beers = JsonConvert.DeserializeObject<List<Beer>>(responseContent);

                    isValidBeerId = ((beers != null) && (beers.Count > 0));
                }
            }

            return isValidBeerId;
        }

        public async Task<List<Beer>> GetBeers(string beerName)
        {
            List<Beer> beers = new List<Beer>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.punkapi.com/v2/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(string.Format("beers?beer_name={0}", beerName));
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    beers = JsonConvert.DeserializeObject<List<Beer>>(responseContent);
                }
            }

            return beers;
        }
    }
}
