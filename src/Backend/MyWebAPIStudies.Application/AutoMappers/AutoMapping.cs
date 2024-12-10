using AutoMapper;
using MyWebAPIStudies.Communication.Requests;
using MyWebAPIStudies.Communication.Responses;

namespace MyWebAPIStudies.Application.AutoMappers
{
	public class AutoMapping : Profile
	{
		public AutoMapping()
		{
			RequestToDomain();
			DomainToResponse();
		}

		private void RequestToDomain()
		{
			CreateMap<RequestCreateUserJson,Domain.Entities.User>()
				.ForMember(dest=> dest.Password, opt=> opt.Ignore());

			#region dica
			//Essa dica de como usamos o AutoMappi caso a minha entidade seja diferente do meu mapping
			//CreateMap<RequestCreateUserJson, Domain.Entities.User>()
			//	.ForMember(dest => dest.Password, opt => opt.MapFrom(source => source.Password)); 
			#endregion
		}

		private void DomainToResponse()
		{
			CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
		}
	}
}
