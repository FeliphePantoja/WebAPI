using AutoMapper;
using MyWebAPIStudies.Communication.Requests;

namespace MyWebAPIStudies.Application.AutoMappers
{
	public class AutoMapping : Profile
	{
		public AutoMapping()
		{
			RequestToDomain();
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
	}
}
