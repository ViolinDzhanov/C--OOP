namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]
        public void Setup()
        {
            car = new("Mercedes", "C200", 5, 100);
        }

        [Test] 
        public void ConstructorWorksProperly()
        {
            Assert.IsNotNull(car);
            Assert.AreEqual("Mercedes", car.Make);
            Assert.AreEqual("C200", car.Model);
            Assert.AreEqual(5, car.FuelConsumption);
            Assert.AreEqual(100, car.FuelCapacity);
        }

        [TestCase("")]
        [TestCase(null)]
        public void CarNameShouldThrowException(string name)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car = new(name, "C200", 5, 100));

            Assert.AreEqual("Make cannot be null or empty!", ex.Message);
        }
        [TestCase("")]
        [TestCase(null)]
        public void CarModelShouldThrowException(string name)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car = new("Mercedes", name, 5, 100));

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CarFuelConsumptionShouldThrowException(double fuelConsumption)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car = new("Mercedes", "C200", fuelConsumption, 100));

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CarFuelCapacityShouldThrowException(double fuelConsumption)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car = new("Mercedes", "C200", 5, fuelConsumption));

            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void CarFuelAmountsetTo0WhenInitialisation()
        {
            double expected = 0;
            double actual = car.FuelAmount;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CarFuelAmountShouldThrowExceptionIfIsNegative()
        {
            Assert.Throws<InvalidOperationException>(()
                => car.Drive(12), "Fuel amount cannot be negative!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void RefuelMethodShouldThrowException(double fuelQuantity)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => car.Refuel(fuelQuantity));

            Assert.AreEqual("Fuel amount cannot be zero or negative!", ex.Message);
        }

        [Test]
        public void RefuelMethodShouldWorkProperly()
        {
            car.Refuel(50);

            Assert.AreEqual(50, car.FuelAmount);
        }

        [Test]
        public void RefuelMethodShouldRefuelNoMoreThanCapacity()
        {
            int expectedResult = 100;
            car.Refuel(120);

            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void RefuelMethodShouldRefuelProperly()
        {
            int expectedResult = 50;
            car.Refuel(50);

            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DriveMethodShouldWorkProperly()
        {
            car.Refuel(10);
            car.Drive(100);

            Assert.AreEqual(5, car.FuelAmount);
        }
        [Test]
        public void DriveMethodShouldThrowException()
        {
           InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() 
               => car.Drive(100));

            Assert.AreEqual("You don't have enough fuel to drive!", ex.Message);
        }
    }
}