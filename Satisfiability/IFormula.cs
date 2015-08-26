namespace Satisfiability
{
    using System.Collections.Generic;

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
