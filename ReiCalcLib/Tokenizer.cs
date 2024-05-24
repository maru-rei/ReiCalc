using ReiCalcLib.Tokens;
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
                    if (char.IsNumber(currentChar) || currentChar == '-')
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

            if (expression[startIndex] == '-')
            {
                // Need to skip the first char if it's a negative sign and assume the number continues past that
                ++lookahead;
            }

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