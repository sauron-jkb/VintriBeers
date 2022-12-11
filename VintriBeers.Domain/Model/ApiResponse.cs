
using Newtonsoft.Json;

namespace VintriBeers.Domain.Model
{
    public class ApiResponse
    {
        public dynamic LoginSession { get; set; }
        public dynamic ActionResult { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
