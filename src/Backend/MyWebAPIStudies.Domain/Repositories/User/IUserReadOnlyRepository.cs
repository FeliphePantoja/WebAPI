﻿namespace MyWebAPIStudies.Domain.Repositories.User
{
	public interface IUserReadOnlyRepository
	{
		Task<bool> ExistActiveUserWithEmail(string email);
	}
}
