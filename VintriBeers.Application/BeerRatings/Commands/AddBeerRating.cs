
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;
using MediatR;

using VintriBeers.Domain.Data;
using VintriBeers.Domain.Model;
using VintriBeers.Externalities.ExternalServices.PunkApi;

namespace VintriBeers.Application.BeerRatings.Commands
{
    public class AddBeerRating
    {
        public class Command : IRequest<bool>
        {
            public int BeerId { get; set; }
            public string UserName { get; set; }
            public int Rating { get; set; }
            public string Comments { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IPunkApiService punkApiService)
            {
                RuleFor(r => r.BeerId).MustAsync(async (id, cancellationToken) =>
                {
                    return await punkApiService.GetIsValidBeerId(id);
                }).WithMessage("BeerId must be valid.");

                RuleFor(r => r.UserName).NotNull().NotEmpty().WithMessage("UserName must be supplied.");
                RuleFor(r => r.Rating).Must((rating) => rating >= 1 && rating <= 5).WithMessage("Rating must be in the range of 1 to 5.");
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IUserRatingsContext _userRatingsContext;
            private readonly IPunkApiService _punkApiService;
            
            public Handler(IUserRatingsContext userRatingsContext, IPunkApiService punkApiService)
            {
                _userRatingsContext = userRatingsContext;
                _punkApiService = punkApiService;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                bool successfulOperation = false;

                throw new Exception("Is this owin?");

                Validator validator = new Validator(_punkApiService);
                ValidationResult results = validator.Validate(request);

                if (results.IsValid)
                {
                    _userRatingsContext.AddUserRating(new UserRating
                    {
                        BeerId = request.BeerId,
                        UserName = request.UserName,
                        Rating = request.Rating,
                        Comments = request.Comments
                    });

                    successfulOperation = true;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("Error trying to add beer rating.");
                    stringBuilder.Append(results.ToString());

                    throw new Exception(stringBuilder.ToString());
                }

                return successfulOperation;
            }
        }
    }
}
