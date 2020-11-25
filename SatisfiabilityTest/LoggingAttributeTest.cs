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

        private const string Indentation = "   ";

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
        public void Test_0002_MethodCallIsLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            classUnderTest.MethodToBeTested();

            // Assert
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => 
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.MethodToBeTested [0] params"))), Times.Once);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => s.Equals("Exit: []"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Test_0003_MethodArgumentsAreLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            classUnderTest.MethodWith2ArgumentsToBeTested(42, "UnitTest");

            // Assert
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.MethodWith2ArgumentsToBeTested [2] params"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Exactly(2));
            mockedLogger.Verify(m => m.Debug(It.Is<string>(s => s.Equals("firstArgument: 42"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.Is<string>(s => s.Equals("secondArgument: UnitTest"))), Times.Once);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => s.Equals("Exit: []"))), Times.Once);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Test_0004_ReturnValueIsLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            var result = classUnderTest.MethodWithReturnValueToBeTested();

            // Assert

            Assert.AreEqual(42, result);

            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.MethodWithReturnValueToBeTested [0] params"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => s.Equals("Exit: [42]"))), Times.Once);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task Test_0005_AsyncReturnValueIsLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            var result = await classUnderTest.AsyncMethodToBeTested();

            // Assert
            Assert.AreEqual(42, result);

            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.AsyncMethodToBeTested [0] params"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => s.Equals("Exit: [42]"))), Times.Once);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task Test_0006_AsyncReturnIsLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            await classUnderTest.AsyncMethodWithoutReturnValueToBeTested();

            // Assert
            
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.AsyncMethodWithoutReturnValueToBeTested [0] params"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s => s.Equals("Exit: []"))), Times.Once);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Test_0007_ExceptionIsLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            try
            {
                classUnderTest.MethodThatThrowsExceptionToBeTested();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(Exception));
            }

            // Assert
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.Is<string>(s => s.Equals("OnException: System.Exception: UnitTestException"))), Times.Once);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.MethodThatThrowsExceptionToBeTested [0] params"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task Test_0008_ExceptionInAsyncMethodIsLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act
            try
            {
                await classUnderTest.AsyncMethodThatThrowsExceptionToBeTested();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(Exception));
            }

            // Assert
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Error(It.Is<string>(s => s.Equals("OnException: System.Exception: UnitTestException"))), Times.Once);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
                s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.AsyncMethodThatThrowsExceptionToBeTested [0] params"))), Times.Once);
            mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Test_0009_NestedMethodsAreLoggedWithCorrectIndentationLogged()
        {
            // Arrange 
            var mockedLogger = GetLogger();
            var classUnderTest = new ClassUnderTest();

            SetLoggingInterfaceInAttributeOfClassUnderTest(mockedLogger.Object);

            // Act

            classUnderTest.NestedMethodToBeTested1();
           
            // Assert
            mockedLogger.Verify(m => m.Fatal(It.IsAny<string>()), Times.Never);
            mockedLogger.Verify(m => m.Warn(It.IsAny<string>()), Times.Never);
            //mockedLogger.Verify(m => m.Info(It.Is<string>(s =>
            //    s.Equals("Init: SatisfiabilityTest.LoggingAttributeTest+ClassUnderTest.AsyncMethodThatThrowsExceptionToBeTested [0] params"))), Times.Once);
            //mockedLogger.Verify(m => m.Debug(It.IsAny<string>()), Times.Never);
            //mockedLogger.Verify(m => m.Trace(It.IsAny<string>()), Times.Never);
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

            public void NestedMethodToBeTested1()
            {
                NestedMethodToBeTested2();
            }
            public void NestedMethodToBeTested2()
            {
                NestedMethodToBeTested3();
            }

            public void NestedMethodToBeTested3()
            {
                NestedMethodToBeTested4();
            }

            public void NestedMethodToBeTested4()
            {

            }

            public void MethodThatThrowsExceptionToBeTested()
            {
                throw new Exception("UnitTestException");
            }

            public Task<int> AsyncMethodToBeTested()
            {
                return Task.FromResult(42);
            }

            public Task AsyncMethodWithoutReturnValueToBeTested()
            {
                return Task.Delay(100);
            }

            public Task AsyncMethodThatThrowsExceptionToBeTested()
            {
                throw new Exception("UnitTestException");
            }
        }

        private void SetLoggingInterfaceInAttributeOfClassUnderTest(ILogger logger)
        {
            typeof(LoggingAttribute)
                .GetField("Logger", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, logger);
        }

        private Mock<ILogger> GetLogger()
        {
            var mockedLogger = new Mock<ILogger>();

            mockedLogger.Setup(m => m.Fatal(It.IsAny<string>())).Callback<string>(Console.WriteLine);
            mockedLogger.Setup(m => m.Error(It.IsAny<string>())).Callback<string>(Console.WriteLine);
            mockedLogger.Setup(m => m.Warn(It.IsAny<string>())).Callback<string>(Console.WriteLine);
            mockedLogger.Setup(m => m.Info(It.IsAny<string>())).Callback<string>(Console.WriteLine);
            mockedLogger.Setup(m => m.Debug(It.IsAny<string>())).Callback<string>(Console.WriteLine);
            mockedLogger.Setup(m => m.Trace(It.IsAny<string>())).Callback<string>(Console.WriteLine);

            return mockedLogger;
        }

        #endregion
    }
}
