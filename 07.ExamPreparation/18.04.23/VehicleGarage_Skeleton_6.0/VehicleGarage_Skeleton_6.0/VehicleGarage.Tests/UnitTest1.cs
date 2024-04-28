using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Garage garage;
        [SetUp]
        public void Setup()
        {
            garage = new Garage(2);


        }

        [Test]
        public void Test_1()
        {
            Assert.IsNotNull(garage);
            Assert.AreEqual(2, garage.Capacity);
            Assert.IsNotNull(garage.Vehicles);
        }
        [Test]
        public void Test0()
        {
            Vehicle vehicle = new Vehicle("mercedes", "c200", "asd");
            garage.Vehicles.Add(vehicle);

            Assert.AreEqual(1, garage.Vehicles.Count);
        }
        [Test]
        public void Test_2()
        {
            Vehicle vehicle = new Vehicle("mercedes", "c200", "asd");

            Assert.AreEqual(true, garage.AddVehicle(vehicle));
        }
        [Test]
        public void Test_3()
        {
            Vehicle vehicle = new Vehicle("mercedes", "c200", "asd");
            Vehicle vehicle1 = new Vehicle("bmw", "200", "dfg");
            Vehicle vehicle2 = new Vehicle("vw", "c2", "qwe");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle);

            Assert.AreEqual(false, garage.AddVehicle(vehicle2));
        }
        [Test]
        public void Test_4()
        {
            Vehicle vehicle1 = new Vehicle("bmw", "200", "dfg");
            Vehicle vehicle2 = new Vehicle("vw", "c2", "qwe");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            vehicle1.BatteryLevel = 50;
            vehicle2.BatteryLevel = 40;
            Assert.AreEqual(2, garage.ChargeVehicles(50));
        }
    }
}