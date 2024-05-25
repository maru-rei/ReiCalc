namespace ReiCalcLib.Tokens.Operations
{
    public class AddOperatorToken : OperatorToken
    {
        public override string ExpressionPattern => "+";

        public override int Precedence => 2;

        public override EAssociativity Associativity => EAssociativity.Left;
    }
}
