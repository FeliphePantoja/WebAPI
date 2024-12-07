namespace MyWebAPIStudies.Exceptions.ExceptionsBase
{
	public class InvalidLoginException : MyRecipeException
	{
		public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
		{

		}
	}
}
