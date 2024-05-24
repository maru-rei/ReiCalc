using ReiCalcLib.Tokens;
using System.Globalization;
using System.Text;

namespace ReiCalcLib
{
    public class Tokenizer
    {
        public Token[] TokenizeExpression(string expression)
        {
            List<Token> tokens = new List<Token>();

            StringBuilder tokenStringBuilder = new StringBuilder();
            string tokenTempString = string.Empty;
            Token lastToken = null;

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                if (lastToken == null)
                {
                    if (char.IsNumber(currentChar) ||
                        currentChar == '-' ||
                        currentChar == Symbols.DecimalSeparator)
                    {
                        // Number
                        if (TryFindAndParseNumber(expression, i, out _, out double? parsedNumber))
                        {
                            tokens.Add(new NumberToken(parsedNumber.Value));
                        }
                    }
                    else
                    {

                    }
                    // TODO: Add an else-if case for parenthesis
                }
            }

            return tokens.ToArray();
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

// 3 + 26
// NumberToken(3) AddOperatorToken NumberToken(26)
// 29

// 3 + 26 - -489420
// NumberToken(3) AddOperatorToken NumberToken(26) SubtractOperatorToken NumberToken(-4)
// 489449