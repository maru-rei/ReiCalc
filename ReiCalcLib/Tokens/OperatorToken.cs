namespace ReiCalcLib.Tokens
{
    public abstract class OperatorToken : Token
    {
        public NumberToken LeftValue { get; set; }

        public NumberToken RightValue { get; set; }
    }
}
