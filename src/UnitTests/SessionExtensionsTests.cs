﻿using System;
using Neo4j.Driver;
using Neo4jMapper;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class SessionExtensionsTests
    {
        public class NonEntity
        {
            public long Id { get; set; }
        }

        public class EntityWithNullableId
        {
            [NodeId]
            public long? Id { get; set; }
        }

        [Test]
        public void SetNodeAsync_Should_Throw_Exception_If_NodeIdAttribute_Is_Not_Found()
        {
            var session = Substitute.For<IAsyncSession>();

            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await session.SetNodeAsync(new NonEntity()));

            Assert.AreEqual("NodeIdAttribute not specified or the Node Id is null", exception.Message);
        }

        [Test]
        public void SetNodeAsync_Should_Throw_Exception_If_Node_Id_Value_Is_Null()
        {
            var session = Substitute.For<IAsyncSession>();

            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await session.SetNodeAsync(new EntityWithNullableId()));

            Assert.AreEqual("NodeIdAttribute not specified or the Node Id is null", exception.Message);
        }

        [Test]
        public void SetNodeAsync_Tx_Should_Throw_Exception_If_NodeIdAttribute_Is_Not_Found()
        {
            var transaction = Substitute.For<IAsyncTransaction>();

            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await transaction.SetNodeAsync(new NonEntity()));

            Assert.AreEqual("NodeIdAttribute not specified or the Node Id is null", exception.Message);
        }

        [Test]
        public void SetNodeAsync_Tx_Should_Throw_Exception_If_Node_Id_Value_Is_Null()
        {
            var transaction = Substitute.For<IAsyncTransaction>();

            var exception = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await transaction.SetNodeAsync(new EntityWithNullableId()));

            Assert.AreEqual("NodeIdAttribute not specified or the Node Id is null", exception.Message);
        }
    }
}
