using System.Collections.Generic;
using System.Linq;
using Nest.NET.Service.Infrastructure.Json;
using Nest.NET.Service.Model;
using Newtonsoft.Json;
using Xunit;

namespace Nest.NETCore11.Service.UnitTests.Infrastructure.Json
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
            Assert.Equal(response.Length, 2);
            Assert.Equal(response.First().Id, "aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            Assert.Equal(response.First().Name, "Vacation Home");
            Assert.Equal(response.Skip(1).First().Id, "sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            Assert.Equal(response.Skip(1).First().Name, "Structure 1");
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
            Assert.Equal(response.Count, 2);
            Assert.Equal(response.First().Id, "aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            Assert.Equal(response.First().Name, "Vacation Home");
            Assert.Equal(response.Skip(1).First().Id, "sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            Assert.Equal(response.Skip(1).First().Name, "Structure 1");
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
            Assert.Equal(response.Length, 1);
            Assert.Equal(response.First().Id, "aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            Assert.Equal(response.First().Name, "Vacation Home");
            Assert.Equal(response.First().CountryCode, "US");
            Assert.Equal(response.First().TimeZone, "America/Chicago");
            Assert.Equal(response.First().Away, "home");
            Assert.Equal(response.First().Wheres.Count(), 2);
        }
    }
}
