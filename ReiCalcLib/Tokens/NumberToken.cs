namespace ReiCalcLib.Tokens
{
    public class NumberToken : Token
    {
        public double Value { get; set; }

        public NumberToken(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
