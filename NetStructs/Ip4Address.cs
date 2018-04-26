// SPDX-License-Identifier: MIT
using System;
using System.Net;
using System.Runtime.InteropServices;

namespace NetStructs
{
    /// <summary>
    /// An IPv4 address
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Ip4Address
    {
        #region constants
        /// <summary>
        /// The Any address: 0.0.0.0
        /// </summary>
        public static readonly Ip4Address Any = new Ip4Address(0, 0, 0, 0);

        /// <summary>
        /// The broadcast address: 255.255.255.255
        /// </summary>
        public static readonly Ip4Address Broadcast = new Ip4Address(255, 255, 255, 255);
        #endregion

        /// <summary>
        /// Network byte-order representation of the address
        /// </summary>
        public UInt32 NetworkOrder;

        #region constructors 

        /// <summary>
        /// Create an IP address from a buffer in network byte-order.
        /// </summary>
        /// <param name="buffer"></param>
        public Ip4Address(byte[] buffer)
        {
            if (buffer.Length != 4)
                throw new ArgumentException("must have 4 bytes", "address");

            unsafe
            {
                fixed (byte* bptr = buffer)
                {
                    this.NetworkOrder = *(uint*)bptr;
                }
            }
        }

        /// <summary>
        /// Create an IP address from a buffer in network byte-order.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public Ip4Address(byte[] buffer, int offset, int size)
        {
            if (offset < 0)
                throw new ArgumentException("must be positive", "offset");
            if (size < 4)
                throw new ArgumentException("must have 4 bytes", "address");
            if (buffer.Length < offset + size)
                throw new ArgumentException("Segment indicated overflows buffer.");

            unsafe
            {
                fixed (byte* bptr = buffer)
                {
                    this.NetworkOrder = *(uint*)(bptr + offset);
                }
            }
        }

        /// <summary>
        /// Create an IP address from constiuent bytes in network byte-order.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        public Ip4Address(byte b0, byte b1, byte b2, byte b3)
        {
            this.NetworkOrder = 0;
            this.NetworkOrder |= b0;
            this.NetworkOrder |= (uint)b1 << 8;
            this.NetworkOrder |= (uint)b2 << 16;
            this.NetworkOrder |= (uint)b3 << 24;
        }

        /// <summary>
        /// Create an IP address from the BCL representation.
        /// </summary>
        /// <param name="address"></param>
        public Ip4Address(IPAddress address)
            : this(address.GetAddressBytes())
        { }

        /// <summary>
        /// Parse an IP address from the conventional representation, ie "198.51.100.4"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Ip4Address Parse(string s)
        {
            var parts = s.Split('.');
            return new Ip4Address(Byte.Parse(parts[0]), Byte.Parse(parts[1]), Byte.Parse(parts[2]), Byte.Parse(parts[3]));
        }

        #endregion

        #region accessors and converters

        /// <summary>
        /// The first byte of the address (in network order)
        /// </summary>
        public byte B0
        {
            get
            {
                return (byte)((this.NetworkOrder & 0x000000ff));
            }
            set
            {
                this.NetworkOrder &= 0xffffff00;
                this.NetworkOrder |= value;
            }
        }

        /// <summary>
        /// The second byte of the address (in network order)
        /// </summary>
        public byte B1
        {
            get
            {
                return (byte)((this.NetworkOrder & 0x0000ff00) >> 8);
            }
            set
            {
                this.NetworkOrder &= 0xffff00ff;
                this.NetworkOrder |= (uint)value << 8;
            }
        }

        /// <summary>
        /// The third byte of the address (in network order)
        /// </summary>
        public byte B2
        {
            get
            {
                return (byte)((this.NetworkOrder & 0x00ff0000) >> 16);
            }
            set
            {
                this.NetworkOrder &= 0xff00ffff;
                this.NetworkOrder |= (uint)value << 16;
            }
        }

        /// <summary>
        /// The fourth byte of the address (in network order)
        /// </summary>
        public byte B3
        {
            get
            {
                return (byte)((this.NetworkOrder & 0xff000000) >> 24);
            }
            set
            {
                this.NetworkOrder &= 0x00ffffff;
                this.NetworkOrder |= (uint)value << 24;
            }
        }

        /// <summary>
        /// Access a single byte of the address (in network order)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index]
        {
            get
            {
                if (index < 0 || index > 3)
                    throw new IndexOutOfRangeException($"{index}");
                int shift = 8 * (3 - index);
                var mask = (uint)(0xff000000 >> shift);
                var isolated = this.NetworkOrder & mask;
                var value = isolated >> 8*index;
                return (byte)value;
            }
            set
            {
                if (index < 0 || index > 3)
                    throw new IndexOutOfRangeException($"{index}");
                int dest_shift = 8 * (3 - index);
                var dest_mask = (uint)(0xff000000 >> dest_shift);
                var dest_val = (uint)(value << 8 * index);
                this.NetworkOrder &= ~dest_mask;
                this.NetworkOrder |= dest_val;
            }
        }

        /// <summary>
        /// Get the address in host order.
        /// </summary>
        public UInt32 HostOrder
        {
            get
            {
                return (UInt32)IPAddress.NetworkToHostOrder((int)this.NetworkOrder);
            }
        }

        /// <summary>
        /// Copy an address to a buffer in network byte-order
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        public void CopyToBuffer(byte[] buffer, int offset, int size)
        {
            if (offset < 0)
                throw new ArgumentException("must be positive", "offset");
            if (size < 4)
                throw new ArgumentException("must be at least 4", "offset");
            if (buffer.Length < offset + size)
                throw new ArgumentException($"Given segment [{offset},{offset}+{size}) overflows buffer length {buffer.Length}");
            CopyToBufferUnchecked(buffer, offset);
        }

        /// <summary>
        /// Copy an address to a buffer in network byte-order
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public void CopyToBufferUnchecked(byte[] buffer, int offset)
        {
            unsafe
            {
                fixed (byte* bptr = buffer)
                {
                    *(uint*)(bptr + offset) = this.NetworkOrder;
                }
            }
        }

        /// <summary>
        /// Get a copy of the bytes of the address in network byte-order
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            return new byte[] { this.B0, this.B1, this.B2, this.B3 };
        }

        /// <summary>
        /// Convert to the BCL representation.
        /// </summary>
        /// <returns></returns>
        public IPAddress ToBclIpAddress()
        {
            return new IPAddress(this.GetBytes());
        }

        #endregion

        /// <summary>
        /// Shows whether the address is a machine-local address
        /// </summary>
        public bool IsLocal
        {
            get
            {
                return this.B0 == 127;
            }
        }

        private const byte MulticastMask = 240;

        /// <summary>
        /// Shows whether the address is a multicast address.
        /// </summary>
        public bool IsMulticast
        {
            get
            {
                return (this.B0 & MulticastMask) == 224 || (this.IsLocal && (this.B1 & MulticastMask) == 224);
            }
        }

        /// <summary>
        /// Shows whether the address is the null address 0.0.0.0;
        /// </summary>
        public bool IsNull
        {
            get
            {
                return this.NetworkOrder == 0;
            }
        }

        #region customary overloads
        public bool Equals(Ip4Address that)
        {
            return this.NetworkOrder == that.NetworkOrder;
        }

        public override bool Equals(object obj)
        {
            return (obj != null && obj is Ip4Address) ? this.Equals((Ip4Address)obj) : false;
        }

        public override int GetHashCode()
        {
            return (int)this.NetworkOrder;
        }

        public override string ToString()
        {
            string output = String.Format("{0}.{1}.{2}.{3}", this.B0, this.B1, this.B2, this.B3);
            return output;
        }

        public static bool operator ==(Ip4Address a, Ip4Address b)
        {
            return a.NetworkOrder == b.NetworkOrder;
        }

        public static bool operator !=(Ip4Address a, Ip4Address b)
        {
            return a.NetworkOrder != b.NetworkOrder;
        }

        public static Ip4Address operator &(Ip4Address a, Ip4Address b)
        {
            var output = new Ip4Address();
            output.NetworkOrder = a.NetworkOrder & b.NetworkOrder;
            return output;
        }

        public static Ip4Address operator |(Ip4Address a, Ip4Address b)
        {
            var output = new Ip4Address();
            output.NetworkOrder = a.NetworkOrder | b.NetworkOrder;
            return output;
        }

        public static Ip4Address operator ~(Ip4Address a)
        {
            var output = new Ip4Address();
            output.NetworkOrder = ~a.NetworkOrder;
            return output;
        }

        public static Ip4Address operator ^(Ip4Address a, Ip4Address b)
        {
            var output = new Ip4Address();
            output.NetworkOrder = a.NetworkOrder ^ b.NetworkOrder;
            return output;
        }
        #endregion
    }
}
