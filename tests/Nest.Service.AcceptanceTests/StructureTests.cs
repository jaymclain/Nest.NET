using System.Linq;
using Nest.NET.Service;
using Xunit;

namespace Nest.Service.AcceptanceTests;

public class StructureTests
{
    [Fact]
    public void When_Invoked_Should_Get_CurrentPerson()
    {
        // Arrange
        var service = NestService.Create(new TestServiceOptions());

        // Act
        var structures = service.Structures.ToList();

        // Assert
        Assert.NotNull(structures);
        Assert.NotEmpty(structures);
        Assert.True(!string.IsNullOrEmpty(structures.First().Id));
        Assert.NotEmpty(structures.Skip(1).First().Thermostats);
    }
}