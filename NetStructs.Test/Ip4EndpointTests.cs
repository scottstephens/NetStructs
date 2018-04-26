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
    }
}
