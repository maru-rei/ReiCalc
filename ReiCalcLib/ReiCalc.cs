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
            regexWhitespace = new Regex(@"\s+"); // Never changes, faster to construct once and reuse
            tokenizer = new Tokenizer();
            shuntingYard = new ShuntingYard();
            rpnCalculator = new RpnCalculator();
        }

        /// <summary>
        /// Takes a mathmatical expression as a string and calculates its result.
        /// </summary>
        /// <param name="expression">The expression to solve.</param>
        /// <returns>Result of the mathmatical expression.</returns>
        public double Calculate(string expression)
        {
            // TODO: Validate the expression
            //       ^ might not need to, though. Should be possible to do this during tokenization and shunting yard.

            // Remove all whitespace from the expression
            expression = regexWhitespace.Replace(expression, "");

            // First convert the string expression to token objects, parsing from left to right
            Token[] expressionTokens = tokenizer.TokenizeExpression(expression);

            // Rearrange the tokens to reverse Polish notation, using the shunting yard algorithm (chosen for relative ease of implementation)
            Token[] rpnExpression = shuntingYard.Run(expressionTokens);

            // Solve the expression
            double result = rpnCalculator.Calculate(rpnExpression);

            return result;
        }
    }
}