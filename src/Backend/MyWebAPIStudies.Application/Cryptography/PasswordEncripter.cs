using System.Security.Cryptography;
using System.Text;

namespace MyWebAPIStudies.Application.Cryptography
{
	public class PasswordEncripter
	{

		public string Encrypt(string input)
		{
			var keyAddition = "CDB";
			var newPassw = $"{input}{keyAddition}";

			var bytes = Encoding.UTF8.GetBytes(input);
			var hashBytes = SHA512.HashData(bytes);
			return newStringBytes(hashBytes);
		}

		private string newStringBytes(byte[] hashBytes)
		{
			var stringB  = new StringBuilder();
			foreach (byte b in hashBytes)
			{
				var hex = b.ToString("x2");
				stringB.Append(hex);
			}

			return stringB.ToString();
		}
	}
}
