namespace Satisfiability
{
    using System.Collections.Generic;

    public interface IClause 
    {
        void SubstituteAsTrue(int variable);

        void SubstituteAsFalse(int variable);

        bool IsUnsat { get; }

        ISet<int> Variables { get; }

        ISet<int> NegatedVariables { get; }
    }
}
