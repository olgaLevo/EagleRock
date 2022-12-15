using EagleBot.Api.Tests.Models;
using EagleBot.Api.Tests.TestHelpers.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace EagleBot.Api.Tests.Controllers
{
    public class EagleBotControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public EagleBotControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/EagleBot/");
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsExpectedArrayOfProducts()
        {
            _factory.FakeRedisCacheService.ResetDefaultRecords(useCustomIfAvailable: false);

            var records = await _client.GetFromJsonAsync<ExpectedEagleBotModel[]>("");

            Assert.NotNull(records);
            Assert.Equal(_factory.FakeRedisCacheService.Records.GroupBy(m=>m.BotId).Count(), records.Count());
        }

        [Fact]
        public async Task Get_ReturnsExpectedRecord()
        {
            var firstProduct = _factory.FakeRedisCacheService.Records.GroupBy(m=>m.BotId).First();

            var records = await _client.GetFromJsonAsync<ExpectedEagleBotModel[]>("");

      
            Assert.Equal(firstProduct.Key, records.First().Id);
        }

        [Theory]
        [MemberData(nameof(GetInvalidInputs))]
        public async Task Post_InvalidData_ReturnsBadRequest(TestEagleBotInputModel eagleInputModel)
        {
            var response = await _client.PostAsJsonAsync("", eagleInputModel, JsonSerializerHelper.DefaultSerialisationOptions);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetInvalidInputsAndProblemDetailsErrorValidator))]
        public async Task Post_WithInvalidName_ReturnsExpectedProblemDetails(TestEagleBotInputModel productInputModel,
         Action<KeyValuePair<string, string[]>> validator)
        {
            var response = await _client.PostAsJsonAsync("", productInputModel, JsonSerializerHelper.DefaultSerialisationOptions);

            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Collection(problemDetails.Errors, validator);
        }

        [Fact]
        public async Task Post_WithValidRecord_ReturnsCreatedResult()
        {
            var id = Guid.NewGuid();
            var content = GetValidProductJsonContent(id);

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        private static JsonContent GetValidProductJsonContent(Guid? id = null)
        {
            return JsonContent.Create(GetValidRecordInputModel(id));
        }

        private static TestEagleBotInputModel GetValidRecordInputModel(Guid? id = null)
        {
            return new TestEagleBotInputModel
            {
                Id = id is object ? id.Value : Guid.NewGuid(),
                Location = "-26.564735, 153.084453",
                RoadName = "David Low way",
                Status = "Active",
                Time = DateTime.Now.AddHours(-1),
                AverageVehicleSpeed = 100,
                TrafficFlowDirection = "south",
                TrafficFlowRate = 8
            };
        }
        public static IEnumerable<object[]> GetInvalidInputs()
        {
            return GetInvalidInputsAndProblemDetailsErrorValidator().Select(x => new[] { x[0] });
        }


        public static IEnumerable<object[]> GetInvalidInputsAndProblemDetailsErrorValidator()
        {
            var testData = new List<object[]>
            {
                new object[]
                {
                    GetValidRecordInputModel().CloneWith(x => x.Id = default),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("BotId", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("A non-default ID is required.", error);
                    })
                },

                new object[]
                {
                    GetValidRecordInputModel().CloneWith(x => x.Id = Guid.Empty),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("BotId", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("A non-default ID is required.", error);
                    })
                },

                new object[]
                {
                    GetValidRecordInputModel().CloneWith(x => x.Location = string.Empty),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("Location", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The Location field is required.", error);
                    })
                },

                new object[]
                {
                    GetValidRecordInputModel().CloneWith(x => x.Location =  new string('a', 257)),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                         Assert.Equal("Location", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The field Location must be a string with a maximum length of 256.", error);
                    })
                },

                new object[]
                {
                    GetValidRecordInputModel().CloneWith(x => x.RoadName = null),
                    new Action<KeyValuePair<string, string[]>>(kvp =>
                    {
                        Assert.Equal("RoadName", kvp.Key);
                        var error = Assert.Single(kvp.Value);
                        Assert.Equal("The RoadName field is required.", error);
                    })
                },

               //todo: implement the rest of the input validation
            };

            return testData;
        }
    }
}
