namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        Database database;

        [SetUp]
        public void Setup()
        {
            Person[] persons =
            {
                new Person(1, "Asen"),
                new Person(2, "Biser"),
                new Person(3, "Velio"),
                new Person(4, "Galq"),
                new Person(5, "Dani"),
                new Person(6, "Elena"),
                new Person(7, "Jivko"),
                new Person(8, "Zdravko"),
                new Person(9, "Ivan"),
                new Person(10, "Iordan"),
            };

            database = new(persons);
        }

        [Test]
        public void CreatingDatabaseCountShouldWork()
        {
            int expectedResult = 10;

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void CreatingDatabaseShouldThrowExceptionWhenCountIsMoreThan16()
        {
            Person[] persons =
            {
                new Person(1, "Asen"),
                new Person(2, "Biser"),
                new Person(3, "Velio"),
                new Person(4, "Galq"),
                new Person(5, "Dani"),
                new Person(6, "Elena"),
                new Person(7, "Jivko"),
                new Person(8, "Zdravko"),
                new Person(9, "Ivan"),
                new Person(10, "Iordan"),
                new Person(11, "Konstantin"),
                new Person(12, "Lili"),
                new Person(13, "Mariq"),
                new Person(14, "Neli"),
                new Person(15, "Ognqn"),
                new Person(16, "Petq"),
                new Person(17, "Reni"),
            };

            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => database = new Database(persons));

            Assert.AreEqual("Provided data length should be in range [0..16]!", ex.Message);
        }

        [Test]
        public void AddMethodWorksProperly()
        {
            int expectedResult = 11;

            Person person = new(11, "Kalin");

            database.Add(person);

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenAddMoreThan16Persons()
        {
            Person[] persons =
            {
               new Person(1, "Asen"),
               new Person(2, "Biser"),
               new Person(3, "Velio"),
               new Person(4, "Galq"),
               new Person(5, "Dani"),
               new Person(6, "Elena"),
               new Person(7, "Jivko"),
               new Person(8, "Zdravko"),
               new Person(9, "Ivan"),
               new Person(10, "Iordan"),
               new Person(11, "Konstantin"),
               new Person(12, "Lili"),
               new Person(13, "Mariq"),
               new Person(14, "Neli"),
               new Person(15, "Ognqn"),
               new Person(16, "Petq"),
            };

            database = new(persons);

            var person = new Person(17, "Reni");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(person));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenAddExistingUserName()
        {
            var person = new Person(11, "Asen");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(person));

            Assert.AreEqual("There is already user with this username!", ex.Message);
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenAddExistingUserId()
        {
            var person = new Person(1, "Reni");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(person));

            Assert.AreEqual("There is already user with this Id!", ex.Message);
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionWhenEmptyAndRemoveCommandIsCalled()
        {
            database = new();

             Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void RemoveMethodShouldWorkProperly()
        {
            int expectedResult = 9;

            database.Remove();

            Assert.AreEqual(expectedResult, database.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void FindByUserNameMethodShouldThrowExceptionWhenNameIsNull(string name)
        {            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(()
                => database.FindByUsername(name));

            Assert.AreEqual("Username parameter is null!", ex.ParamName);
        }
        [Test]
        public void DatabaseFindByUsernameMethodShouldBeCaseSensitive()
        {
            string expectedResult = "asen";
            string actualResult = database.FindByUsername("Asen").UserName;

            Assert.AreNotEqual(expectedResult, actualResult);
        }

        [TestCase("Neli")]
        [TestCase("Reni")]
        public void FindByUserNameMethodShouldThrowExceptionWhenNameIsNotFound(string name)
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.FindByUsername(name));

            Assert.AreEqual("No user is present by this username!", ex.Message);
        }

        [TestCase("Asen")]
        public void FindByUserNameMethodShouldWorkCorrectly(string name)
        {
            string expectedResult = "Asen";
            string actualResult = database.FindByUsername(name).UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void FindByIDMethodShouldThrowExceptionWhenNegativeIdIsGiven()
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => database.FindById(-1));

            Assert.AreEqual("Id should be a positive number!", ex.ParamName);
        }

        [Test]
        public void FindByIDMethodShouldThrowExceptionWhenInvalidIdIsGiven()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.FindById(25));

            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }

        [Test]
        public void FindByIDMethodShouldWorkProperly()
        {
            long expectedResult = 1;

            Assert.AreEqual(expectedResult, database.FindById(1).Id);
        }
    }
}