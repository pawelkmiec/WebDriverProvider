using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebDriverProvider.Implementation
{
	internal struct Option<T>
	{
		private readonly bool _hasValue;

		private readonly T _value;

		internal Option(T value, bool hasValue)
		{
			_value = value;
			_hasValue = hasValue;
		}

		public bool HasValue => _hasValue;

		public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
		{
			if (some == null) throw new ArgumentNullException(nameof(some));
			if (none == null) throw new ArgumentNullException(nameof(none));
			return _hasValue ? some(_value) : none();
		}

		public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> some, Func<Task<TResult>> none)
		{
			if (some == null) throw new ArgumentNullException(nameof(some));
			if (none == null) throw new ArgumentNullException(nameof(none));
			if (_hasValue)
			{
				var someResult = await some(_value);
				return someResult;
			}
			var noneResult = await none();
			return noneResult;
		}

		public void Match(Action<T> some, Action none)
		{
			if (some == null) throw new ArgumentNullException(nameof(some));
			if (none == null) throw new ArgumentNullException(nameof(none));

			if (_hasValue)
			{
				some(_value);
			}
			else
			{
				none();
			}
		}

		public async Task Match(Func<T, Task> some, Func<Task> none)
		{
			if (some == null) throw new ArgumentNullException(nameof(some));
			if (none == null) throw new ArgumentNullException(nameof(none));

			if (_hasValue)
			{
				await some(_value);
			}
			else
			{
				await none();
			}
		}

		public bool Equals(Option<T> other)
		{
			return _hasValue == other._hasValue && EqualityComparer<T>.Default.Equals(_value, other._value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Option<T> option && Equals(option);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_hasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(_value);
			}
		}
	}
}
