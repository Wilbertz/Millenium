using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;
using Satisfiability.Common;

namespace SatisfiabilityTest
{
    /// <summary>
    /// This is a test class for LoggingAttribute and is intended
    /// to contain all LoggingAttribute Unit Tests
    /// </summary>
    [TestClass]
    public class LoggingAttributeTest
    {
        #region Fields and Properties -----------------------------------------

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        /// <summary>
        /// A test for LoggingAttribute Constructor
        /// </summary>
        [TestMethod]
        public void Test_0001_LoggingAttributeConstructor()
        {
            // Arrange

            // Act
            var result = new LoggingAttribute();

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Test_0002_LoggingAttributeConstructor()
        {
            // Arrange 
            var mockedLogger = new Mock<ILogger>();
            //mockedLogger.Setup(m => m.Info(It.IsAny<string>())).
            var classUnderTest = CreateClassUnderTestWithInjectedLogger(mockedLogger.Object);

            // Act
            classUnderTest.MethodToBeTested();

            // Assert
            mockedLogger.Verify(m => m.Info(It.IsAny<string>()), Times.AtLeastOnce);

        }

        #region Helper --------------------------------------------------------

        [Logging]
        private class ClassUnderTest
        {
            public void MethodToBeTested()
            {

            }
        }

        private ClassUnderTest CreateClassUnderTestWithInjectedLogger(ILogger logger)
        {
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(classUnderTest, logger);

            return classUnderTest;
        }

        private void SetLoggingInterfaceInAttributeOfClassUnderTest(object classUnderTest, ILogger logger)
        {
            var attribute = classUnderTest
                .GetType()
                .GetCustomAttributes<LoggingAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                var loggerField = typeof(LoggingAttribute).GetField("_logger", BindingFlags.Instance | BindingFlags.NonPublic);
                loggerField?.SetValue(attribute, logger);
            }
        }

        #endregion
    }
}
