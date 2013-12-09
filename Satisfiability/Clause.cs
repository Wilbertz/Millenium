namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class is used in order to present a logical disjunction of 
    /// several possibly negated variables.
    /// </summary>
    public class Clause : IClause, ICloneable
    {
        #region Fields and Properties -----------------------------------------

        private ISet<int> variables = null;

        public ISet<int> Variables
        {
            get 
            { 
                return this.variables; 
            }
        }

        private ISet<int> negatedVariables = null;

        public ISet<int> NegatedVariables
        {
            get 
            { 
                return this.negatedVariables; 
            }
        }

        private bool isUnsat = false;

        public bool IsUnsat
        {
            get 
            { 
                return this.isUnsat; 
            }
        }

        #endregion

        #region Constructors --------------------------------------------------

        private Clause()
        {
        }

        public Clause(IEnumerable<int> variables, IEnumerable<int> negatedVariables)
        {
            CheckConstructorArguments(variables, negatedVariables);

            this.variables = new HashSet<int>(variables);
            this.negatedVariables = new HashSet<int>(negatedVariables);
        }

        #endregion

        #region IClause Members -----------------------------------------------

        public void SubstituteAsTrue(int variable)
        {
            Contract.Requires(this.variables != null);
            Contract.Requires(this.negatedVariables != null);

            if (this.variables.Contains(variable))
            {
                this.variables.Remove(variable);
            }

            if (this.negatedVariables.Contains(variable))
            {
                this.isUnsat = true;
            }
        }

        public void SubstituteAsFalse(int variable)
        {
            Contract.Assume(this.variables != null);
            Contract.Assume(this.negatedVariables != null);

            if (this.variables.Contains(variable))
            {
                this.isUnsat = true;
            }

            if (this.negatedVariables.Contains(variable))
            {
                this.negatedVariables.Remove(variable);
            }
        }

        #endregion

        #region ICloneable Members --------------------------------------------

        public object Clone()
        {
            Clause c = new Clause();

            c.variables = new HashSet<int>(this.variables);
            c.negatedVariables = new HashSet<int>(this.negatedVariables);
            c.isUnsat = this.isUnsat;

            return c;
        }

        #endregion

        #region Internal Helper -----------------------------------------------

        [Pure]
        private static void CheckConstructorArguments(IEnumerable<int> variables, IEnumerable<int> negatedVariables)
        {
            if (variables == null)
            {
                throw new ArgumentNullException("variables");
            }

            if (negatedVariables == null)
            {
                throw new ArgumentNullException("negatedVariables");
            }

            if (variables.Intersect<int>(negatedVariables).Count<int>() > 0)
            {
                throw new ArgumentException("Variables and negated Variables cannot contain a common element");
            }    
        }

        #endregion
    }
}
