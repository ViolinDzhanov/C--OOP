using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        private CoffeeMat coffeeMat;

        [SetUp]
        public void Setup()
        {
            coffeeMat = new(150, 2);
        }

        [Test]
        public void Constructor()
        {
            Assert.IsNotNull(coffeeMat);
            Assert.AreEqual(150, coffeeMat.WaterCapacity);
            Assert.AreEqual(2, coffeeMat.ButtonsCount);
            Assert.AreEqual(0, coffeeMat.Income);
        }
        [Test]
        public void FillWaterTankTest()
        {
            coffeeMat.FillWaterTank();
            Assert.AreEqual("Water tank is already full!", coffeeMat.FillWaterTank());
        }
        [Test]
        public void FillWaterTankTest1()
        {

            Assert.AreEqual("Water tank is filled with 150ml", coffeeMat.FillWaterTank());
        }
        [Test]
        public void AddDrinkTest()
        {
            coffeeMat.AddDrink("kafe", 2.2);
            Assert.IsTrue(coffeeMat.AddDrink("latte", 2.1));

        }
        [Test]
        public void AddDrinkTest1()
        {
            coffeeMat.AddDrink("kafe", 2.2);
            coffeeMat.AddDrink("latte", 2.1);
            Assert.IsFalse(coffeeMat.AddDrink("latte", 2.1));
            Assert.IsFalse(coffeeMat.AddDrink("moka", 1.50));

        }
        [Test]
        public void BuyDrinkTest()
        {
            coffeeMat.AddDrink("kafe", 2.2);
            coffeeMat.AddDrink("latte", 2.1);
            Assert.AreEqual("CoffeeMat is out of water!", coffeeMat.BuyDrink("latte"));

        }
        [Test]
        public void BuyDrinkTest1()
        {
            coffeeMat.AddDrink("kafe", 2.2);
            coffeeMat.AddDrink("latte", 2.1);
            coffeeMat.FillWaterTank();
            Assert.AreEqual("Your bill is 2.10$", coffeeMat.BuyDrink("latte"));
        }
        [Test]
        public void BuyDrinkTest2()
        {
            coffeeMat.AddDrink("kafe", 2.2);
            coffeeMat.AddDrink("latte", 2.1);
            coffeeMat.FillWaterTank();
            Assert.AreEqual("mokka is not available!", coffeeMat.BuyDrink("mokka"));
        }
        [Test]
        public void IncomeTest2()
        {
            coffeeMat.AddDrink("kafe", 2.2);
            coffeeMat.AddDrink("latte", 2.1);
            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("latte");

            Assert.AreEqual(2.10, coffeeMat.CollectIncome());
        }
    }
}