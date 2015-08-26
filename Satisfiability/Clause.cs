namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// This class is used in order to present a logical disjunction of 
    /// several possibly negated variables.
    /// </summary>
    public class Clause : IClause, ICloneable
    {
        #region Fields and Properties -----------------------------------------

        private ISet<int> _variables;

        public ISet<int> Variables => _variables;

        private ISet<int> _negatedVariables;

        public ISet<int> NegatedVariables => _negatedVariables;

        private bool _isUnsat;

        public bool IsUnsat => _isUnsat;

        #endregion

        #region Constructors --------------------------------------------------

        private Clause()
        {
        }

        public Clause(IEnumerable<int> variables, IEnumerable<int> negatedVariables)
        {
            var variableList = variables as IList<int> ?? variables.ToList();
            var negatedVariableList = negatedVariables as IList<int> ?? negatedVariables.ToList();
            CheckConstructorArguments(variableList, negatedVariableList);

            _variables = new HashSet<int>(variableList);
            _negatedVariables = new HashSet<int>(negatedVariableList);
        }

        #endregion

        #region IClause Members -----------------------------------------------

        public void SubstituteAsTrue(int variable)
        {
            Contract.Requires(this._variables != null);
            Contract.Requires(this._negatedVariables != null);

            if (_variables.Contains(variable))
            {
                _variables.Remove(variable);
            }

            if (_negatedVariables.Contains(variable))
            {
                _isUnsat = true;
            }
        }

        public void SubstituteAsFalse(int variable)
        {
            Contract.Assume(_variables != null);
            Contract.Assume(_negatedVariables != null);

            if (_variables.Contains(variable))
            {
                _isUnsat = true;
            }

            if (_negatedVariables.Contains(variable))
            {
                _negatedVariables.Remove(variable);
            }
        }

        #endregion

        #region ICloneable Members --------------------------------------------

        public object Clone()
        {
            Clause c = new Clause
            {
                _variables = new HashSet<int>(_variables),
                _negatedVariables = new HashSet<int>(_negatedVariables),
                _isUnsat = _isUnsat
            };


            return c;
        }

        #endregion

        #region Internal Helper -----------------------------------------------

        private static void CheckConstructorArguments(IEnumerable<int> variables, IEnumerable<int> negatedVariables)
        {
            if (variables == null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            if (negatedVariables == null)
            {
                throw new ArgumentNullException(nameof(negatedVariables));
            }

            if (variables.Intersect(negatedVariables).Any())
            {
                throw new ArgumentException("Variables and negated Variables cannot contain a common element");
            }    
        }

        #endregion
    }
}
