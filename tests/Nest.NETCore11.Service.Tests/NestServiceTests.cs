using Nest.NET.Service;
using Xunit;

namespace Nest.NETCore11.Service.UnitTests
{
    public class NestServiceTests
    {
        [Fact(Skip = "Manual")]
        public void When_Created_Should_Succeed()
        {
            var service = NestService.Create(new ServiceOptions { AccessToken = "{Your Access Token}" });
            var structures = service.Structures;

            Assert.NotNull(service);
            Assert.NotNull(structures);
            Assert.NotEmpty(structures);
        }
    }
}
