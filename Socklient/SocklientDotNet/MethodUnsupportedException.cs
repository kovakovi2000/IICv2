using System;

namespace SocklientDotNet
{
	public class MethodUnsupportedException : Exception
	{
		public Method ServerReplyMethod { get; private set; }

		public MethodUnsupportedException(Method serverReplyMethod)
		{
			ServerReplyMethod = serverReplyMethod;
		}

		public MethodUnsupportedException(string message, Method serverReplyMethod)
			: base(message)
		{
			ServerReplyMethod = serverReplyMethod;
		}

		public MethodUnsupportedException(string message, Method serverReplyMethod, Exception inner)
			: base(message, inner)
		{
			ServerReplyMethod = serverReplyMethod;
		}
	}
}
