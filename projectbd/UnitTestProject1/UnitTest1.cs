using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            // Arrange
            AddBird addBird = new AddBird();
            string id = "123456";

            // Act
            bool result = addBird.IsValidId(id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidLetters_WithValidLetters_ShouldReturnTrue()
        {
            // Arrange
            AddBird addBird = new AddBird();
            string text = "ABCabc";

            // Act
            bool result = addBird.IsValidLetters(text);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
    