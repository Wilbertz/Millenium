namespace Satisfiability
{
    using System.Collections.Generic;

    /// <summary>
    /// This interface encodes a formula to be solved.
    /// </summary>
    public interface IFormula
    {
        void SubstituteAsTrue(int variable);

        void SubstituteAsFalse(int variable);

        IEnumerable<IClause> Clauses
        {
            get;
        }
    }
}
