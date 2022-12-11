
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using VintriBeers.Application.BeerRatings.Commands;

namespace VintriBeers.Api
{
    public class UserRatingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool continueExecuting = false;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$");

            var parameters = actionContext.ActionDescriptor.GetParameters();

            foreach (var parameter in parameters)
            {
                if (actionContext.ActionArguments.ContainsKey(parameter.ParameterName))
                {
                    object parameterValue = actionContext.ActionArguments[parameter.ParameterName];

                    if (parameterValue.GetType() == typeof(AddBeerRating.Command))
                    {
                        Match regexMatch = regex.Match(((AddBeerRating.Command)parameterValue).UserName);
                        continueExecuting = regexMatch.Success;
                        break;
                    }
                }
            }

            if (continueExecuting)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.PreconditionFailed);
            }
        }
    }
}