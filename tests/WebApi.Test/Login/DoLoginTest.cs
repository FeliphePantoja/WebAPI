using Azure.Core;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using MyWebAPIStudies.Communication.Requests;
using FluentAssertions;
using CommonTestUtil.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MyWebAPIStudies.Exceptions;

namespace WebApi.Test.Login
{
	public class DoLoginTest : IClassFixture<WebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly string _urlBase = "/api/login";

		private readonly string _name;
		private readonly string _email;
		private readonly string _password;

		public DoLoginTest(WebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_name = factory.GetUser.Name;
			_email = factory.GetUser.Email;
			_password = factory.GetUser.Password;
		}

		[Fact]
		public async Task Should_return_success_login()
		{
			var request = new RequestLoginJson()
			{
				Email = _email,
				Password = _password
			};

			var response = await _httpClient.PostAsJsonAsync(_urlBase, request);

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			await using var body = await response.Content.ReadAsStreamAsync();

			var data = await JsonDocument.ParseAsync(body);

			data.RootElement.GetProperty("name")
				.GetString()
				.Should()
				.NotBeNullOrWhiteSpace()
				.And
				.Be(_name);
		}

		[Fact]
		public async Task Should_return_fail_login()
		{
			var request = RequestLoginJsonBuilder.Build();

			var response = await _httpClient.PostAsJsonAsync(_urlBase, request);

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

			await using var responseBody = await response.Content.ReadAsStreamAsync();

			var responseData = await JsonDocument.ParseAsync(responseBody);

			var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

			var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID");

			errors.Should().ContainSingle().And.Contain(error=>error.GetString()!.Equals(expectedMessage));
		}
	}
}
