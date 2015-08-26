namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// This class represents a boolean expression in conjunctive normal form.
    /// </summary>
    public class Formula : IFormula, ICloneable
    {
        #region Fields and Properties -----------------------------------------

        private List<IClause> _clauses;

        public IEnumerable<IClause> Clauses => _clauses;

        public bool IsUnsat
        {
            get 
            {
                if (Clauses != null)
                {
                    return Clauses.Any(c => c.IsUnsat);
                }
                else
                {
                    return false;
                }
            }
        }

        public IEnumerable<int> Variables
        {
            get
            {
                IEnumerable<int> variables = new HashSet<int>();

                foreach (var clause in Clauses)
                {
                    var c = (Clause) clause;
                    variables = variables.Union(c.Variables);
                    variables = variables.Union(c.NegatedVariables);
                }

                return variables;
            }
        }

        #endregion

        #region Constructors --------------------------------------------------

        private Formula()
        {
        }

        public Formula(IEnumerable<IClause> clauses)
        {
            Contract.Requires(clauses != null);

            _clauses = new List<IClause>(clauses);
        }

        #endregion

        #region IFormula Members ----------------------------------------------

        public void SubstituteAsTrue(int variable)
        {
            Contract.Assume(Clauses != null);
            foreach (var clause in Clauses)
            {
                var c = (Clause) clause;
                Contract.Assume(c != null);
                c.SubstituteAsTrue(variable);
            }
        }

        public void SubstituteAsFalse(int variable)
        {
            Contract.Assume(Clauses != null);
            foreach (var clause in Clauses)
            {
                var c = (Clause) clause;
                Contract.Assume(c != null);
                c.SubstituteAsFalse(variable);
            }
        }

        #endregion

        #region ICloneable Members --------------------------------------------

        public object Clone()
        {
            Formula f = new Formula {_clauses = new List<IClause>(Clauses)};

            return f;
        }

        #endregion
    }
}