using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MyWebAPIStudies.API.Converters
{
	public partial class StringConverter : JsonConverter<string>
	{
		/// <summary>
		/// Remove space in string
		/// ex :    Felipe       Pantoja    
		/// output : Felipe Pantoja
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="typeToConvert"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString()?.Trim();

			if (value is null)
				return null;

			return RemoveExtraWhiteSpaces().Replace(value, " ");
		}

		public override void Write(Utf8JsonWriter writer,
			string value,
			JsonSerializerOptions options) => writer.WriteStringValue(value);

		[GeneratedRegex(@"\s+")]
		private static partial Regex RemoveExtraWhiteSpaces();
	}
}
