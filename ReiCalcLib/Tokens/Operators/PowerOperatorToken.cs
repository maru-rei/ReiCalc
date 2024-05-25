namespace ReiCalcLib.Tokens.Operators
{
    public class PowerOperatorToken : OperatorToken
    {
        public override string ExpressionPattern => "^";

        public override int Precedence => 4;

        public override EAssociativity Associativity => EAssociativity.Right;

        public override NumberToken Execute(params NumberToken[] inputTokens)
        {
            // TODO: Validate inputTokens
            return new NumberToken(Math.Pow(inputTokens[0].Value, inputTokens[1].Value));
        }
    }
}
