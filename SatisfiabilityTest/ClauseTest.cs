using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using NLog.Targets;

namespace SatisfiabilityTest
{
    using Satisfiability;

    /// <summary>
    /// This is a test class for ClauseTest and is intended
    /// to contain all ClauseTest Unit Tests
    /// </summary>
    [TestClass]
    public class ClauseTest
    {
        #region Fields and Properties -----------------------------------------

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Additional test attributes ------------------------------------

        #endregion

        #region Tests ---------------------------------------------------------

        /// <summary>
        /// A test for Clause Constructor.
        /// </summary>
        [TestMethod]
        public void Test_0001_ClauseConstructor()
        {
            // Arrange and Act
            Clause target = new Clause(new[] { 2, 3, 6 }, new [] { 1, 4 });

            // Assert
            Assert.IsNotNull(target, "Could not create clause instance.");
            Assert.IsFalse(target.IsUnsat);
        }

        /// <summary>
        /// A test for Clause Constructor with invalid arguments.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_0002_ClauseConstructor()
        {
            // Arrange and Act
            Clause target = new Clause(new [] { 2, 0, 6 }, new [] { 1, 2, 4 });

            // Assert
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// A test for Clause Constructor with null input value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_0003_ClauseConstructor()
        {
            // Arrange and Act
            Clause target = new Clause(null, new [] { 1, 2, 4 });

            // Assert
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// A test for Clause Constructor with null input value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_0004_ClauseConstructor()
        {
            // Arrange and Act
            Clause target = new Clause(new [] { 2, 0, 6 }, null);

            // Assert
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// A test for the clone operation.
        /// </summary>
        [TestMethod]
        public void Test_0005_Clone()
        {
            // Arrange
            Clause source = new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 });

            // Act
            Clause clone = source.Clone() as Clause;

            // Assert
            Assert.IsNotNull(clone, "Could not clone instance.");

            clone.SubstituteAsTrue(1);
            clone.SubstituteAsFalse(2);
        }

        #endregion
    }
}
