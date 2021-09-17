namespace SocklientDotNet
{
	public enum SocksStatus
	{
		/// <summary>
		/// Before handshake and authentication.
		/// </summary>
		Initial,
		/// <summary>
		/// After handshake and authentication, able to send data.
		/// </summary>
		Connected,
		/// <summary>
		/// Connection closed, can not reuse.
		/// </summary>
		Closed
	}
}
