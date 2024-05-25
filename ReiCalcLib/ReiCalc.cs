using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ReiCalcLib
{
    public class ReiCalc
    {
        private Regex regexWhitespace;
        private Tokenizer tokenizer;
        private ShuntingYard shuntingYard;
        private RpnCalculator rpnCalculator;

        public ReiCalc()
        {
            regexWhitespace = new Regex(@"\s+");
            tokenizer = new Tokenizer();
            shuntingYard = new ShuntingYard();
            rpnCalculator = new RpnCalculator();
        }

        public double Calculate(string expression)
        {
            // TODO: Validate the expression

            // Remove all whitespace from the expression
            expression = regexWhitespace.Replace(expression, "");

            Token[] expressionTokens = tokenizer.TokenizeExpression(expression);
            Token[] rpnExpression = shuntingYard.Run(expressionTokens);
            double result = rpnCalculator.Calculate(rpnExpression);

            return result;
        }

        
    }
}