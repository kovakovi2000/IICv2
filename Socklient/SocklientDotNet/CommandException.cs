using System;

namespace SocklientDotNet
{
	public class CommandException : Exception
	{
		public Reply Reply { get; private set; }

		public CommandException(Reply reply)
		{
			Reply = reply;
		}

		public CommandException(string message, Reply reply)
			: base(message)
		{
			Reply = reply;
		}

		public CommandException(string message, Reply reply, Exception inner)
			: base(message, inner)
		{
			Reply = reply;
		}
	}
}
