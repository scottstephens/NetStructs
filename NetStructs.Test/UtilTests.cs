// SPDX-License-Identifier: MIT
using System;
using System.Collections.Generic;
using System.Text;
using NetStructs;
using NUnit.Framework;

namespace NetStructs.Test
{
    [TestFixture]
    public class UtilTests
    {
        [Test]
        public void ByteSwap_Works()
        {
            ushort x = 0xabcd;
            var y = Util.ByteSwap(x);
            Assert.That(y, Is.EqualTo(0xcdab));
        }
    }
}
