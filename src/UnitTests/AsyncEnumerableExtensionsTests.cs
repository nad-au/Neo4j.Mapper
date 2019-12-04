using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class AsyncEnumerableExtensionsTests
    {
        public class MockAsyncEnumerable : IAsyncEnumerable<IRecord>
        {
            private readonly IRecord record;

            public MockAsyncEnumerable(IRecord record)
            {
                this.record = record;
            }

            public async IAsyncEnumerator<IRecord> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
            {
                yield return await Task.FromResult(record);
            }
        }

        [Test]
        public async Task Map_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            var result = await mockAsyncEnumerable.Map<int>().SingleAsync();

            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task Map_2_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2) => new[]
            {
                value1,
                value2
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
        }

        [Test]
        public async Task Map_3_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3) => new[]
            {
                value1,
                value2,
                value3
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [Test]
        public async Task Map_4_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4) => new[]
            {
                value1,
                value2,
                value3,
                value4
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }

        [Test]
        public async Task Map_5_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
        }

        [Test]
        public async Task Map_6_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
        }

        [Test]
        public async Task Map_7_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
        }

        [Test]
        public async Task Map_8_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
        }

        [Test]
        public async Task Map_9_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
        }

        [Test]
        public async Task Map_10_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
        }

        [Test]
        public async Task Map_11_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);
            record[10].Returns(11);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10, int value11) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10,
                value11
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
            Assert.AreEqual(11, result[10]);
        }

        [Test]
        public async Task Map_12_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);
            record[10].Returns(11);
            record[11].Returns(12);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10, int value11,
                int value12) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10,
                value11,
                value12
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
            Assert.AreEqual(11, result[10]);
            Assert.AreEqual(12, result[11]);
        }

        [Test]
        public async Task Map_13_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);
            record[10].Returns(11);
            record[11].Returns(12);
            record[12].Returns(13);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10, int value11,
                int value12, int value13) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10,
                value11,
                value12,
                value13
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
            Assert.AreEqual(11, result[10]);
            Assert.AreEqual(12, result[11]);
            Assert.AreEqual(13, result[12]);
        }

        [Test]
        public async Task Map_14_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);
            record[10].Returns(11);
            record[11].Returns(12);
            record[12].Returns(13);
            record[13].Returns(14);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10, int value11,
                int value12, int value13, int value14) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10,
                value11,
                value12,
                value13,
                value14
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
            Assert.AreEqual(11, result[10]);
            Assert.AreEqual(12, result[11]);
            Assert.AreEqual(13, result[12]);
            Assert.AreEqual(14, result[13]);
        }

        [Test]
        public async Task Map_15_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);
            record[10].Returns(11);
            record[11].Returns(12);
            record[12].Returns(13);
            record[13].Returns(14);
            record[14].Returns(15);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10, int value11,
                int value12, int value13, int value14, int value15) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10,
                value11,
                value12,
                value13,
                value14,
                value15
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
            Assert.AreEqual(11, result[10]);
            Assert.AreEqual(12, result[11]);
            Assert.AreEqual(13, result[12]);
            Assert.AreEqual(14, result[13]);
            Assert.AreEqual(15, result[14]);
        }

        [Test]
        public async Task Map_16_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);
            record[7].Returns(8);
            record[8].Returns(9);
            record[9].Returns(10);
            record[10].Returns(11);
            record[11].Returns(12);
            record[12].Returns(13);
            record[13].Returns(14);
            record[14].Returns(15);
            record[15].Returns(16);

            var mockAsyncEnumerable = new MockAsyncEnumerable(record);
            
            var result = await mockAsyncEnumerable.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7, int value8, int value9, int value10, int value11,
                int value12, int value13, int value14, int value15, int value16) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7,
                value8,
                value9,
                value10,
                value11,
                value12,
                value13,
                value14,
                value15,
                value16
            }).SingleAsync();

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
            Assert.AreEqual(8, result[7]);
            Assert.AreEqual(9, result[8]);
            Assert.AreEqual(10, result[9]);
            Assert.AreEqual(11, result[10]);
            Assert.AreEqual(12, result[11]);
            Assert.AreEqual(13, result[12]);
            Assert.AreEqual(14, result[13]);
            Assert.AreEqual(15, result[14]);
            Assert.AreEqual(16, result[15]);
        }
    }
}
