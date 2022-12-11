
using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

using VintriBeers.Application.BeerRatings.Commands;

namespace VintriBeers.Application.Tests.BeerRatings.Commands
{
    [Collection("ApplicationContextCollection")]
    public class AddBeerRatingCommandHandlerTests
    {
		ApplicationTestBase _applicationTestBase;

		public AddBeerRatingCommandHandlerTests(ApplicationTestBase applicationTestBase)
		{
			_applicationTestBase = applicationTestBase;
		}

        [Fact]
        public async Task AddUserRatingSuccessfully()
        {
            // Arrange
            var command = new AddBeerRating.Command { BeerId = 1, Comments = "Some comments", Rating = 1, UserName = "alias@email.com" };
            var handler = new AddBeerRating.Handler(_applicationTestBase.UserRatingsContext, _applicationTestBase.PunkApiService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddUserRatingFailsOnRating()
        {
            // Arrange
            var command = new AddBeerRating.Command { BeerId = 1, Comments = "Some comments", Rating = 9999, UserName = "alias@email.com" };
            var handler = new AddBeerRating.Handler(_applicationTestBase.UserRatingsContext, _applicationTestBase.PunkApiService);

            // Act
            var result = await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal("Error trying to add beer rating.\r\nRating must be in the range of 1 to 5.", result.Message);
        }

        [Fact]
        public async Task AddUserRatingFailsOnBeer()
        {
            // Arrange
            var command = new AddBeerRating.Command { BeerId = 0, Comments = "Some comments", Rating = 3, UserName = "alias@email.com" };
            var handler = new AddBeerRating.Handler(_applicationTestBase.UserRatingsContext, _applicationTestBase.PunkApiService);

            // Act
            var result = await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal("Error trying to add beer rating.\r\nBeerId must be valid.", result.Message);
        }

        [Fact]
        public async Task AddUserRatingFailsOnUserName()
        {
            // Arrange
            var command = new AddBeerRating.Command { BeerId = 1, Comments = "Some comments", Rating = 3, UserName = "" };
            var handler = new AddBeerRating.Handler(_applicationTestBase.UserRatingsContext, _applicationTestBase.PunkApiService);

            // Act
            var result = await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(command, CancellationToken.None));

            Assert.Equal("Error trying to add beer rating.\r\nUserName must be supplied.", result.Message);
        }
    }
}
