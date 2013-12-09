namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
