namespace SatisfiabilityTest
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Satisfiability;

    /// <summary>
    /// This class is used in order to test the implementation of the DPLL algorithm.
    /// </summary>
    [TestClass]
    public class DpllAlgorithmTest
    {
        [TestMethod]
        public void Test_0001_DPLLAlgorithmConstructor()
        {
            // Arrange
            IEnumerable<IClause> clauses = new [] 
            { 
                new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 }),
                new Clause(new [] { 1, 5, 6 }, new [] { 2, 3, 7 })
            };

            Formula formula = new Formula(clauses);
            Assert.IsNotNull(formula);

            // Act
            DpllAlgorithm algorithm = new DpllAlgorithm(formula);

            // Assert
            Assert.IsNotNull(algorithm);
        }

        //[TestMethod]
        public void Test_0002_DPLLAlgorithmSolve()
        {
            // Arrange
            IEnumerable<IClause> clauses = new [] 
            { 
                new Clause(new [] { 2, 3, 6 }, new [] { 1, 4 }),
                new Clause(new [] { 1, 5, 6 }, new [] { 2, 3, 7 })
            };
            Formula formula = new Formula(clauses);
            Assert.IsNotNull(formula);

            DpllAlgorithm algorithm = new DpllAlgorithm(formula);
            Assert.IsNotNull(algorithm);

            // Act
            Dictionary<int, bool> solution = new Dictionary<int, bool>(algorithm.Solve());

            // Act
            Assert.IsNotNull(solution);

            // ToDo: Additional Asserts
        }
    }
}
