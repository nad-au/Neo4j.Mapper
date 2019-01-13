using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;
using Neo4jMapper;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture()]
    public class RecordExtensionsTests
    {
        [Test]
        public void Map_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);

            var result = record.Map<int>();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Map_2_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);

            var result = record.Map((int value1, int value2) => new[]
            {
                value1,
                value2
            });

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
        }

        [Test]
        public void Map_3_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);

            var result = record.Map((int value1, int value2, int value3) => new[]
            {
                value1,
                value2,
                value3
            });

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [Test]
        public void Map_4_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);

            var result = record.Map((int value1, int value2, int value3, int value4) => new[]
            {
                value1,
                value2,
                value3,
                value4
            });

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }

        [Test]
        public void Map_5_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);

            var result = record.Map((int value1, int value2, int value3, int value4, int value5) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5
            });

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
        }

        [Test]
        public void Map_6_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
                    int value6) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6
            });

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
        }

        [Test]
        public void Map_7_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7
            });

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(5, result[4]);
            Assert.AreEqual(6, result[5]);
            Assert.AreEqual(7, result[6]);
        }

        [Test]
        public void Map_8_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_9_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_10_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_11_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_12_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_13_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_14_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_15_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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
        public void Map_16_Should_Return_Result()
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

            var result = record.Map((int value1, int value2, int value3, int value4, int value5,
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
            });

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

        [Test]
        public void IEnumerable_Map_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map<int>().ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First());
        }

        [Test]
        public void IEnumerable_Map_2_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2) => new[]
            {
                value1,
                value2
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
        }

        [Test]
        public void IEnumerable_Map_3_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3) => new[]
            {
                value1,
                value2,
                value3
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
        }

        [Test]
        public void IEnumerable_Map_4_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4) => new[]
            {
                value1,
                value2,
                value3,
                value4
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
        }

        [Test]
        public void IEnumerable_Map_5_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
        }

        [Test]
        public void IEnumerable_Map_6_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
                    int value6) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
        }

        [Test]
        public void IEnumerable_Map_7_Should_Return_Result()
        {
            var record = Substitute.For<IRecord>();
            record[0].Returns(1);
            record[1].Returns(2);
            record[2].Returns(3);
            record[3].Returns(4);
            record[4].Returns(5);
            record[5].Returns(6);
            record[6].Returns(7);

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
                int value6, int value7) => new[]
            {
                value1,
                value2,
                value3,
                value4,
                value5,
                value6,
                value7
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
        }

        [Test]
        public void IEnumerable_Map_8_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
        }

        [Test]
        public void IEnumerable_Map_9_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
        }

        [Test]
        public void IEnumerable_Map_10_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
        }

        [Test]
        public void IEnumerable_Map_11_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
            Assert.AreEqual(11, result.First()[10]);
        }

        [Test]
        public void IEnumerable_Map_12_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
            Assert.AreEqual(11, result.First()[10]);
            Assert.AreEqual(12, result.First()[11]);
        }

        [Test]
        public void IEnumerable_Map_13_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
            Assert.AreEqual(11, result.First()[10]);
            Assert.AreEqual(12, result.First()[11]);
            Assert.AreEqual(13, result.First()[12]);
        }

        [Test]
        public void IEnumerable_Map_14_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
            Assert.AreEqual(11, result.First()[10]);
            Assert.AreEqual(12, result.First()[11]);
            Assert.AreEqual(13, result.First()[12]);
            Assert.AreEqual(14, result.First()[13]);
        }

        [Test]
        public void IEnumerable_Map_15_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
            Assert.AreEqual(11, result.First()[10]);
            Assert.AreEqual(12, result.First()[11]);
            Assert.AreEqual(13, result.First()[12]);
            Assert.AreEqual(14, result.First()[13]);
            Assert.AreEqual(15, result.First()[14]);
        }

        [Test]
        public void IEnumerable_Map_16_Should_Return_Result()
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

            var records = Substitute.For<IStatementResult>();
            records.GetEnumerator().Returns(
                new List<IRecord> {record, record}.GetEnumerator());

            var result = records.Map((int value1, int value2, int value3, int value4, int value5,
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
            }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result.First()[0]);
            Assert.AreEqual(2, result.First()[1]);
            Assert.AreEqual(3, result.First()[2]);
            Assert.AreEqual(4, result.First()[3]);
            Assert.AreEqual(5, result.First()[4]);
            Assert.AreEqual(6, result.First()[5]);
            Assert.AreEqual(7, result.First()[6]);
            Assert.AreEqual(8, result.First()[7]);
            Assert.AreEqual(9, result.First()[8]);
            Assert.AreEqual(10, result.First()[9]);
            Assert.AreEqual(11, result.First()[10]);
            Assert.AreEqual(12, result.First()[11]);
            Assert.AreEqual(13, result.First()[12]);
            Assert.AreEqual(14, result.First()[13]);
            Assert.AreEqual(15, result.First()[14]);
            Assert.AreEqual(16, result.First()[15]);
        }
    }
}
