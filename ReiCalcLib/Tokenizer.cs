using ReiCalcLib.Tokens;
using ReiCalcLib.Tokens.Operations;
using ReiCalcLib.Tokens.Operators;
using System.Globalization;
using System.Text;

namespace ReiCalcLib
{
    public class Tokenizer
    {
        private OperatorToken[] supportedOperatorTokens =
        {
            new AddOperatorToken(),
            new SubtractOperatorToken(),
            new MultiplyOperatorToken()
        };

        public Token[] TokenizeExpression(string expression)
        {
            // List of parsed tokens which will be returned at the end of this method.
            // Do not add to this directly, use AddToken instead!
            List<Token> tokens = new List<Token>();

            StringBuilder tokenStringBuilder = new StringBuilder();
            string tokenTempString = string.Empty;
            Token lastToken = null;

            // Convenience function to make it less likely to forget to set lastToken
            void AddToken(Token newToken)
            {
                tokens.Add(newToken);
                lastToken = newToken;
            }

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                if (lastToken == null || lastToken.GetType().IsSubclassOf(typeof(OperatorToken)))
                {
                    if (char.IsNumber(currentChar) ||
                        currentChar == '-' ||
                        currentChar == Symbols.DecimalSeparator)
                    {
                        // Number
                        if (TryFindAndParseNumber(expression, i, out _, out double? parsedNumber))
                        {
                            AddToken(new NumberToken(parsedNumber.Value));
                        }
                    }
                    else
                    {
                        // TODO: Handle unexpected char
                    }
                }
                else if (lastToken.GetType() == typeof(NumberToken))
                {
                    if (TryIdentifyOperator(expression, i, out _, out Type matchedOperatorType))
                    {
                        AddToken(Activator.CreateInstance(matchedOperatorType) as OperatorToken);
                    }
                }
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="matchedOperatorType">
        /// The type of the operator that was matched, or null if nothing matches.
        /// Outputing the type instead of the operator instance to enforce the instantiation of a new operator
        /// in case operators can hold state at some point.
        /// </param>
        /// <returns></returns>
        private bool TryIdentifyOperator(
            string expression,
            int startIndex,
            out int endIndex,
            out Type matchedOperatorType)
        {
            matchedOperatorType = null;
            endIndex = -1;

            foreach (OperatorToken operatorToken in supportedOperatorTokens)
            {
                bool matches = false;

                for (int iPat = 0; iPat < operatorToken.ExpressionPattern.Length; iPat++)
                {
                    if (startIndex + iPat > expression.Length)
                        break;

                    if (operatorToken.ExpressionPattern[iPat] != expression[startIndex + iPat])
                        break;

                    // Reached the end of the expression pattern
                    matches = true;
                    endIndex = startIndex + iPat;
                }

                if (matches)
                {
                    matchedOperatorType = operatorToken.GetType();
                    break;
                }
            }

            return matchedOperatorType != null;
        }

        private bool TryFindAndParseNumber(
            string expression,
            int startIndex,
            out int endIndex,
            out double? result)
        {
            int lookahead = 1;

            if (expression[startIndex] == '-' ||
                expression[startIndex] == Symbols.DecimalSeparator)
            {
                // Need to skip the first char if it's a negative sign or decimal separator
                // and assume the number continues past that
                ++lookahead;
            }

            // There might be a better/more performant way of doing this, but this seems to work well enough for now.
            // Regex could have worked too, would need to do some profiling to see which is faster.
            double? lastSuccessfulParseResult = null;
            double parseResult;
            while (startIndex + lookahead <= expression.Length &&
                double.TryParse(expression.Substring(startIndex, lookahead), out parseResult))
            {
                lastSuccessfulParseResult = parseResult;
                ++lookahead;
            }

            endIndex = startIndex + lookahead;
            result = lastSuccessfulParseResult;
            return lastSuccessfulParseResult.HasValue;
        }
    }
}