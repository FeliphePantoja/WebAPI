﻿namespace MyWebAPIStudies.Communication.Requests
{
	public class RequestCreateUserJson
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
