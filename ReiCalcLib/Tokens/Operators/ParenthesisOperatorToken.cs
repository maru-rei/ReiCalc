namespace ReiCalcLib.Tokens.Operators
{
    public abstract class ParenthesisOperatorToken : OperatorToken
    {
        public override int Precedence => 5;

        public override EAssociativity Associativity => EAssociativity.Left;

        public override NumberToken Execute(params NumberToken[] inputTokens)
        {
            throw new NotImplementedException();
        }
    }
}
