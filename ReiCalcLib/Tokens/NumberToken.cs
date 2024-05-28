namespace ReiCalcLib.Tokens
{
    /// <summary>
    /// Represents a complete numerical (decimal) value in a math expression (e.g. -42.123).
    /// </summary>
    public class NumberToken : Token
    {
        public double Value { get; set; }

        public NumberToken(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + $"({Value})";
        }
    }
}
