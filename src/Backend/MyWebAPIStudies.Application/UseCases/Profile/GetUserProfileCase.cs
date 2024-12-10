using AutoMapper;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;
using MyWebAPIStudies.Domain.Repositories.User;

namespace MyWebAPIStudies.Application.UseCases.Profile
{
	public class GetUserProfileCase : IGetUserProfileCase
	{
		private readonly IUserReadOnlyRepository _userReadOnlyRepository;
		private readonly IMapper _mapper;

		public GetUserProfileCase(IUserReadOnlyRepository userReadOnlyRepository, IMapper mapper)
		{
			_userReadOnlyRepository = userReadOnlyRepository;
			_mapper = mapper;
		}

		public async Task<ResponseUserProfileJson> Execute(RequestProfileJson requestProfile)
		{
			var user = await _userReadOnlyRepository.GetUserProfile(requestProfile.Email);

			return _mapper.Map<ResponseUserProfileJson>(user);
		}
	}
}
