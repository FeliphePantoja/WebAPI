using AutoMapper;
using MyWebAPIStudies.Application.Cryptography;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Domain.Repositories;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Exceptions;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace MyWebAPIStudies.Application.UseCases.User.Create
{
	public class CreateUserUseCase : ICreateUserCase
	{
		private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
		private readonly IUserReadOnlyRepository _userReadOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly PasswordEncripter _passwordEncripter;

		public CreateUserUseCase(IUserWriteOnlyRepository userWriteOnlyRepository,
			IUserReadOnlyRepository userReadOnlyRepository,
			IUnitOfWork unitOfWork,
			PasswordEncripter passwordEncripter,
			IMapper mapper)
		{
			this._userWriteOnlyRepository = userWriteOnlyRepository;
			this._userReadOnlyRepository = userReadOnlyRepository;
			this._unitOfWork = unitOfWork;
			this._passwordEncripter = passwordEncripter;
			this._mapper = mapper;
		}

		public async Task<ResponseCreateUserJson> Execute(RequestCreateUserJson newUser)
		{
			await Validate(newUser);

			var user = this._mapper.Map<Domain.Entities.User>(newUser);
			user.Password = this._passwordEncripter.Encrypt(newUser.Password);

			await _userWriteOnlyRepository.Add(user);
			await _unitOfWork.Commit();

			return new ResponseCreateUserJson
			{
				Name = user.Name,
			};
		}

		private async Task Validate(RequestCreateUserJson newUser)
		{
			var result = new CreateUserValidator().Validate(newUser);

			var emailExist = await this._userReadOnlyRepository.ExistActiveUserWithEmail(newUser.Email);

			if (emailExist)
				result.Errors
					.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_EXISTE));

			if (!result.IsValid)
			{
				var errorMSGs = result.Errors.Select(x => x.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMSGs);
			}
		}
	}
}
