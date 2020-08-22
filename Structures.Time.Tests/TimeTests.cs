using NUnit.Framework;

namespace Structures.Time.Tests
{
    public class TimeTests
    {
        [Test]
        public void Implicitly_create_from_string()
        {
            Time t1 = "5";
            Time t2 = "5:25";
            Time t3 = "5:35:15";

            Assert.AreEqual(new Time(5, 0, 0), t1);
            Assert.AreEqual(new Time(5, 25, 0), t2);
            Assert.AreEqual(new Time(5, 35, 15), t3);
        }

        [Test]
        public void Implicitly_create_from_number()
        {
            Time t1 = 5;
            Time t2 = 5.5;
            Time t3 = 5.33;

            Assert.AreEqual(new Time(5, 0, 0), t1);
            Assert.AreEqual(new Time(5, 30, 0), t2);
            Assert.AreEqual(new Time(5, 19, 48), t3);
        }

        [Test]
        public void Deconstruct_type()
        {
            var (h, m, s) = new Time(5, 10, 15);

            Assert.AreEqual(5, h);
            Assert.AreEqual(10, m);
            Assert.AreEqual(15, s);
        }

        [Test]
        public void Total_times()
        {
            var time = new Time(2, 30, 30);

            Assert.AreEqual(9030, time.TotalSeconds);
            Assert.AreEqual(150.5, time.TotalMinutes);
            Assert.AreEqual(2.5083333333333333, time.TotalHours);
        }


        [Test]
        public void Compare_two_times()
        {
            Assert.True(new Time(2, 0) > new Time(1, 0));
            Assert.True(new Time(2, 40) > new Time(2, 30));
            Assert.True(new Time(2, 40, 30) > new Time(2, 40, 20));

            Assert.False(new Time(2, 0) < new Time(1, 0));
            Assert.True(new Time(2, 20) < new Time(2, 30));
            Assert.True(new Time(2, 30, 20) < new Time(2, 30, 30));

            Assert.IsTrue(new Time(2, 30) == new Time(2, 30));
            Assert.IsTrue(new Time(2, 30) != new Time(1, 30));
            Assert.IsTrue(new Time(1, 30, 30) != new Time(1, 30, 31)); 

            Assert.IsTrue(new Time(2, 30) >= new Time(2, 30));
            Assert.IsTrue(new Time(1, 0) <= new Time(1, 30));

            Time first = 5.5;
            Time second = 3.3;

            Assert.AreEqual(-1, second.CompareTo(first));
            Assert.AreEqual(1, first.CompareTo(second));
            Assert.IsFalse(first.Equals(second));
        }
    }
}