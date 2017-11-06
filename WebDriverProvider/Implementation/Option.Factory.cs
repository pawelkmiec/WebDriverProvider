﻿namespace WebDriverProvider.Implementation
{
	internal static class Option
	{
		public static Option<T> Some<T>(T value) => new Option<T>(value, true);

		public static Option<T> None<T>() => new Option<T>(default(T), false);
	}
}