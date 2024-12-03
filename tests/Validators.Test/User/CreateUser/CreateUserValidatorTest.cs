using CommonTestUtil.Requests;
using FluentAssertions;
using MyWebAPIStudies.Application.UseCases.User.Create;
using MyWebAPIStudies.Exceptions;

namespace Validators.Test.User.CreateUser
{
	public class CreateUserValidatorTest
	{
		[Fact]
		public void Should_return_Success_User_Parameter()
		{
			var result = new CreateUserValidator().Validate(CreateUserValidatorBuilder.Build());

			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Should_return_Error_User_Name_Parameter()
		{
			var request = CreateUserValidatorBuilder.Build();
			request.Name = string.Empty;

			var result = new CreateUserValidator().Validate(request);

			result.IsValid.Should().BeFalse();
			result.Errors.Should()
				.ContainSingle()
				.And
				.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
		}

		[Fact]
		public void Should_return_Error_User_Email_Empyt_Parameter()
		{
			var request = CreateUserValidatorBuilder.Build();
			request.Email = string.Empty;

			var result = new CreateUserValidator().Validate(request);

			result.IsValid.Should().BeFalse();
			result.Errors.Should()
				.ContainSingle()
				.And
				.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
		}

		[Fact]
		public void Should_return_Error_User_Email_Invalid_Parameter()
		{
			var request = CreateUserValidatorBuilder.Build();
			request.Email = "email.com";

			var result = new CreateUserValidator().Validate(request);

			result.IsValid.Should().BeFalse();
			result.Errors.Should()
				.ContainSingle()
				.And
				.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.FORMAT_EMAIL_INCORRECT));
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		[InlineData(5)]
		public void Should_return_Error_User_Password_Invalid_Parameter(int passwordSize)
		{
			var request = CreateUserValidatorBuilder.Build(passwordSize);

			var result = new CreateUserValidator().Validate(request);

			result.IsValid.Should().BeFalse();
			result.Errors.Should()
				.ContainSingle()
				.And
				.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));

		}
	}
}
