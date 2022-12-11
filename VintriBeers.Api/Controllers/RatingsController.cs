
using System;
using System.Threading.Tasks;
using System.Web.Http;

using MediatR;
using Microsoft.Extensions.Logging;

using VintriBeers.Application.BeerRatings.Commands;
using VintriBeers.Domain.Model;

namespace VintriBeers.Api.Controllers
{
    public class RatingsController : BaseController
    {
        private readonly ILogger<RatingsController> _logger;

        public RatingsController(IMediator mediator, ILogger<RatingsController> logger) 
            : base(mediator)
        {
            _logger = logger;
        }

        [System.Web.Mvc.HttpPost]
        [UserRatingFilter]
        public async Task<IHttpActionResult> Post(int id, [FromBody] AddBeerRating.Command request)
        {
            _logger.LogInformation("On {0} {1} received an API request with {3}", DateTime.Now.ToString("MMM dd, yyyy HH:mm:ss"), ActionContext.Request.RequestUri.ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(request));

            request.BeerId = id;

            var response = new ApiResponse
            {
                ActionResult = await Mediator.Send(request)
            };

            return base.Ok(response);
        }
    }
}
