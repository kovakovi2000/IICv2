using System;

namespace SocklientDotNet
{
	public class ProtocolErrorException : Exception
	{
		public ProtocolErrorException()
		{
		}

		public ProtocolErrorException(string message)
			: base(message)
		{
		}

		public ProtocolErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
