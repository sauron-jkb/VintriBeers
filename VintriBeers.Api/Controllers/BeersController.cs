
using System;
using System.Threading.Tasks;
using System.Web.Http;

using MediatR;
using Microsoft.Extensions.Logging;

using VintriBeers.Application.Beers.Queries;
using VintriBeers.Domain.Model;

namespace VintriBeers.Api.Controllers
{
    public class BeersController : BaseController
    {
        private readonly ILogger<BeersController> _logger;

        public BeersController(IMediator mediator, ILogger<BeersController> logger)
            : base(mediator)
        {
            _logger = logger;
        }

        [System.Web.Mvc.HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] GetBeerListing.Query request)
        {
            _logger.LogInformation("On {0} {1} received an API request with {3}", DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss"), ActionContext.Request.RequestUri.ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(request));

            var response = new ApiResponse
            {
                ActionResult = await Mediator.Send(request) 
            };

            return base.Ok(response);
        }
    }
}
