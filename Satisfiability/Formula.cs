namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class represents a boolean expression in conjunctive normal form.
    /// </summary>
    public class Formula : IFormula, ICloneable
    {
        #region Fields and Properties -----------------------------------------

        private List<IClause> clauses = null;

        public IEnumerable<IClause> Clauses
        {
            get 
            { 
                return this.clauses; 
            }
        }

        public bool IsUnsat
        {
            get 
            {
                if (this.Clauses != null)
                {
                    return this.Clauses.Any((c) => c.IsUnsat);
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

                foreach (Clause c in this.Clauses)
                {
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

            this.clauses = new List<IClause>(clauses);
        }

        #endregion

        #region IFormula Members ----------------------------------------------

        public void SubstituteAsTrue(int variable)
        {
            Contract.Assume(this.Clauses != null);
            foreach (Clause c in this.Clauses)
            {
                Contract.Assume(c != null);
                c.SubstituteAsTrue(variable);
            }
        }

        public void SubstituteAsFalse(int variable)
        {
            Contract.Assume(this.Clauses != null);
            foreach (Clause c in this.Clauses)
            {
                Contract.Assume(c != null);
                c.SubstituteAsFalse(variable);
            }
        }

        #endregion

        #region ICloneable Members --------------------------------------------

        public object Clone()
        {
            Formula f = new Formula();

            f.clauses = new List<IClause>(this.Clauses);

            return f;
        }

        #endregion
    }
}