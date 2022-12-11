
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;
using MediatR;

using VintriBeers.Domain.Data;
using VintriBeers.Domain.Model;
using VintriBeers.Externalities.ExternalServices.PunkApi;

namespace VintriBeers.Application.Beers.Queries
{
    public class GetBeerListing
    {
        public class Query : IRequest<List<Beer>>
        {
            public string BeerName { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(r => r.BeerName).NotNull().NotEmpty().WithMessage("A beer name must be supplied.");
            }
        }

        public class Handler : IRequestHandler<Query, List<Beer>>
        {
            private readonly IUserRatingsContext _userRatingsContext;
            private readonly IPunkApiService _punkApiService;

            public Handler(IUserRatingsContext userRatingsContext, IPunkApiService punkApiService)
            {
                _userRatingsContext = userRatingsContext;
                _punkApiService = punkApiService;
            }

            public async Task<List<Beer>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Beer> beers = new List<Beer>();

                Validator validator = new Validator();
                ValidationResult results = validator.Validate(request);

                if (results.IsValid)
                {
                    beers = await _punkApiService.GetBeers(request.BeerName);

                    foreach (var beer in beers)
                    {
                        var query =
                            (
                                from ur in _userRatingsContext.UserRatings
                                where (ur.BeerId == beer.Id)
                                select ur
                            );

                        beer.UserRatings = query.AsQueryable().ToList();
                    }
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Error trying to get beers.");
                    stringBuilder.Append(results.ToString());

                    throw new Exception(stringBuilder.ToString());
                }

                return beers;
            }
        }
    }
}
