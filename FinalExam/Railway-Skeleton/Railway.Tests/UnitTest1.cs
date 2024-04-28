namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Tests
    {
       
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            RailwayStation railwayStation = new RailwayStation("Pleven");

            Assert.IsNotNull(railwayStation);
            Assert.AreEqual("Pleven", railwayStation.Name);
            Assert.IsNotNull(railwayStation.DepartureTrains);
            Assert.IsNotNull(railwayStation.ArrivalTrains);
        }
        [Test]
        public void Test2()
        {
          
            Assert.Throws<ArgumentException>(() => new RailwayStation(null));
            Assert.Throws<ArgumentException>(() => new RailwayStation("  "));
        }
        [Test]
        public void Test3()
        {
            RailwayStation railwayStation = new RailwayStation("Pleven");
            railwayStation.NewArrivalOnBoard("sofia");

            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count);
        }
        [Test]
        public void Test4()
        {
            RailwayStation railwayStation = new RailwayStation("pleven");
            railwayStation.NewArrivalOnBoard("sof");
            railwayStation.NewArrivalOnBoard("var");

            Assert.AreEqual($"There are other trains to arrive before var."
                , railwayStation.TrainHasArrived("var"));
        }
        [Test]
        public void Test5()
        {
            RailwayStation railwayStation = new RailwayStation("pleven");
            railwayStation.NewArrivalOnBoard("sof");
            railwayStation.NewArrivalOnBoard("var");

            Assert.AreEqual("sof is on the platform and will leave in 5 minutes.",
                railwayStation.TrainHasArrived("sof"));
            Assert.AreEqual(1, railwayStation.ArrivalTrains.Count());
        }
        [Test]
        public void Test6()
        {
            RailwayStation railwayStation = new RailwayStation("pleven");
            railwayStation.NewArrivalOnBoard("sof");
            railwayStation.NewArrivalOnBoard("var");
            railwayStation.TrainHasArrived("sof");

            Assert.IsTrue(railwayStation.TrainHasLeft("sof"));
            Assert.AreEqual(railwayStation.DepartureTrains.Count(), 0);

        }
        [Test]
        public void Test7()
        {
            RailwayStation railwayStation = new RailwayStation("pleven");
            railwayStation.NewArrivalOnBoard("sof");
            railwayStation.NewArrivalOnBoard("var");
            railwayStation.TrainHasArrived("sof");

            Assert.IsFalse(railwayStation.TrainHasLeft("var"));
            Assert.AreEqual(1, railwayStation.DepartureTrains.Count());
        }
    }
}