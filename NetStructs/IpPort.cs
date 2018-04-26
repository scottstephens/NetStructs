// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace NetStructs
{
    [StructLayout(LayoutKind.Explicit, Pack=1,Size =2)]
    public struct IpPort
    {
        [FieldOffset(0)] public ushort NetworkOrder;

        #region constructors
        /// <summary>
        /// Construct from a buffer in network byte-order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public IpPort(byte[] buffer, int offset)
        {
            unsafe
            {
                fixed (byte* bptr = buffer)
                {
                    this.NetworkOrder = *((ushort*)(bptr + offset));
                }
            }
        }

        /// <summary>
        /// Construct from a buffer in network byte-order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public unsafe IpPort(byte* buffer, int offset)
        {
            this.NetworkOrder = *((ushort*)(buffer + offset));
        }

        /// <summary>
        /// Construct from the host-order port number.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IpPort FromHostOrder(ushort port)
        {
            var output = new IpPort();
            output.HostOrder = port;
            return output;
        }

        /// <summary>
        /// Construct from the host-order port number.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IpPort FromHostOrder(int port)
        {
            if (port < 0 || port > ushort.MaxValue)
                throw new ArgumentException($"port must be positive and less than {ushort.MaxValue}");

            return IpPort.FromHostOrder((ushort)port);
        }

        /// <summary>
        /// Construct from a network-order port number.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IpPort FromNetworkOrder(ushort port)
        {
            var output = new IpPort();
            output.NetworkOrder = port;
            return output;
        }
        #endregion

        #region accessors and conversions

        /// <summary>
        /// Get the port number in host order
        /// </summary>
        public ushort HostOrder
        {
            get
            {
                return Util.NetworkToHostOrder(this.NetworkOrder);
            }
            set
            {
                this.NetworkOrder = Util.HostToNetworkOrder(value);
            }
        }

        /// <summary>
        /// Copy to a buffer in network order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public unsafe void CopyToBufferUnchecked(byte[] buffer, int offset)
        {
            fixed (byte* bptr = buffer)
            {
                *((ushort*)(bptr + offset)) = this.NetworkOrder;
            }
        }

        /// <summary>
        /// Copy to a buffer in network order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void CopyToBuffer(byte[] buffer, int offset, int length)
        {
            if (offset < 0)
                throw new ArgumentException($"Argument offset must be positive.");
            if (length < 2)
                throw new ArgumentException($"Argument 'length' must be at least 2.");
            if (buffer.Length < offset + length)
                throw new ArgumentException($"Specified segment [{offset},{offset}+{length}) overflows buffer.");
            this.CopyToBufferUnchecked(buffer, offset);
        }

        #endregion

        #region customary methods and operators

        public override string ToString()
        {
            return this.HostOrder.ToString();
        }

        public override int GetHashCode()
        {
            return this.NetworkOrder;
        }

        public override bool Equals(object obj)
        {
            if (obj is IpPort)
                return this.Equals((IpPort)obj);
            else
                return false;
        }

        public bool Equals(IpPort that)
        {
            return this.NetworkOrder == that.NetworkOrder;
        }

        public static bool operator==(IpPort x, IpPort y)
        {
            return x.NetworkOrder == y.NetworkOrder;
        }

        public static bool operator !=(IpPort x, IpPort y)
        {
            return x.NetworkOrder != y.NetworkOrder;
        }

        public static IpPort operator +(IpPort x, int y)
        {
            return IpPort.FromHostOrder(x.HostOrder + y);
        }

        public static IpPort operator -(IpPort x, int y)
        {
            return IpPort.FromHostOrder(x.HostOrder - y);
        }

        #endregion
    }
}
