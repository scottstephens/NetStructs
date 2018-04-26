// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace NetStructs.Test
{
    [TestFixture]
    public class Ip4AddressTests
    {
        [Test]
        public void Constructor_Array_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            var host_order_man = (1u << 24) + (2u << 16) + (3u << 8) + 4u;

            var host_order_bcl = IPAddress.NetworkToHostOrder((int)ip.NetworkOrder);
            Assert.That(host_order_man, Is.EqualTo(host_order_bcl));
        }

        [Test]
        public void Constructor_Bytes_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4 );
            var host_order_man = (1u << 24) + (2u << 16) + (3u << 8) + 4u;

            var host_order_bcl = IPAddress.NetworkToHostOrder((int)ip.NetworkOrder);
            Assert.That(host_order_man, Is.EqualTo(host_order_bcl));
        }

        [Test]
        public void GetB0_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B0, Is.EqualTo(1));
        }

        [Test]
        public void GetB1_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B1, Is.EqualTo(2));
        }

        [Test]
        public void GetB2_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B2, Is.EqualTo(3));
        }

        [Test]
        public void GetB3_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B3, Is.EqualTo(4));
        }

        [Test]
        public void SetB0_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B0, Is.EqualTo(1));
            ip.B0 = 9;
            Assert.That(ip.B0, Is.EqualTo(9));
        }

        [Test]
        public void SetB1_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B1, Is.EqualTo(2));
            ip.B1 = 9;
            Assert.That(ip.B1, Is.EqualTo(9));
        }

        [Test]
        public void SetB2_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B2, Is.EqualTo(3));
            ip.B2 = 9;
            Assert.That(ip.B2, Is.EqualTo(9));
        }

        [Test]
        public void SetB3_Works()
        {
            var ip = new Ip4Address(new byte[] { 1, 2, 3, 4 });
            Assert.That(ip.B3, Is.EqualTo(4));
            ip.B3 = 9;
            Assert.That(ip.B3, Is.EqualTo(9));
        }

        [Test]
        public void GetBytes_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            var bytes = ip.GetBytes();
            Assert.That(bytes[0], Is.EqualTo(1));
            Assert.That(bytes[1], Is.EqualTo(2));
            Assert.That(bytes[2], Is.EqualTo(3));
            Assert.That(bytes[3], Is.EqualTo(4));
        }

        [Test]
        public void CopyToBufferUnchecked_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            var output = new byte[8];
            ip.CopyToBufferUnchecked(output, 3);
            Assert.That(output[3], Is.EqualTo(1));
            Assert.That(output[4], Is.EqualTo(2));
            Assert.That(output[5], Is.EqualTo(3));
            Assert.That(output[6], Is.EqualTo(4));
        }

        [Test]
        public void GetHostOrder_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            var host_order_man = (1u << 24) + (2u << 16) + (3u << 8) + 4u;
            Assert.That(ip.HostOrder, Is.EqualTo(host_order_man));
        }

        [Test]
        public void GetIndex_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            Assert.That(ip[0], Is.EqualTo(1));
            Assert.That(ip[1], Is.EqualTo(2));
            Assert.That(ip[2], Is.EqualTo(3));
            Assert.That(ip[3], Is.EqualTo(4));
        }

        [Test]
        public void SetIndex_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            Assert.That(ip[0], Is.EqualTo(1));
            Assert.That(ip[1], Is.EqualTo(2));
            Assert.That(ip[2], Is.EqualTo(3));
            Assert.That(ip[3], Is.EqualTo(4));

            ip[0] = 5;
            Assert.That(ip[0], Is.EqualTo(5));
            Assert.That(ip[1], Is.EqualTo(2));
            Assert.That(ip[2], Is.EqualTo(3));
            Assert.That(ip[3], Is.EqualTo(4));

            ip[1] = 6;
            Assert.That(ip[0], Is.EqualTo(5));
            Assert.That(ip[1], Is.EqualTo(6));
            Assert.That(ip[2], Is.EqualTo(3));
            Assert.That(ip[3], Is.EqualTo(4));

            ip[2] = 7;
            Assert.That(ip[0], Is.EqualTo(5));
            Assert.That(ip[1], Is.EqualTo(6));
            Assert.That(ip[2], Is.EqualTo(7));
            Assert.That(ip[3], Is.EqualTo(4));

            ip[3] = 8;
            Assert.That(ip[0], Is.EqualTo(5));
            Assert.That(ip[1], Is.EqualTo(6));
            Assert.That(ip[2], Is.EqualTo(7));
            Assert.That(ip[3], Is.EqualTo(8));
        }

        [Test]
        public void ToString_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            Assert.That(ip.ToString(), Is.EqualTo("1.2.3.4"));
        }

        [Test]
        public void CopyToBuffer_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            var output = new byte[7];
            ip.CopyToBuffer(output, 3, 4);
            Assert.That(output[3], Is.EqualTo(1));
            Assert.That(output[4], Is.EqualTo(2));
            Assert.That(output[5], Is.EqualTo(3));
            Assert.That(output[6], Is.EqualTo(4));
        }

        [Test]
        public void ToBclIpAddress_Works()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            var bcl_ip = ip.ToBclIpAddress();
            Assert.That(bcl_ip.ToString(), Is.EqualTo("1.2.3.4"));
        }

        [Test]
        public void GetHashCode_Smoke()
        {
            var ip = new Ip4Address(1, 2, 3, 4);
            var hc = ip.GetHashCode();
            Assert.IsTrue(true);
        }
    }
}
