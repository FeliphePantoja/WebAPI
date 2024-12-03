using AutoMapper;
using MyWebAPIStudies.Application.AutoMappers;

namespace CommonTestUtil.Mapper
{
	public static class MapperBuilder
	{

		public static IMapper Build()
		{
			return new MapperConfiguration(options =>
			{
				options.AddProfile(new AutoMapping());
			}).CreateMapper();
		}

	}
}
