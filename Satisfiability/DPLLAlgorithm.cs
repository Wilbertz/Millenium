namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class implements the Davis, Putnam, Logemann and Putland
    /// algorithm, that is used to solve an instance of the K-SAT problem.
    /// </summary>
    public class DPLLAlgorithm
    {
        #region Fields and Properties -----------------------------------------

        private IFormula formula = null;

        public IFormula Formula
        {
            get { return this.formula; }
        }

        private HashSet<int> assignedVariables = new HashSet<int>();

        public HashSet<int> AssignedVariables
        {
            get { return this.assignedVariables; }
        }

        private HashSet<int> unassignedVariables = new HashSet<int>();

        public HashSet<int> UnassignedVariables
        {
            get { return this.unassignedVariables; }
        }

        #endregion

        #region Constructors --------------------------------------------------

        public DPLLAlgorithm(Formula formula)
        {
            Contract.Requires(formula != null);

            this.formula = formula;
        }

        #endregion

        #region Methods -------------------------------------------------------

        public IDictionary<int, bool> Solve()
        {
            HashSet<int> unassignedVariables = this.InitializeVariables();

            return this.InternalSolve(this.formula, new Dictionary<int, bool>(), unassignedVariables); 
        }

        #endregion

        #region Internal Helper -----------------------------------------------

        [Pure]
        private Dictionary<int, bool> InternalSolve(IFormula formula, Dictionary<int, bool> assignedVariables, HashSet<int> unassignedVariables)
        {
            Contract.Requires(formula != null);
            Contract.Requires(formula.Clauses != null);
            Contract.Requires(assignedVariables != null);
            Contract.Requires(unassignedVariables != null);

            if (unassignedVariables.Count<int>() > 0)
            {
                Dictionary<int, bool> result = new Dictionary<int, bool>();
                int variable = SelectUnassignedVaiable(unassignedVariables);

                assignedVariables.Add(variable, false);
                unassignedVariables.Remove(variable);

                result = this.InternalSolve(formula, assignedVariables, unassignedVariables);
                return result;
            }
            else
            {
                if (formula.Clauses.Count<IClause>() == 0)
                {
                    return assignedVariables;
                }
                else
                {
                    return null;
                }
            }
        }

        [Pure]
        private static int SelectUnassignedVaiable(HashSet<int> unassignedVariables)
        {
            Contract.Requires(unassignedVariables != null);

            return unassignedVariables.First<int>();
        }

        [Pure]
        private HashSet<int> InitializeVariables()
        {
            Contract.Assume(this.Formula != null);
            Contract.Assume(this.Formula.Clauses != null);

            HashSet<int> result = new HashSet<int>();

            foreach (Clause c in this.Formula.Clauses)
            {
                Contract.Assume(c != null);
                Contract.Assume(c.Variables != null);
                Contract.Assume(c.NegatedVariables != null);

                result.UnionWith(c.Variables.Union<int>(c.NegatedVariables));
            }

            return result;
        }

        #endregion
    }
}
