﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SatisfiabilityTest
{
    using Satisfiability;

    /// <summary>
    /// This is a test class for FormulaTest and is intended
    /// to contain all FormulaTest Unit Tests
    /// </summary>
    [TestClass]
    public class FormulaTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        Logger _logger = LogManager.GetCurrentClassLogger();

        #region Additional test attributes ------------------------------------

        #endregion

        /// <summary>
        /// A test for Formula Constructor
        /// </summary>
        [TestMethod]
        public void Test_0001_FormulaConstructor()
        {
            // Arrange
            IEnumerable<IClause> clauses = new [] 
            { 
                new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 }),
                new Clause(new [] { 1, 5, 6 }, new [] { 2, 3, 7 })
            };

            // Act
            Formula target = new Formula(clauses);

            // Assert
            Assert.IsNotNull(target);

            HashSet<int> variables = new HashSet<int>(target.Variables);
            Assert.IsTrue(variables.Contains(1), "Could not get variable 1");
            Assert.IsTrue(variables.Contains(2), "Could not get variable 2");
            Assert.IsTrue(variables.Contains(3), "Could not get variable 3");
            Assert.IsTrue(variables.Contains(4), "Could not get variable 4");
            Assert.IsTrue(variables.Contains(5), "Could not get variable 5");
            Assert.IsTrue(variables.Contains(6), "Could not get variable 6");
            Assert.IsTrue(variables.Contains(7), "Could not get variable 7");
            Assert.IsFalse(variables.Contains(8), "Could get variable 8");

            Assert.IsTrue(variables.Count == 7);

            Assert.IsFalse(target.IsUnsat);
        }

        /// <summary>
        /// A test for SubstituteAsFalse
        /// </summary>
        [TestMethod]
        public void Test_0002_SubstituteAsFalse()
        {
            // Arrange
            IEnumerable<IClause> clauses = new [] 
            { 
                new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 }),
                new Clause(new [] { 1, 5, 6 }, new [] { 2, 3, 7 }) 
            };

            Formula target = new Formula(clauses); 
            int variable = 1; 

            // Act
            target.SubstituteAsFalse(variable);

            // Asserts
            // ToDo: 
        }

        /// <summary>
        /// A test for SubstituteAsTrue
        /// </summary>
        [TestMethod]
        public void Test_0003_SubstituteAsTrue()
        {
            // Arrange
            IEnumerable<IClause> clauses = new [] 
            { 
                new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 }),
                new Clause(new [] { 1, 5, 6 }, new [] { 2, 3, 7 }) 
            };

            Formula target = new Formula(clauses);

            int variable = 1; 

            // Act
            target.SubstituteAsTrue(variable);

            // Asserts
            // ToDo: 
        }

        /// <summary>
        /// A test for SubstituteAsTrue
        /// </summary>
        [TestMethod]
        public void Test_0004_Clone()
        {
            // Arrange
            IEnumerable<IClause> clauses = new [] 
            { 
                new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 }),
                new Clause(new [] { 1, 5, 6 }, new [] { 2, 3, 7 }) 
            };

            Formula target = new Formula(clauses);

            // Act
            Formula clone = target.Clone() as Formula;

            // Assert
            Assert.IsNotNull(clone);
        }
    }
}
