
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

namespace VintriBeers.Application.BeerRatings.Queries
{
    public class GetBeerUserRatings
    {
        public class Query : IRequest<List<UserRating>>
        {
            public int BeerId { get; set; }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(r => r.BeerId).GreaterThan((int)0).WithMessage("A beer id must be supplied.");
            }
        }

        public class Handler : IRequestHandler<Query, List<UserRating>>
        {
            private readonly IUserRatingsContext _userRatingsContext;

            public Handler(IUserRatingsContext userRatingsContext)
            {
                _userRatingsContext = userRatingsContext;
            }

            public async Task<List<UserRating>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<UserRating> userRatings = new List<UserRating>();

                Validator validator = new Validator();
                ValidationResult results = validator.Validate(request);

                if (results.IsValid)
                {
                    var query =
                        (
                            from ur in _userRatingsContext.UserRatings
                            where (ur.BeerId == request.BeerId)
                            select ur
                        );

                    userRatings = query.AsQueryable().ToList();
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Error trying to get beer user ratings.");
                    stringBuilder.Append(results.ToString());

                    throw new Exception(stringBuilder.ToString());
                }

                return userRatings;
            }
        }
    }
}
