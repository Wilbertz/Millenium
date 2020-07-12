using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using NLog;

namespace Satisfiability
{
    using Satisfiability.Common;
    /// <summary>
    /// This class is used in order to present a logical disjunction of 
    /// several possibly negated variables.
    /// </summary>
    [Logging]
    public class Clause : ContextBoundObject, IClause, ICloneable
    {
        #region Fields and Properties -----------------------------------------

        public ISet<int> Variables { get; set; }

        public ISet<int> NegatedVariables { get; set; }

        public bool IsUnsat { get; set; }

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

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

            Variables = new HashSet<int>(variableList);
            NegatedVariables = new HashSet<int>(negatedVariableList);
        }

        #endregion

        #region IClause Members -----------------------------------------------

        public void SubstituteAsTrue(int variable)
        {
            Contract.Requires(this.Variables != null);
            Contract.Requires(this.NegatedVariables != null);

            if (Variables.Contains(variable))
            {
                Variables.Remove(variable);
            }

            if (NegatedVariables.Contains(variable))
            {
                IsUnsat = true;
            }
        }

        public void SubstituteAsFalse(int variable)
        {
            Contract.Assume(Variables != null);
            Contract.Assume(NegatedVariables != null);

            if (Variables.Contains(variable))
            {
                IsUnsat = true;
            }

            if (NegatedVariables.Contains(variable))
            {
                NegatedVariables.Remove(variable);
            }
        }

        #endregion

        #region ICloneable Members --------------------------------------------

        public Object Clone()
        {
            Clause c = new Clause()
            {
                Variables = new HashSet<int>(Variables),
                NegatedVariables = new HashSet<int>(NegatedVariables),
                IsUnsat = IsUnsat
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
