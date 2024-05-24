namespace ReiCalcLib.Tokens
{
    public abstract class OperatorToken : Token
    {
        public virtual string ExpressionPattern => null;
    }
}
