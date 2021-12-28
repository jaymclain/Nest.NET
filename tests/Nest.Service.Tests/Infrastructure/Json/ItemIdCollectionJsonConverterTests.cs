using System.Collections.Generic;
using System.Linq;
using Nest.NET.Service.Infrastructure.Json;
using Nest.NET.Service.Model;
using Newtonsoft.Json;
using Xunit;

namespace Nest.Service.UnitTests.Infrastructure.Json
{
    public class ItemIdCollectionJsonConverterTests
    {
        [Fact]
        public void When_Invoked_Should_Deserialize_Unnamed_Structures_Collection_As_Enumerable()
        {
            // Arrange
            const string Json = @"{
                ""aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw"": {
                    ""name"": ""Vacation Home"",
                    ""structure_id"": ""aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw""
                },
                ""sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw"": {
                    ""name"": ""Structure 1"",
                    ""structure_id"": ""sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw""
                }
            }";

            // Act
            var response = JsonConvert.DeserializeObject<IEnumerable<Structure>>(
                    Json, new ItemIdCollectionJsonConverter<IEnumerable<Structure>>())
                .ToArray();

            // Assert
            Assert.Equal(2, response.Length);
            Assert.Equal("aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw", response.First().Id);
            Assert.Equal("Vacation Home", response.First().Name);
            Assert.Equal("sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw", response.Skip(1).First().Id);
            Assert.Equal("Structure 1", response.Skip(1).First().Name);
        }

        [Fact]
        public void When_Invoked_Should_Deserialize_Unnamed_Structures_Collection_As_List()
        {
            // Arrange
            const string Json = @"{
                ""aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw"": {
                    ""name"": ""Vacation Home"",
                    ""structure_id"": ""aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw""
                },
                ""sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw"": {
                    ""name"": ""Structure 1"",
                    ""structure_id"": ""sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw""
                }
            }";

            // Act
            var response = JsonConvert.DeserializeObject<List<Structure>>(
                    Json, new ItemIdCollectionJsonConverter<List<Structure>>());

            // Assert
            Assert.Equal(2, response.Count);
            Assert.Equal("aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw", response.First().Id);
            Assert.Equal("Vacation Home", response.First().Name);
            Assert.Equal("sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw", response.Skip(1).First().Id);
            Assert.Equal("Structure 1", response.Skip(1).First().Name);
        }

        [Fact]
        public void When_Invoked_Should_Deserialize_Child_Unnamed_Structures_Collection_As_Enumerable()
        {
            // Arrange
            const string Json = @"{
                ""aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw"": {
                    ""name"": ""Vacation Home"",
                    ""country_code"": ""US"",
                    ""time_zone"": ""America/Chicago"",
                    ""away"": ""home"",
                    ""structure_id"": ""aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw"",
                    ""wheres"": {
                        ""bgRdRd-82X9Qe1uFn79D6tiOambJQsZmrNwn8TKzSwL-Q7xENySXSQ"": {
                            ""where_id"": ""bgRdRd-82X9Qe1uFn79D6tiOambJQsZmrNwn8TKzSwL-Q7xENySXSQ"",
                            ""name"": ""Backyard""
                        },
                        ""bgRdRd-82X9Qe1uFn79D6tiOambJQsZmrNwn8TKzSwLbmm8IKNWjFA"": {
                            ""where_id"": ""bgRdRd-82X9Qe1uFn79D6tiOambJQsZmrNwn8TKzSwLbmm8IKNWjFA"",
                            ""name"": ""Basement""
                        }
                    }
                }
            }";

            // Act
            var response = JsonConvert.DeserializeObject<IEnumerable<Structure>>(
                    Json, new ItemIdCollectionJsonConverter<IEnumerable<Structure>>())
                .ToArray();

            // Assert
            Assert.Single(response);
            Assert.Equal("aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw", response.First().Id);
            Assert.Equal("Vacation Home", response.First().Name);
            Assert.Equal("US", response.First().CountryCode);
            Assert.Equal("America/Chicago", response.First().TimeZone);
            Assert.Equal("home", response.First().Away);
            Assert.Equal(2, response.First().Wheres.Count());
        }
    }
}
