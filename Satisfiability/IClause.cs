namespace Satisfiability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IClause 
    {
        void SubstituteAsTrue(int variable);

        void SubstituteAsFalse(int variable);

        bool IsUnsat { get; }

        ISet<int> Variables { get; }

        ISet<int> NegatedVariables { get; }
    }
}
