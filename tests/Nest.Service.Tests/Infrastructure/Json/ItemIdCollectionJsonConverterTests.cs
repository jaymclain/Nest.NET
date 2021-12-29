using System.Collections.Generic;
using System.Linq;
using Nest.NET.Service.Infrastructure.Json;
using Nest.NET.Service.Model;
using Newtonsoft.Json;
using Shouldly;
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
                ?.ToArray();

            // Assert
            response.ShouldNotBeEmpty();
            response.Length.ShouldBe(2);
            response[0].Id.ShouldBe("aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            response[0].Name.ShouldBe("Vacation Home");
            response[1].Id.ShouldBe("sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            response[1].Name.ShouldBe("Structure 1");
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
            response.ShouldNotBeEmpty();
            response.Count.ShouldBe(2);
            response[0].Id.ShouldBe("aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            response[0].Name.ShouldBe("Vacation Home");
            response[1].Id.ShouldBe("sN4-Q97UhX6Plmqwv6eTxVwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            response[1].Name.ShouldBe("Structure 1");
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
                ?.ToArray();

            // Assert
            response.ShouldNotBeEmpty();
            response[0].Id.ShouldBe("aNOFUasMKI98ilLy8GQRmlwrdERVd1TSSfxFnj4pGLM560U5C5xqEw");
            response[0].Name.ShouldBe("Vacation Home");
            response[0].CountryCode.ShouldBe("US");
            response[0].TimeZone.ShouldBe("America/Chicago");
            response[0].Away.ShouldBe("home");
            response[0].Wheres.Count().ShouldBe(2);
        }
    }
}
