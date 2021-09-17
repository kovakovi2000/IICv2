using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocklientDotNet
{
	public sealed class Socklient : IDisposable
	{
		private const byte VERSION = 5;

		private const byte AUTHENTICATION_VERSION = 1;

		private const int IPv4AddressBytes = 4;

		private const int IPv6AddressBytes = 16;

		private string _socksServerHost;

		private int _socksServerPort;

		private IPEndPoint _socksServerEndPoint;

		private NetworkStream _stream;

		private Command _socksType;

		private bool _disposed;

		private IPEndPoint _remoteEndPoint = new IPEndPoint(IPAddress.Loopback, 0);

		/// <summary>
		/// ATYP
		/// </summary>
		public AddressType BoundType { get; private set; }

		/// <summary>
		/// BND.ADDR, when ATYP is a Domain
		/// </summary>
		public string BoundDomain { get; private set; }

		/// <summary>
		/// BND.ADDR, when ATYP either IPv4 or IPv6
		/// </summary>
		public IPAddress BoundAddress { get; private set; }

		/// <summary>
		/// BND.PORT
		/// </summary>
		public int BoundPort { get; private set; }

		/// <summary>
		/// Get underlying TcpClient for more fine-grained control when you are using CONNECT mode
		/// </summary>
		public TcpClient TCP { get; private set; }

		/// <summary>
		/// Get underlying UdpClient for more fine-grained control when you are using UDP-ASSOCIATE mode
		/// </summary>
		public UdpClient UDP { get; private set; }

		/// <summary>
		/// Get current status of socklient.
		/// </summary>
		public SocksStatus Status { get; private set; }

		public NetworkCredential Credential { get; set; }

		/// <summary>
		/// Construct a socklient client with specified SOCKS5 server
		/// </summary>
		/// <param name="socksServerHost">Socks5 Server hostname or address</param>
		/// <param name="port">SOCKS5 protocol service port</param>
		public Socklient(string socksServerHost, int port)
			: this(socksServerHost, port, null)
		{
		}

		/// <summary>
		/// Construct a socklient client with specified SOCKS5 server that requires a basic username/password authentication
		/// </summary>
		/// <param name="socksServerHost">Socks5 Server hostname or address</param>
		/// <param name="port">SOCKS5 protocol service port</param>
		/// <param name="credential">a simple credential contains username and password for authentication</param>
		public Socklient(string socksServerHost, int port, NetworkCredential credential)
		{
			_socksServerHost = socksServerHost;
			_socksServerPort = port;
			Credential = credential;
			if (IPAddress.TryParse(socksServerHost, out var address))
			{
				_socksServerEndPoint = new IPEndPoint(address, port);
				TCP = new TcpClient(address.AddressFamily);
			}
			else
			{
				TCP = new TcpClient();
			}
		}

		/// <summary>
		/// Construct a socklient client with specified SOCKS5 server
		/// </summary>
		/// <param name="socksServerAddress"></param>
		/// <param name="port"></param>
		public Socklient(IPAddress socksServerAddress, int port)
			: this(new IPEndPoint(socksServerAddress, port), null)
		{
		}

		/// <summary>
		/// Construct a socklient client with specified SOCKS5 server that requires a basic username/password authentication
		/// </summary>
		/// <param name="socksServerAddress"></param>
		/// <param name="port"></param>
		/// <param name="credential"></param>
		public Socklient(IPAddress socksServerAddress, int port, NetworkCredential credential)
			: this(new IPEndPoint(socksServerAddress, port), credential)
		{
		}

		/// <summary>
		/// Construct a socklient client with specified SOCKS5 server
		/// </summary>
		/// <param name="socksServerEndPoint"></param>
		public Socklient(IPEndPoint socksServerEndPoint)
			: this(socksServerEndPoint, null)
		{
		}

		/// <summary>
		/// Construct a socklient client with specified SOCKS5 server that requires a basic username/password authentication
		/// </summary>
		/// <param name="socksServerEndPoint"></param>
		/// <param name="credential"></param>
		public Socklient(IPEndPoint socksServerEndPoint, NetworkCredential credential)
		{
			_socksServerEndPoint = socksServerEndPoint;
			Credential = credential;
			TCP = new TcpClient(socksServerEndPoint.AddressFamily);
		}

		/// <summary>
		/// Send a connect command to SOCKS5 server for TCP relay
		/// </summary>
		/// <param name="destHost">The destination host you want to communicate via socks server</param>
		/// <param name="destPort">The destination port of the host</param>
		public void Connect(string destHost, int destPort)
		{
			Connect(destHost, null, destPort);
		}

		/// <summary>
		/// Send a connect command to SOCKS5 server for TCP relay
		/// </summary>
		/// <param name="destAddress">The destination address you want to communicate via socks server</param>
		/// <param name="destPort">The destination port of the host</param>
		public void Connect(IPAddress destAddress, int destPort)
		{
			Connect(null, destAddress, destPort);
		}

		/// <summary>
		/// Connect internal implementation
		/// </summary>
		/// <param name="destHost">The destination host you want to communicate via socks server</param>
		/// <param name="destAddress">The destination address you want to communicate via socks server</param>
		/// <param name="destPort">The destination port of the host</param>
		private void Connect(string destHost, IPAddress destAddress, int destPort)
		{
			if (Status != 0)
			{
				throw new InvalidOperationException("This instance already connected.");
			}
			if (_socksServerEndPoint != null)
			{
				TCP.Connect(_socksServerEndPoint);
			}
			else
			{
				TCP.Connect(_socksServerHost, _socksServerPort);
			}
			_stream = TCP.GetStream();
			HandshakeAndAuthentication(Credential);
			SendCommand(Command.Connect, destHost, destAddress, destPort);
			_socksType = Command.Connect;
			Status = SocksStatus.Connected;
		}

		/// <summary>
		/// Send a connect command to SOCKS5 server for TCP relay as an asynchronous operation
		/// </summary>
		/// <param name="destHost">The destination host you want to communicate via socks server</param>
		/// <param name="destPort">The destination port of the host</param>
		public Task ConnectAsync(string destHost, int destPort)
		{
			return ConnectAsync(destHost, null, destPort);
		}

		/// <summary>
		/// Send a connect command to SOCKS5 server for TCP relay as an asynchronous operation
		/// </summary>
		/// <param name="destAddress">The destination host you want to communicate via socks server</param>
		/// <param name="destPort">The destination port of the host</param>
		/// <returns></returns>
		public Task ConnectAsync(IPAddress destAddress, int destPort)
		{
			return ConnectAsync(null, destAddress, destPort);
		}

		/// <summary>
		/// ConnectAsync internal implementation
		/// </summary>
		/// <param name="destHost">The destination host you want to communicate via socks server</param>
		/// <param name="destAddress">The destination address you want to communicate via socks server</param>
		/// <param name="destPort">The destination port of the host</param>
		/// <returns></returns>
		private async Task ConnectAsync(string destHost, IPAddress destAddress, int destPort)
		{
			if (Status != 0)
			{
				throw new InvalidOperationException("This instance already connected.");
			}
			if (_socksServerEndPoint == null)
			{
				await TCP.ConnectAsync(_socksServerHost, _socksServerPort);
			}
			else
			{
				await TCP.ConnectAsync(_socksServerEndPoint.Address, _socksServerEndPoint.Port);
			}
			_stream = TCP.GetStream();
			await HandshakeAndAuthenticationAsync(Credential);
			await SendCommandAsync(Command.Connect, destHost, destAddress, destPort);
			_socksType = Command.Connect;
			Status = SocksStatus.Connected;
		}

		/// <summary>
		/// Send a UDP associate command to SOCKS5 server for UDP relay. The <paramref name="srcAddress" /> and <paramref name="srcPort" /> fields contain the address and port that the client expects to use to send UDP datagrams on for the association. The server MAY use this information to limit access to the association. If the client is not in possesion of the information at the time of UDP Associate (for example, all personal users are NAT, there is no way to determine the public IP and port they will use before sending), the client MUST use a port number and address of all zeros.
		/// </summary>
		/// <param name="srcAddress">The address that the client expects to use to send UDP datagrams on for the association. Alias of DST.ADDR defined in RFC 1928 UDP Associate.</param>
		/// <param name="srcPort">The port that the client expects to use to send UDP datagrams on for the association. Alias of DST.PORT defined in RFC 1928 UDP Associate.</param>
		public void UdpAssociate(IPAddress srcAddress, int srcPort)
		{
			if (Status != 0)
			{
				throw new InvalidOperationException("This instance already associated.");
			}
			if (_socksServerEndPoint != null)
			{
				TCP.Connect(_socksServerEndPoint);
			}
			else
			{
				TCP.Connect(_socksServerHost, _socksServerPort);
			}
			_stream = TCP.GetStream();
			HandshakeAndAuthentication(Credential);
			UDP = new UdpClient(srcPort, TCP.Client.AddressFamily);
			SendCommand(Command.UdpAssociate, null, srcAddress, srcPort);
			if (BoundType == AddressType.Domain)
			{
				UDP.Connect(BoundDomain, BoundPort);
			}
			else
			{
				if (BoundAddress.Equals(IPAddress.Any) || BoundAddress.Equals(IPAddress.IPv6Any))
				{
					BoundAddress = _socksServerEndPoint?.Address ?? IPAddress.Parse(_socksServerHost);
				}
				UDP.Connect(BoundAddress, BoundPort);
			}
			_socksType = Command.UdpAssociate;
			Status = SocksStatus.Connected;
		}

		/// <summary>
		/// Send a UDP associate command to SOCKS5 server for UDP relay. The <paramref name="srcAddress" /> and <paramref name="srcPort" /> fields contain the address and port that the client expects to use to send UDP datagrams on for the association. The server MAY use this information to limit access to the association. If the client is not in possesion of the information at the time of UDP Associate (for example, all personal users are NAT, there is no way to determine the public IP and port they will use before sending), the client MUST use a port number and address of all zeros.
		/// </summary>
		/// <param name="srcAddress">The address that the client expects to use to send UDP datagrams on for the association. Alias of DST.ADDR defined in RFC 1928 UDP Associate.</param>
		/// <param name="srcPort">The port that the client expects to use to send UDP datagrams on for the association. Alias of DST.PORT defined in RFC 1928 UDP Associate.</param>
		public async Task UdpAssociateAsync(IPAddress srcAddress, int srcPort)
		{
			if (Status != 0)
			{
				throw new InvalidOperationException("This instance already associated.");
			}
			if (_socksServerEndPoint == null)
			{
				await TCP.ConnectAsync(_socksServerHost, _socksServerPort);
			}
			else
			{
				await TCP.ConnectAsync(_socksServerEndPoint.Address, _socksServerEndPoint.Port);
			}
			_stream = TCP.GetStream();
			await HandshakeAndAuthenticationAsync(Credential);
			UDP = new UdpClient(srcPort, TCP.Client.AddressFamily);
			await SendCommandAsync(Command.UdpAssociate, null, srcAddress, srcPort);
			if (BoundType == AddressType.Domain)
			{
				UDP.Connect(BoundDomain, BoundPort);
			}
			else
			{
				if (BoundAddress.Equals(IPAddress.Any) || BoundAddress.Equals(IPAddress.IPv6Any))
				{
					BoundAddress = _socksServerEndPoint?.Address ?? IPAddress.Parse(_socksServerHost);
				}
				UDP.Connect(BoundAddress, BoundPort);
			}
			_socksType = Command.UdpAssociate;
			Status = SocksStatus.Connected;
		}

		/// <summary>
		/// Close and release all connections and local UDP ports
		/// </summary>
		public void Close()
		{
			if (!_disposed)
			{
				_disposed = true;
				_stream?.Close();
				TCP?.Close();
				UDP?.Close();
				Status = SocksStatus.Closed;
			}
		}

		public NetworkStream GetStream()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			if (Status != SocksStatus.Connected)
			{
				throw new InvalidOperationException("Socklient not yet connected.");
			}
			return TCP.GetStream();
		}

		/// <summary>
		/// Sending string data to destination via SOCKS server.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="destHost"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public int Send(string str, string destHost, int destPort)
		{
			return Send(Encoding.UTF8.GetBytes(str), destHost, destPort);
		}

		/// <summary>
		/// Sending string data to destination via SOCKS server.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="destAddress"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public int Send(string str, IPAddress destAddress, int destPort)
		{
			return Send(Encoding.UTF8.GetBytes(str), destAddress, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagram"></param>
		/// <param name="destHost"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public int Send(byte[] datagram, string destHost, int destPort)
		{
			return Send(datagram, 0, datagram.Length, destHost, null, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagram"></param>
		/// <param name="destAddress"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public int Send(byte[] datagram, IPAddress destAddress, int destPort)
		{
			return Send(datagram, 0, datagram.Length, null, destAddress, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagramBuffer"></param>
		/// <param name="offset"></param>
		/// <param name="bytes"></param>
		/// <param name="destHost"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public int Send(byte[] datagramBuffer, int offset, int bytes, string destHost, int destPort)
		{
			return Send(datagramBuffer, offset, bytes, destHost, null, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagramBuffer"></param>
		/// <param name="offset"></param>
		/// <param name="bytes"></param>
		/// <param name="destAddress"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public int Send(byte[] datagramBuffer, int offset, int bytes, IPAddress destAddress, int destPort)
		{
			return Send(datagramBuffer, offset, bytes, null, destAddress, destPort);
		}

		private int Send(byte[] datagramBuffer, int offset, int bytes, string destHost, IPAddress destAddress, int destPort)
		{
			CheckSocksStatus(Command.UdpAssociate);
			byte[] array = PackUdp(destHost, destAddress, destPort, datagramBuffer, offset, bytes);
			int num = array.Length - bytes;
			return UDP.Send(array, array.Length) - num;
		}

		/// <summary>
		/// Receiving datagram for UDP relay
		/// </summary>
		/// <returns></returns>
		public byte[] Receive()
		{
			string remoteHost;
			IPAddress remoteAddress;
			int remotePort;
			return Receive(out remoteHost, out remoteAddress, out remotePort);
		}

		/// <summary>
		/// Receiving datagram with remote host info for UDP relay
		/// </summary>
		/// <param name="remoteHost">The host what you relay via SOCKS5 server</param>
		/// <param name="remoteAddress">The address of host what you relay via SOCKS5 server</param>
		/// <param name="remotePort">The service port of host</param>
		/// <returns></returns>
		public byte[] Receive(out string remoteHost, out IPAddress remoteAddress, out int remotePort)
		{
			CheckSocksStatus(Command.UdpAssociate);
			return UnpackUdp(UDP.Receive(ref _remoteEndPoint), out remoteHost, out remoteAddress, out remotePort);
		}

		/// <summary>
		/// Sending string data to destination via SOCKS server.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="destHost"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public Task<int> SendAsync(string str, string destHost, int destPort)
		{
			return SendAsync(Encoding.UTF8.GetBytes(str), destHost, destPort);
		}

		/// <summary>
		/// Sending string data to destination via SOCKS server.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="destAddress"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public Task<int> SendAsync(string str, IPAddress destAddress, int destPort)
		{
			return SendAsync(Encoding.UTF8.GetBytes(str), destAddress, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagram"></param>
		/// <param name="destHost"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public Task<int> SendAsync(byte[] datagram, string destHost, int destPort)
		{
			return SendAsync(datagram, 0, datagram.Length, destHost, null, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagram"></param>
		/// <param name="destAddress"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public Task<int> SendAsync(byte[] datagram, IPAddress destAddress, int destPort)
		{
			return SendAsync(datagram, 0, datagram.Length, null, destAddress, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagramBuffer"></param>
		/// <param name="offset"></param>
		/// <param name="bytes"></param>
		/// <param name="destHost"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public Task<int> SendAsync(byte[] datagramBuffer, int offset, int bytes, string destHost, int destPort)
		{
			return SendAsync(datagramBuffer, offset, bytes, destHost, null, destPort);
		}

		/// <summary>
		/// Sending user datagram to destination via SOCKS server.
		/// </summary>
		/// <param name="datagramBuffer"></param>
		/// <param name="offset"></param>
		/// <param name="bytes"></param>
		/// <param name="destAddress"></param>
		/// <param name="destPort"></param>
		/// <returns></returns>
		public Task<int> SendAsync(byte[] datagramBuffer, int offset, int bytes, IPAddress destAddress, int destPort)
		{
			return SendAsync(datagramBuffer, offset, bytes, null, destAddress, destPort);
		}

		private async Task<int> SendAsync(byte[] datagramBuffer, int offset, int bytes, string destHost, IPAddress destAddress, int destPort)
		{
			CheckSocksStatus(Command.UdpAssociate);
			byte[] array = PackUdp(destHost, destAddress, destPort, datagramBuffer, offset, bytes);
			int headerLength = array.Length - bytes;
			return await UDP.SendAsync(array, array.Length) - headerLength;
		}

		/// <summary>
		/// Receiving datagram with remote host info for UDP relay as an asynchronous operation.
		/// </summary>
		/// <returns></returns>
		public async Task<UdpReceivePacket> ReceiveAsync()
		{
			CheckSocksStatus(Command.UdpAssociate);
			string remoteHost;
			IPAddress remoteAddress;
			int remotePort;
			return new UdpReceivePacket(UnpackUdp((await UDP.ReceiveAsync()).Buffer, out remoteHost, out remoteAddress, out remotePort), remoteHost, remoteAddress, remotePort);
		}

		private void HandshakeAndAuthentication(NetworkCredential credential)
		{
			if (Status == SocksStatus.Connected)
			{
				throw new InvalidOperationException("Socklient has been initialized.");
			}
			if (Status == SocksStatus.Closed)
			{
				throw new InvalidOperationException("Socklient closed, renew an instance for reuse.");
			}
			List<Method> list = new List<Method> { Method.NoAuthentication };
			if (credential != null)
			{
				list.Add(Method.UsernamePassword);
			}
			if (Handshake(list.ToArray()) == Method.UsernamePassword)
			{
				Authenticate(credential.UserName, credential.Password);
			}
		}

		private async Task HandshakeAndAuthenticationAsync(NetworkCredential credential)
		{
			if (Status == SocksStatus.Connected)
			{
				throw new InvalidOperationException("Socklient has been initialized.");
			}
			if (Status == SocksStatus.Closed)
			{
				throw new InvalidOperationException("Socklient closed, renew an instance for reuse.");
			}
			List<Method> list = new List<Method> { Method.NoAuthentication };
			if (credential != null)
			{
				list.Add(Method.UsernamePassword);
			}
			if (await HandshakeAsync(list.ToArray()) == Method.UsernamePassword)
			{
				await AuthenticateAsync(credential.UserName, credential.Password);
			}
		}

		private AddressType PackDestinationAddress(string hostName, IPAddress address, out byte[] addressBytes)
		{
			AddressType result;
			if (address != null)
			{
				result = ((address.AddressFamily != AddressFamily.InterNetworkV6) ? AddressType.IPv4 : AddressType.IPv6);
				addressBytes = address.GetAddressBytes();
			}
			else if (IPAddress.TryParse(hostName, out address))
			{
				result = ((address.AddressFamily != AddressFamily.InterNetworkV6) ? AddressType.IPv4 : AddressType.IPv6);
				addressBytes = address.GetAddressBytes();
			}
			else
			{
				result = AddressType.Domain;
				addressBytes = Encoding.UTF8.GetBytes(hostName);
			}
			return result;
		}

		private byte[] PackUdp(string destHost, IPAddress destAddress, int destPort, byte[] payloadBuffer, int offset, int bytes)
		{
			byte[] addressBytes;
			AddressType addressType = PackDestinationAddress(destHost, destAddress, out addressBytes);
			int num = addressBytes.Length + ((addressType == AddressType.Domain) ? 1 : 0);
			byte[] array = new byte[4 + num + 2 + bytes];
			using MemoryStream output = new MemoryStream(array);
			using BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write((ushort)0);
			binaryWriter.Write((byte)0);
			binaryWriter.Write((byte)addressType);
			switch (addressType)
			{
			case AddressType.IPv4:
			case AddressType.IPv6:
				binaryWriter.Write(addressBytes);
				break;
			case AddressType.Domain:
				binaryWriter.Write((byte)addressBytes.Length);
				binaryWriter.Write(addressBytes);
				break;
			default:
				throw new InvalidOperationException($"Unsupported type: {addressType}.");
			}
			binaryWriter.Write(IPAddress.HostToNetworkOrder((short)destPort));
			binaryWriter.Write(payloadBuffer, offset, bytes);
			return array;
		}

		private byte[] UnpackUdp(byte[] buffer, out string remoteHost, out IPAddress remoteAddress, out int remotePort)
		{
			using MemoryStream input = new MemoryStream(buffer);
			using BinaryReader binaryReader = new BinaryReader(input);
			try
			{
				binaryReader.ReadBytes(3);
				AddressType addressType = (AddressType)binaryReader.ReadByte();
				int num = 0;
				if (addressType == AddressType.Domain)
				{
					byte b = binaryReader.ReadByte();
					byte[] array = binaryReader.ReadBytes(b);
					if (array.Length != b)
					{
						throw new ProtocolErrorException($"Server reply a error domain, length: {array.Length}, bytes: {BitConverter.ToString(array)}, domain: {Encoding.UTF8.GetString(array)}");
					}
					remoteHost = Encoding.UTF8.GetString(array);
					remoteAddress = null;
					num = b;
				}
				else
				{
					int num2 = ((addressType == AddressType.IPv4) ? 4 : 16);
					byte[] array2 = binaryReader.ReadBytes(num2);
					if (array2.Length != num2)
					{
						throw new ProtocolErrorException($"Server reply an error address, length: {array2.Length}, bytes: {BitConverter.ToString(array2)}");
					}
					remoteHost = null;
					remoteAddress = new IPAddress(array2);
					num = num2;
				}
				remotePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
				int count = buffer.Length - 4 - num - 2;
				return binaryReader.ReadBytes(count);
			}
			catch (EndOfStreamException)
			{
				throw new ProtocolErrorException("Server respond unknown message: " + BitConverter.ToString(buffer) + ".");
			}
		}

		private Method Handshake(params Method[] selectionMethods)
		{
			byte[] array = PackHandshake(selectionMethods);
			_stream.Write(array, 0, array.Length);
			byte[] array2 = new byte[2];
			int numberOfBytesRead = _stream.Read(array2, 0, array2.Length);
			return UnpackHandshake(array2, numberOfBytesRead, selectionMethods);
		}

		private async Task<Method> HandshakeAsync(params Method[] selectionMethods)
		{
			byte[] array = PackHandshake(selectionMethods);
			await _stream.WriteAsync(array, 0, array.Length);
			byte[] receiveBuffer = new byte[2];
			return UnpackHandshake(receiveBuffer, await _stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length), selectionMethods);
		}

		private byte[] PackHandshake(params Method[] selectionMethods)
		{
			if (selectionMethods.Length > 255)
			{
				throw new InvalidOperationException("Param 'selectionMethods'.Length can not greater than 255.");
			}
			byte[] array = new byte[2 + selectionMethods.Length];
			using MemoryStream output = new MemoryStream(array);
			using BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write((byte)5);
			binaryWriter.Write((byte)selectionMethods.Length);
			binaryWriter.Write(Array.ConvertAll(selectionMethods, (Method m) => (byte)m));
			return array;
		}

		private Method UnpackHandshake(byte[] buffer, int numberOfBytesRead, Method[] selectionMethods)
		{
			if (numberOfBytesRead < 2)
			{
				throw new ProtocolErrorException("Server respond unknown message: " + BitConverter.ToString(buffer, 0, numberOfBytesRead) + ".");
			}
			byte b = buffer[0];
			if (b != 5)
			{
				throw new ProtocolErrorException($"Server version isn't 5: 0x{b:X2}.");
			}
			Method method = (Method)buffer[1];
			if (!Enum.IsDefined(typeof(Method), method))
			{
				throw new ProtocolErrorException($"Server respond a unknown method: 0x{(byte)method:X2}.");
			}
			if (!selectionMethods.Contains(method))
			{
				throw new MethodUnsupportedException($"Server respond a method({method}:0x{(byte)method:X2}) that is not in 'selectionMethods'.", method);
			}
			return method;
		}

		private void Authenticate(string username, string password)
		{
			byte[] array = PackAuthentication(username, password);
			_stream.Write(array, 0, array.Length);
			byte[] array2 = new byte[2];
			int numberOfBytesRead = _stream.Read(array2, 0, array2.Length);
			UnpackAuthentication(array2, numberOfBytesRead);
		}

		private async Task AuthenticateAsync(string username, string password)
		{
			byte[] array = PackAuthentication(username, password);
			await _stream.WriteAsync(array, 0, array.Length);
			byte[] receiveBuffer = new byte[2];
			UnpackAuthentication(receiveBuffer, await _stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length));
		}

		private byte[] PackAuthentication(string username, string password)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(username);
			if (bytes.Length > 255)
			{
				throw new InvalidOperationException("The length of param 'username' that convert to bytes can not greater than 255.");
			}
			byte[] bytes2 = Encoding.UTF8.GetBytes(password);
			if (bytes2.Length > 255)
			{
				throw new InvalidOperationException("The length of param 'password' that convert to bytes can not greater than 255.");
			}
			byte[] array = new byte[2 + bytes.Length + 1 + bytes2.Length];
			using MemoryStream output = new MemoryStream(array);
			using BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write((byte)1);
			binaryWriter.Write((byte)bytes.Length);
			binaryWriter.Write(bytes);
			binaryWriter.Write((byte)bytes2.Length);
			binaryWriter.Write(bytes2);
			return array;
		}

		private void UnpackAuthentication(byte[] buffer, int numberOfBytesRead)
		{
			if (numberOfBytesRead < 2)
			{
				throw new ProtocolErrorException("Server respond unknown message: " + BitConverter.ToString(buffer, 0, numberOfBytesRead) + ".");
			}
			byte b = buffer[1];
			if (b != 0)
			{
				throw new AuthenticationFailureException($"Authentication fail because server respond status code: {b}.", b);
			}
		}

		private void SendCommand(Command cmd, string destHostNameOrAddress, IPAddress destAddress, int destPort)
		{
			byte[] array = PackCommand(cmd, destHostNameOrAddress, destAddress, destPort);
			_stream.Write(array, 0, array.Length);
			byte[] array2 = new byte[512];
			int numberOfBytesRead = _stream.Read(array2, 0, array2.Length);
			UnpackCommand(array2, numberOfBytesRead);
		}

		private async Task SendCommandAsync(Command cmd, string destHostNameOrAddress, IPAddress destAddress, int destPort)
		{
			byte[] array = PackCommand(cmd, destHostNameOrAddress, destAddress, destPort);
			await _stream.WriteAsync(array, 0, array.Length);
			byte[] receiveBuffer = new byte[512];
			UnpackCommand(receiveBuffer, await _stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length));
		}

		private byte[] PackCommand(Command cmd, string destHostNameOrAddress, IPAddress destAddress, int destPort)
		{
			if (cmd == Command.Bind)
			{
				throw new InvalidOperationException("Unsupport 'Bind' command yet.");
			}
			byte[] addressBytes;
			AddressType addressType = PackDestinationAddress(destHostNameOrAddress, destAddress, out addressBytes);
			int num = addressBytes.Length + ((addressType == AddressType.Domain) ? 1 : 0);
			byte[] array = new byte[4 + num + 2];
			using MemoryStream output = new MemoryStream(array);
			using BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write((byte)5);
			binaryWriter.Write((byte)cmd);
			binaryWriter.Write((byte)0);
			binaryWriter.Write((byte)addressType);
			switch (addressType)
			{
			case AddressType.IPv4:
			case AddressType.IPv6:
				binaryWriter.Write(addressBytes);
				break;
			case AddressType.Domain:
				binaryWriter.Write((byte)addressBytes.Length);
				binaryWriter.Write(addressBytes);
				break;
			default:
				throw new InvalidOperationException($"Unsupported type: {addressType}.");
			}
			binaryWriter.Write(IPAddress.HostToNetworkOrder((short)destPort));
			return array;
		}

		private void UnpackCommand(byte[] buffer, int numberOfBytesRead)
		{
			using MemoryStream input = new MemoryStream(buffer, 0, numberOfBytesRead);
			using BinaryReader binaryReader = new BinaryReader(input);
			try
			{
				byte b = binaryReader.ReadByte();
				if (b != 5)
				{
					throw new ProtocolErrorException($"Server version isn't 5: 0x{b:X2}.");
				}
				Reply reply = (Reply)binaryReader.ReadByte();
				if (reply != 0)
				{
					throw new CommandException($"Command failed, server reply: {reply}.", reply);
				}
				binaryReader.ReadByte();
				BoundType = (AddressType)binaryReader.ReadByte();
				switch (BoundType)
				{
				case AddressType.IPv4:
				case AddressType.IPv6:
				{
					int num = ((BoundType == AddressType.IPv4) ? 4 : 16);
					byte[] array2 = binaryReader.ReadBytes(num);
					if (array2.Length != num)
					{
						throw new ProtocolErrorException($"Server reply an error address, length: {array2.Length}, bytes: {BitConverter.ToString(array2)}");
					}
					BoundAddress = new IPAddress(array2);
					break;
				}
				case AddressType.Domain:
				{
					byte b2 = binaryReader.ReadByte();
					byte[] array = binaryReader.ReadBytes(b2);
					if (array.Length != b2)
					{
						throw new ProtocolErrorException($"Server reply a error domain, length: {array.Length}, bytes: {BitConverter.ToString(array)}, domain: {Encoding.UTF8.GetString(array)}");
					}
					BoundDomain = Encoding.UTF8.GetString(array);
					break;
				}
				default:
					throw new ProtocolErrorException($"Server reply an unsupported address type: {BoundType}.");
				}
				BoundPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());
			}
			catch (EndOfStreamException)
			{
				throw new ProtocolErrorException("Server respond unknown message: " + BitConverter.ToString(buffer, 0, numberOfBytesRead) + ".");
			}
		}

		private void CheckSocksStatus(Command allowedType)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			if (_socksType != allowedType)
			{
				throw new InvalidOperationException($"This method only available where socklient under {allowedType} mode");
			}
		}

		private void CheckUdpClient()
		{
			if (UDP == null)
			{
				throw new InvalidOperationException("This property is available after 'Socklient.UdpAssociate' success.");
			}
		}

		public void Dispose()
		{
			Close();
		}
	}
}
