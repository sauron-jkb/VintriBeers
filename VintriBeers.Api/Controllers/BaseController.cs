
using System.Web.Http;

using MediatR;

namespace VintriBeers.Api.Controllers
{
    public class BaseController : ApiController
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator;

        protected BaseController(IMediator mediator) 
        {
            _mediator = mediator;
        }
    }
}