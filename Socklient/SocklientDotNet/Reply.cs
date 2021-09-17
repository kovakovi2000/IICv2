namespace SocklientDotNet
{
	public enum Reply : byte
	{
		Successed,
		GeneralFailure,
		ConnectionNotAllowed,
		NetworkUnreachable,
		HostUnreachable,
		ConnectionRefused,
		TTLExpired,
		CommandNotSupported,
		AddressTypeNotSupported
	}
}
