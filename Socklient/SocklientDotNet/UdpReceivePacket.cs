using System.Net;

namespace SocklientDotNet
{
	public class UdpReceivePacket
	{
		public byte[] Buffer { get; }

		public string RemoteHost { get; }

		public IPAddress RemoteAddress { get; }

		public int RemotePort { get; }

		public UdpReceivePacket(byte[] buffer, string remoteHost, IPAddress remoteAddress, int remotePort)
		{
			Buffer = buffer;
			RemoteHost = remoteHost;
			RemoteAddress = remoteAddress;
			RemotePort = remotePort;
		}
	}
}
