using System;
using System.Reflection;
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

        public Logger Logger { get; } = LogManager.GetCurrentClassLogger();

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
        public void Test_0002_LoggingAttributeMethodsAreCalled()
        {
            // Arrange 
            var mockedLogger = new Mock<ILogger>();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            classUnderTest.MethodToBeTested();

            // Assert
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => 
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.MethodToBeTested [0] params"))), Times.Exactly(1));
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => s.Equals("Exit: []"))), Times.Exactly(1));
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        #region Helper --------------------------------------------------------

        [Logging]
        private class ClassUnderTest
        {
            public void MethodToBeTested() {}
            
            public void MethodWith2ArgumentsToBeTested(int firstArgument, string secondArgument) {}

            public int MethodWithReturnValueToBeTested()
            {
                return 42; 
            }

            public void MethodThatThrowsExceptionToBeTested()
            {
                throw new Exception("UnitTestException");
            }

            public Task<int> AsyncMethodToBeTested()
            {
                return Task.FromResult(42);
            }
        }

        private void SetLoggingInterfaceInAttributeOfClassUnderTest(ILogger logger)
        {
            typeof(LoggingAttribute)
                .GetField("Logger", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, logger);
        }

        #endregion
    }
}
