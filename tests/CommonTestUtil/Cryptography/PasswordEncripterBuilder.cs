using MyWebAPIStudies.Application.Cryptography;

namespace CommonTestUtil.Cryptography
{
	public class PasswordEncripterBuilder
	{
		public static PasswordEncripter Build()=> new PasswordEncripter("123456abcd");
	}
}
