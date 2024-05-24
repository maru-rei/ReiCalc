using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ReiCalcLib
{
    public class ReiCalc
    {
        private Regex regexWhitespace;
        private Tokenizer tokenizer;

        public ReiCalc()
        {
            regexWhitespace = new Regex(@"\s+");
            tokenizer = new Tokenizer();
        }

        public double Calculate(string expression)
        {
            // TODO: Validate the expression

            // Remove all whitespace from the expression
            regexWhitespace.Replace(expression, "");

            Token[] expressionTokens = tokenizer.TokenizeExpression(expression);

            return 42.1234;
        }

        
    }
}