
using Xunit;

namespace VintriBeers.Application.Tests
{
    [CollectionDefinition("ApplicationContextCollection")]
    public class OperationsContextCollection : ICollectionFixture<ApplicationTestBase>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
