namespace ReiCalcLib.Tokens.Operators
{
    public class SubtractOperatorToken : OperatorToken
    {
        public override string ExpressionPattern => "-";

        public override int Precedence => 2;

        public override EAssociativity Associativity => EAssociativity.Left;

        public override NumberToken Execute(params NumberToken[] inputTokens)
        {
            // TODO: Validate inputTokens
            return new NumberToken(inputTokens[0].Value - inputTokens[1].Value);
        }
    }
}
