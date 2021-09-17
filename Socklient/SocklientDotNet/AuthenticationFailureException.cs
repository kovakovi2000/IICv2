using System;

namespace SocklientDotNet
{
	public class AuthenticationFailureException : Exception
	{
		public byte StatusCode { get; private set; }

		public AuthenticationFailureException(byte statusCode)
		{
			StatusCode = statusCode;
		}

		public AuthenticationFailureException(string message, byte statusCode)
			: base(message)
		{
			StatusCode = statusCode;
		}

		public AuthenticationFailureException(string message, byte statusCode, Exception inner)
			: base(message, inner)
		{
			StatusCode = statusCode;
		}
	}
}
