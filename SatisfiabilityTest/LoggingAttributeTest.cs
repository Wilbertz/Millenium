using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /// A test for Formula Constructor
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
    }
}
