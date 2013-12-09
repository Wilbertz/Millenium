namespace SatisfiabilityTest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Satisfiability;

    /// <summary>
    /// This class is used in order to test the implementation of the DPLL algorithm.
    /// </summary>
    [TestClass]
    public class DPLLAlgorithmTest
    {
        [TestMethod]
        public void Test_0001_DPLLAlgorithmConstructor()
        {
            IEnumerable<IClause> clauses = new Clause[] 
            { 
                new Clause(new int[] { 2, 3, 6 }, new int[] { 1, 4 }),
                new Clause(new int[] { 1, 5, 6 }, new int[] { 2, 3, 7 })
            };
            Formula formula = new Formula(clauses);
            Assert.IsNotNull(formula);

            DPLLAlgorithm algorithm = new DPLLAlgorithm(formula);
            Assert.IsNotNull(algorithm);
        }

        [TestMethod]
        public void Test_0002_DPLLAlgorithmSolve()
        {
            IEnumerable<IClause> clauses = new Clause[] 
            { 
                new Clause(new int[] { 2, 3, 6 }, new int[] { 1, 4 }),
                new Clause(new int[] { 1, 5, 6 }, new int[] { 2, 3, 7 })
            };
            Formula formula = new Formula(clauses);
            Assert.IsNotNull(formula);

            DPLLAlgorithm algorithm = new DPLLAlgorithm(formula);
            Assert.IsNotNull(algorithm);

            Dictionary<int, bool> solution = new Dictionary<int, bool>(algorithm.Solve());
            Assert.IsNotNull(solution);
        }
    }
}
