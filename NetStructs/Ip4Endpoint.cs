// SPDX-License-Identifier: MIT
using System;
using System.Net;
using System.Runtime.InteropServices;

namespace NetStructs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Ip4Endpoint
    {
        /// <summary>
        /// The endpoint's address
        /// </summary>
        public Ip4Address Address;

        /// <summary>
        /// The endpoint's port
        /// </summary>
        public IpPort Port;

        /// <summary>
        /// Construct endpoint.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public Ip4Endpoint(Ip4Address address, IpPort port)
        {
            this.Address = address;
            this.Port = port;
        }

        /// <summary>
        /// Construct endpoint; port is in host order.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public Ip4Endpoint(Ip4Address address, int port)
        {
            this.Address = address;
            this.Port = IpPort.FromHostOrder((ushort)port);
        }

        /// <summary>
        /// Construct endpoint; adresss is in network order, port is in host order.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public Ip4Endpoint(byte[] address, int port)
            : this(address, (ushort)port)
        { }

        /// <summary>
        /// Construct endpoint; address is in network order, port is in host order.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public Ip4Endpoint(byte[] address, ushort port)
        {
            this.Address = new Ip4Address(address);
            this.Port = IpPort.FromHostOrder(port);
        }

        /// <summary>
        /// Construct endpoint; address and port are both in network order.
        /// </summary>
        /// <param name="address_buffer"></param>
        /// <param name="address_offset"></param>
        /// <param name="port_buffer"></param>
        /// <param name="port_offset"></param>
        public Ip4Endpoint(byte[] address_buffer, int address_offset, byte[] port_buffer, int port_offset)
        {
            this.Address = new Ip4Address(address_buffer, address_offset, 4);
            this.Port = new IpPort();
            this.Port.NetworkOrder = BitConverter.ToUInt16(port_buffer, port_offset);
        }

        /// <summary>
        /// Construct endpoint; address and port are both in network order.
        /// </summary>
        /// <param name="address_buffer"></param>
        /// <param name="address_offset"></param>
        /// <param name="max_addres_length"></param>
        /// <param name="port_buffer"></param>
        /// <param name="port_offset"></param>
        /// <param name="max_port_length"></param>
        public unsafe Ip4Endpoint(byte* address_buffer, int address_offset, int max_addres_length, byte* port_buffer, int port_offset, int max_port_length)
        {
            if (max_addres_length < 4)
                throw new ArgumentException("Address buffer segment must be at least four bytes.");
            if (max_port_length < 2)
                throw new ArgumentException("Port buffer segment must be at least 2 bytes.");

            this.Address = *((Ip4Address*)(address_buffer + address_offset));
            this.Port = new IpPort(port_buffer, port_offset);
        }

        /// <summary>
        /// Construct from the BCL representation.
        /// </summary>
        /// <param name="ep"></param>
        public Ip4Endpoint(IPEndPoint ep)
            : this(ep.Address, ep.Port)
        { }

        /// <summary>
        /// Construct from a BCL IP address and a host-order port number.
        /// </summary>
        /// <param name="bcl_address"></param>
        /// <param name="port"></param>
        public Ip4Endpoint(IPAddress bcl_address, int port)
            : this(bcl_address.GetAddressBytes(), port)
        {
            if (bcl_address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                throw new Exception("ProcessMemoryEndPoint only supports IPv4");
        }

        /// <summary>
        /// Copy the endpoint to a buffer in network order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public void CopyToBuffer(byte[] buffer, int offset, int size)
        {
            if (offset < 0)
                throw new ArgumentException("must be positive", "offset");
            if (size < 6)
                throw new ArgumentException("must be at least 6", "size");
            if (buffer.Length < offset + size)
                throw new ArgumentException($"input segment [{offset},{offset}+{size}) overflows buffer length {buffer.Length}");
            CopyToBufferUnchecked(buffer, offset);
        }

        /// <summary>
        /// Copy the endpoint to a buffer in network order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public void CopyToBufferUnchecked(byte[] buffer, int offset)
        {
            this.Address.CopyToBufferUnchecked(buffer, offset);
            this.Port.CopyToBufferUnchecked(buffer, offset);
        }

        /// <summary>
        /// Convert to the BCL representation.
        /// </summary>
        /// <returns></returns>
        public IPEndPoint ToBclIpEndPoint()
        {
            return new IPEndPoint(this.Address.ToBclIpAddress(), this.Port.HostOrder);
        }

        /// <summary>
        /// Get a copy of the bytes of the endpoint.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            var output = new byte[6];
            this.CopyToBufferUnchecked(output, 0);
            return output;
        }

        public bool Equals(Ip4Endpoint that)
        {
            return this.Port.Equals(that.Port) && this.Address.Equals(that.Address);
        }

        public override bool Equals(object obj)
        {
            return (obj != null && obj is Ip4Endpoint) ? this.Equals((Ip4Endpoint)obj) : false;
        }

        public override int GetHashCode()
        {
            return this.Address.GetHashCode();
        }

        public override string ToString()
        {
            string output = String.Format("{0}:{1}", this.Address, this.Port);
            return output;
        }
    }
}
