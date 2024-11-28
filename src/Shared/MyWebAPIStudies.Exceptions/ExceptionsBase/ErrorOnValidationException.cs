namespace MyWebAPIStudies.Exceptions.ExceptionsBase
{
	public class ErrorOnValidationException : MyRecipeException
	{
		public IList<string> ErrorMessages { get; set; }
		public ErrorOnValidationException(IList<string> errorMessages) => ErrorMessages = errorMessages;
	}
}
