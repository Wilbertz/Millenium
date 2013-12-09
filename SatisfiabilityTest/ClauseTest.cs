namespace SatisfiabilityTest
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Satisfiability;

    /// <summary>
    /// This is a test class for ClauseTest and is intended
    /// to contain all ClauseTest Unit Tests
    /// </summary>
    [TestClass]
    public class ClauseTest
    {
        #region Fields and Properties -----------------------------------------

        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

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
            Clause target = new Clause(new int[] { 2, 3, 6 }, new int[] { 1, 4 });
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
            Clause target = new Clause(new int[] { 2, 0, 6 }, new int[] { 1, 2, 4 });
        }

        /// <summary>
        /// A test for Clause Constructor with null input value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_0003_ClauseConstructor()
        {
            Clause target = new Clause(null, new int[] { 1, 2, 4 });
        }

        /// <summary>
        /// A test for Clause Constructor with null input value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_0004_ClauseConstructor()
        {
            Clause target = new Clause(new int[] { 2, 0, 6 }, null);
        }

        /// <summary>
        /// A test for the clone operation.
        /// </summary>
        [TestMethod]
        public void Test_0005_Clone()
        {
            Clause source = new Clause(new int[] { 2, 3, 6 }, new int[] { 1, 4 });
            Clause clone = source.Clone() as Clause;
            Assert.IsNotNull(clone, "Could not clone instance.");

            clone.SubstituteAsTrue(1);
            clone.SubstituteAsFalse(2);
        }

        #endregion
    }
}
