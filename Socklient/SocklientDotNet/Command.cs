namespace SocklientDotNet
{
	public enum Command : byte
	{
		Connect = 1,
		/// <summary>
		/// Unsupported yet
		/// </summary>
		Bind,
		UdpAssociate
	}
}
