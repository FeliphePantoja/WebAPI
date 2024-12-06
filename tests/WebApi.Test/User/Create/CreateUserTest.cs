using CommonTestUtil.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.User.Create
{
	public class CreateUserTest : IClassFixture<WebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		public CreateUserTest(WebApplicationFactory factory) => _httpClient = factory.CreateClient();

		[Fact]
		public async Task should_success_request_create()
		{
			var request = CreateUserValidatorBuilder.Build();

			var response = await _httpClient.PostAsJsonAsync("/api/User", request);

			response.StatusCode.Should().Be(HttpStatusCode.Created);

			await using var body = await response.Content.ReadAsStreamAsync();

			var data = await JsonDocument.ParseAsync(body);

			data.RootElement.GetProperty("name")
				.GetString()
				.Should()
				.NotBeNullOrWhiteSpace()
				.And
				.Be(request.Name);
		}
	}
}
