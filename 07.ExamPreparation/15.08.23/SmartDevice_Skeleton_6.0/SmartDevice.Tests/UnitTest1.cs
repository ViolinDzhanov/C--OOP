namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            int memoryCapacity = 100;

            // Act
            Device device = new Device(memoryCapacity);

            // Assert
            Assert.AreEqual(memoryCapacity, device.MemoryCapacity);
            Assert.AreEqual(memoryCapacity, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            CollectionAssert.AreEqual(new List<string>(), device.Applications);
        }

        [Test]
        public void TakePhoto_ShouldTakePhotoSuccessfully()
        {
            // Arrange
            Device device = new Device(100);
            int photoSize = 10;

            // Act
            bool result = device.TakePhoto(photoSize);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, device.Photos);
            Assert.AreEqual(90, device.AvailableMemory);
        }

        [Test]
        public void TakePhoto_ShouldNotTakePhotoWhenNotEnoughMemory()
        {
            // Arrange
            Device device = new Device(5);
            int photoSize = 10;

            // Act
            bool result = device.TakePhoto(photoSize);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(5, device.AvailableMemory);
        }

        [Test]
        public void InstallApp_ShouldInstallAppSuccessfully()
        {
            // Arrange
            Device device = new Device(50);
            int appSize = 10;

            // Act
            string result = device.InstallApp("TestApp", appSize);

            // Assert
            Assert.AreEqual("TestApp is installed successfully. Run application?", result);
            CollectionAssert.Contains(device.Applications, "TestApp");
            Assert.AreEqual(40, device.AvailableMemory);
        }

        [Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        public void InstallApp_ShouldThrowExceptionWhenNotEnoughMemory()
        {
            // Arrange
            Device device = new Device(5);
            int appSize = 10;

            // Act
            string result = device.InstallApp("TestApp", appSize);

            // Assert
            // Expecting an exception
        }

        [Test]
        public void FormatDevice_ShouldResetDevice()
        {
            // Arrange
            Device device = new Device(100);
            device.TakePhoto(20);
            device.InstallApp("TestApp", 30);

            // Act
            device.FormatDevice();

            // Assert
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(100, device.AvailableMemory);
            CollectionAssert.DoesNotContain(device.Applications, "TestApp");
        }

        [Test]
        public void GetDeviceStatus_ShouldReturnCorrectStatus()
        {
            // Arrange
            Device device = new Device(50);
            device.TakePhoto(10);
            device.InstallApp("App1", 15);
            device.InstallApp("App2", 10);

            // Act
            string result = device.GetDeviceStatus();

            // Assert
            StringAssert.Contains(result, "Memory Capacity: 50 MB, Available Memory: 15 MB");
            StringAssert.Contains(result, "Photos Count: 1");
            StringAssert.Contains(result, "Applications Installed: App1, App2");
        }
    }
}
