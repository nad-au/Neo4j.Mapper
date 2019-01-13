using Neo4jMapper;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class EntityAccessorTests
    {
        public class Entity
        {
            [NodeId]
            public long Id { get; set; }
        }

        public class EntityWithNullableId
        {
            [NodeId]
            public long? Id { get; set; }
        }

        public class NonEntity
        {
            public long Id { get; set; }
        }

        [Test]
        public void Should_Return_Entity_Node_Id()
        {
            var entity = new Entity {Id = 1};
            var id = EntityAccessor.GetNodeId(entity);

            Assert.AreEqual(1, id);
        }

        [Test]
        public void Should_Return_Entity_Node_Id_For_Nullable_Id()
        {
            var entity = new EntityWithNullableId {Id = 1};
            var id = EntityAccessor.GetNodeId(entity);

            Assert.AreEqual(1, id);
        }

        [Test]
        public void Should_Return_Null_If_Entity_Does_Not_Use_NodeIdAttribute()
        {
            var entity = new NonEntity {Id = 1};
            var id = EntityAccessor.GetNodeId(entity);

            Assert.IsNull(id);
        }

        [Test]
        public void Should_Set_Entity_Node_Id()
        {
            var entity = new Entity();
            EntityAccessor.SetNodeId(entity, 1);

            Assert.AreEqual(1, entity.Id);
        }

        [Test]
        public void Should_Not_Set_Properties_If_Entity_Does_Not_Use_NodeIdAttribute()
        {
            var entity = new NonEntity();
            EntityAccessor.SetNodeId(entity, 1);

            Assert.AreEqual(0, entity.Id);
        }
    }
}
