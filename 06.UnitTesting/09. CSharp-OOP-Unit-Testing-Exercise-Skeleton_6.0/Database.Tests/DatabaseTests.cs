namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void SetUp()
        {
            database = new Database(1, 2);
        }

        [Test]
        public void CountWorksProperly()
        {
            int expectedResult = 2;

            Assert.IsNotNull(database);
            Assert.AreEqual(expectedResult, database.Count);
        }

        [TestCase(new int[] {1, 2, 3, 4})]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16})]
        public void CreatingDatabaseAddElementsProperly(int[] data)
        {
            database = new Database(data);

            int[] actualresult = database.Fetch();

            Assert.AreEqual(actualresult, data);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })]
        public void CreateDatabaseThrowsException(int[] data)
        {
            
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database = new Database(data));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void DatabaseAddMethodShouldWorkCorrectly()
        {
            int expectedResult = 3;

            database.Add(-5);

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void DatabaseRemoveMethodThrowsException()
        {
            database = new Database();

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Remove());

            Assert.AreEqual("The collection is empty!", ex.Message);
        }

        [Test]
        public void DatabaseRemoveMethodShouldWorkCorrectly()
        {
            int expectedResult = 1;

            database.Remove();

            Assert.AreEqual(expectedResult, database.Count);
        }
    }
}
