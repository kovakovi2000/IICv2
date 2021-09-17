namespace SocklientDotNet
{
	public enum Method : byte
	{
		NoAuthentication = 0,
		GSSAPI = 1,
		UsernamePassword = 2,
		NoAcceptable = byte.MaxValue
	}
}
