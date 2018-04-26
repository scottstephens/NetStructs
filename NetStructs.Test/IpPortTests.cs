// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.Text;
using NetStructs;
using NUnit.Framework;

namespace NetStructs.Test
{
    [TestFixture]
    public class IpPortTests
    {
        [Test]
        public void Constructor_Works()
        {
            var port = new IpPort(new byte[] { 1, 2 }, 0);
            if (BitConverter.IsLittleEndian)
                Assert.That(port.NetworkOrder, Is.EqualTo(0x0201));
            else
                Assert.That(port.NetworkOrder, Is.EqualTo(0x0102));

        }

        [Test]
        public void FromHostOrder_Works()
        {
            var port = IpPort.FromHostOrder(0x0201);
            if (BitConverter.IsLittleEndian)
                Assert.That(port.NetworkOrder, Is.EqualTo(0x0102));
            else
                Assert.That(port.NetworkOrder, Is.EqualTo(0x0201));
        }

        [Test]
        public void FromNetworkOrder_Works()
        {
            var port = IpPort.FromNetworkOrder(0x0102);
            if (BitConverter.IsLittleEndian)
                Assert.That(port.NetworkOrder, Is.EqualTo(0x0102));
            else
                Assert.That(port.NetworkOrder, Is.EqualTo(0x0201));
        }

        [Test]
        public void GetHostOrder_Works()
        {
            var hport = (ushort)55555;
            var port = IpPort.FromHostOrder(hport);
            Assert.That(port.HostOrder, Is.EqualTo(hport));
        }

        [Test]
        public void SetHostOrder_Works()
        {
            var hport = (ushort)55555;
            var port = IpPort.FromHostOrder(hport);
            Assert.That(port.HostOrder, Is.EqualTo(hport));

            var hport2 = (ushort)44444;
            port.HostOrder = hport2;

            Assert.That(port.HostOrder, Is.EqualTo(hport2));
        }

        [Test]
        public void CopyToBuffer_Works()
        {
            var port = new IpPort(new byte[] { 1, 2 }, 0);
            var output = new byte[8];
            port.CopyToBuffer(output, 3, 2);
            Assert.That(output[3], Is.EqualTo(1));
            Assert.That(output[4], Is.EqualTo(2));
        }

        [Test]
        public void GetHashCode_Smoke()
        {
            var port = IpPort.FromHostOrder(44444);
            var hc = port.GetHashCode();
            Assert.IsTrue(true);
        }
    }
}
