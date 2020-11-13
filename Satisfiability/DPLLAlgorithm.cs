using System;
using NLog;

namespace Satisfiability
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Common;
    /// <summary>
    /// This class implements the Davis, Putnam, Logemann and Putland
    /// algorithm, that is used to solve an instance of the K-SAT problem.
    /// </summary>
    [Logging]
    public class DpllAlgorithm : ContextBoundObject
    {
        #region Fields and Properties -----------------------------------------

        private readonly IFormula _formula;

        public IFormula Formula => _formula;

        public HashSet<int> AssignedVariables { get; } = new HashSet<int>();

        public HashSet<int> UnassignedVariables { get; } = new HashSet<int>();

        private readonly Logger _logger;

        #endregion

        #region Constructors --------------------------------------------------

        public DpllAlgorithm(Formula formula)
        {
            Contract.Requires(formula != null);

            _logger = LogManager.GetCurrentClassLogger();
            _formula = formula;
        }

        #endregion

        #region Methods -------------------------------------------------------

        public IDictionary<int, bool> Solve()
        {
            HashSet<int> initializeVariables = InitializeVariables();

            return InternalSolve(_formula, new Dictionary<int, bool>(), initializeVariables); 
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

            if (unassignedVariables.Any())
            {
                int variable = SelectUnassignedVaiable(unassignedVariables);

                assignedVariables.Add(variable, false);
                unassignedVariables.Remove(variable);

                var result = InternalSolve(formula, assignedVariables, unassignedVariables);
                return result;
            }
            else
            {
                if (!formula.Clauses.Any())
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
        private int SelectUnassignedVaiable(HashSet<int> unassignedVariables)
        {
            Contract.Requires(unassignedVariables != null);

            return unassignedVariables.First();
        }

        [Pure]
        private HashSet<int> InitializeVariables()
        {
            Contract.Assume(Formula != null);
            Contract.Assume(Formula.Clauses != null);

            HashSet<int> result = new HashSet<int>();

            foreach (var clause in Formula.Clauses)
            {
                var c = (Clause) clause;
                Contract.Assume(c != null);
                Contract.Assume(c.Variables != null);
                Contract.Assume(c.NegatedVariables != null);

                result.UnionWith(c.Variables.Union(c.NegatedVariables));
            }

            return result;
        }

        #endregion
    }
}
