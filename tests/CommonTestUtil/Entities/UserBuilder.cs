﻿using Bogus;
using CommonTestUtil.Cryptography;
using MyWebAPIStudies.Domain.Entities;

namespace CommonTestUtil.Entities
{
	public class UserBuilder
	{
		public static (User user, string password) Build()
		{
			var passwordEncripter = PasswordEncripterBuilder.Build();
			var password = new Faker().Internet.Password();

			var newUser = new Faker<User>()
				.RuleFor(user => user.Id, () => 1)
				.RuleFor(user => user.Name, (f) => f.Person.FirstName)
				.RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
				.RuleFor(user => user.Password, (f) => passwordEncripter.Encrypt(password));

			return (newUser, password);
		}
	}
}
