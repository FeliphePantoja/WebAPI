using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Domain.Repositories;
using MyWebAPIStudies.Domain.Repositories.UpdateUser;
using MyWebAPIStudies.Domain.Repositories.User;
using MyWebAPIStudies.Exceptions;
using MyWebAPIStudies.Exceptions.ExceptionsBase;

namespace MyWebAPIStudies.Application.UseCases.Update
{
	internal class UpdateUserCase : IUpdateUserCase
	{
		private readonly IUserUpdateOnlyRepository _repository;
		private readonly IUserReadOnlyRepository _userReadOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateUserCase(IUserUpdateOnlyRepository repository,
			IUserReadOnlyRepository userReadOnlyRepository,
			IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_userReadOnlyRepository = userReadOnlyRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task Execute(RequestUpdateUserJson request)
		{
			var profileUser = await _userReadOnlyRepository
				.GetUserProfile(request.Email);

			await Validate(request, profileUser!.Email);

			var user = await _repository.GetById(profileUser!.Id);

			user.Name = request.Name;
			user.Email = request.Email;

			_repository.Update(user);

			await _unitOfWork.Commit();
		}

		private async Task Validate(RequestUpdateUserJson request, string currentEmail)
		{
			var validator = new UpdateUserValidator();
			var result = validator.Validate(request);

			if (!currentEmail.Equals(request.Email))
			{
				var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
				if (userExist)
					result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID));
			}

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
				throw new ErrorOnValidationException(errorMessages);
			}

		}
	}
}
