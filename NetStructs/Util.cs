// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.Text;

namespace NetStructs
{
    public class Util
    {
        private const ushort byte0 = 0x00ff;
        private const ushort byte1 = 0xff00;

        /// <summary>
        /// Swap bytes of input, and thereby convert between 
        /// big-endian and little-endian representations.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ushort ByteSwap(ushort x)
        {
            return (ushort)(((x & byte0) << (ushort)8) | ((x & byte1) >> 8));
        }

        /// <summary>
        /// Convert input from network byte order to host byte order.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ushort NetworkToHostOrder(ushort x)
        {
            if (BitConverter.IsLittleEndian)
                return ByteSwap(x);
            else
                return x;
        }

        /// <summary>
        /// Convert input from host byte order to network byte order.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static ushort HostToNetworkOrder(ushort x)
        {
            if (BitConverter.IsLittleEndian)
                return ByteSwap(x);
            else
                return x;
        }
    }
}
