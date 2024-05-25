namespace ReiCalcLib.Tokens
{
    public abstract class OperatorToken : Token
    {
        public abstract string ExpressionPattern { get; }

        public abstract int Precedence { get; }

        public abstract EAssociativity Associativity { get; }

        public abstract NumberToken Execute(params NumberToken[] inputTokens);
    }
}
