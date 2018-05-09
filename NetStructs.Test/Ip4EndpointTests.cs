// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.Text;
using NetStructs;
using NUnit.Framework;

namespace NetStructs.Test
{
    [TestFixture]
    public class Ip4EndpointTests
    {
        [Test]
        public void GetHashCode_Smoke()
        {
            var ep = new Ip4Endpoint(new Ip4Address(1, 2, 3, 4), 54545);
            var hc = ep.GetHashCode();
            Assert.IsTrue(true);
        }

        [Test]
        public void Parse_Full_Works()
        {
            var input = "10.1.2.3:54321";
            var ep = Ip4Endpoint.Parse(input);
            var expected = new Ip4Endpoint(new Ip4Address(10, 1, 2, 3), 54321);
            Assert.That(ep, Is.EqualTo(expected));
        }

        [Test]
        public void Parse_AddressOnly_Works()
        {
            var input = "10.1.2.3";
            var ep = Ip4Endpoint.Parse(input);
            var expected = new Ip4Endpoint(new Ip4Address(10, 1, 2, 3), 0);
            Assert.That(ep, Is.EqualTo(expected));
        }

        [Test]
        public void Parse_PortOnly_Works()
        {
            var input = ":54321";
            var ep = Ip4Endpoint.Parse(input);
            var expected = new Ip4Endpoint(Ip4Address.Any, 54321);
            Assert.That(ep, Is.EqualTo(expected));
        }
    }
}
