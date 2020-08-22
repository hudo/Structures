using NUnit.Framework;
using System.Text.Json;

namespace Structures.Time.Tests
{
    public class SerializationTests
    {
        [Test]
        public void Can_serialize()
        {
            Assert.AreEqual("\"5:30:5\"", JsonSerializer.Serialize(new Time(5, 30, 5)));
            Assert.AreEqual("\"5:0:0\"", JsonSerializer.Serialize(new Time(5, 0, 0)));
        }

        [Test]
        public void Can_deserialize()
        {
            Assert.AreEqual(new Time(5, 30, 5), JsonSerializer.Deserialize<Time>("\"5:30:5\""));
            Assert.AreEqual(new Time(5, 20, 0), JsonSerializer.Deserialize<Time>("\"5:20\""));
            Assert.AreEqual(new Time(5, 0, 0), JsonSerializer.Deserialize<Time>("\"5\""));
            Assert.AreEqual(new Time(5, 0, 0), JsonSerializer.Deserialize<Time>("\"5:0000000\""));
        }
    }
}
