using ReiCalcLib.Tokens;
using ReiCalcLib.Tokens.Operations;
using ReiCalcLib.Tokens.Operators;

namespace ReiCalcLib
{
    /// <summary>
    /// Parses a string into a list of <see cref="Token"/> objects.
    /// </summary>
    internal class Tokenizer
    {
        private OperatorToken[] supportedOperatorTokens =
        {
            new AddOperatorToken(),
            new SubtractOperatorToken(),
            new MultiplyOperatorToken(),
            new DivisionOperatorToken(),
            new PowerOperatorToken(),
            new LeftParenthesisOperatorToken(),
            new RightParenthesisOperatorToken()
        };

        /// <summary>
        /// Intermediate token list wrapper object to enforce LastToken to always be set.
        /// </summary>
        private class IntermediateTokenCollection
        {
            /// <summary>
            /// The token that was most recently added to the collection.
            /// </summary>
            public Token LastToken { get; private set; }

            private List<Token> tokens;

            public IntermediateTokenCollection()
            {
                tokens = new List<Token>();
            }

            public void Add(Token token)
            {
                tokens.Add(token);
                LastToken = token;
            }

            public Token[] ToArray()
            {
                return tokens.ToArray();
            }
        }

        /// <summary>
        /// Converts the given math expression string to an array of <see cref="Token"/> in the order they appear in the expression (left to right).
        /// </summary>
        /// <param name="expression">The math expression to tokenize.</param>
        /// <returns>Array of tokens ordered by appearance from left to right.</returns>
        public Token[] TokenizeExpression(string expression)
        {
            // List of parsed tokens which will be returned at the end of this method.
            // Using a wrapper object because the last token must always be set when adding to the list.
            // Hiding the internal list enforces this.
            IntermediateTokenCollection tokens = new IntermediateTokenCollection();

            for (int i = 0; i < expression.Length; i++)
            {
                if (tokens.LastToken == null ||
                    (tokens.LastToken is OperatorToken && tokens.LastToken is not RightParenthesisOperatorToken))
                {
                    // TODO: Implement handling of unexpected chars
                    bool unexpectedCharFlag = false;

                    if (TryParseNumber(expression, i, out _, out double? parsedNumber))
                    {
                        // Number
                        tokens.Add(new NumberToken(parsedNumber.Value));
                    }
                    else if (TryParseOperator(expression, i, out _, out Type matchedOperatorType))
                    {
                        // Special case for left parenthesis "("
                        if (matchedOperatorType == typeof(LeftParenthesisOperatorToken))
                        {
                            tokens.Add(Activator.CreateInstance(matchedOperatorType) as OperatorToken);
                        }
                    }
                    else
                    {
                        unexpectedCharFlag = true;
                    }
                }
                else if (tokens.LastToken is NumberToken || tokens.LastToken is RightParenthesisOperatorToken)
                {
                    if (TryParseOperator(expression, i, out _, out Type matchedOperatorType))
                    {
                        // Operators
                        tokens.Add(Activator.CreateInstance(matchedOperatorType) as OperatorToken);
                    }
                }
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Attempts to parse an operator token starting at the given start index.
        /// Only checks against operators in <see cref="supportedOperatorTokens"/>.
        /// </summary>
        /// <param name="expression">The complete expression string to search the operator in.</param>
        /// <param name="startIndex">Index to start parsing from (must be the first character of the operator to be parsed).</param>
        /// <param name="endIndex">Index of the last character of the parsed operator, -1 if no operator matches.</param>
        /// <param name="matchedOperatorType">
        /// The type of the operator that was matched, or null if nothing matches.
        /// Outputing the type instead of the operator instance to enforce the instantiation of a new operator
        /// in case operators can hold state at some point.
        /// </param>
        /// <returns>True if any operator matches, false otherwise.</returns>
        private bool TryParseOperator(
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
                
                // Regex could have worked here too, but then I'd need to substring the expression first
                // which (I assume) might cause a bunch of GC to happen. This way nothing gets allocated or destroyed in the loop.
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

        /// <summary>
        /// Finds the bounds of a number (double) string and parses it to a double.
        /// </summary>
        /// <param name="expression">The complete expression string to search the number in.</param>
        /// <param name="startIndex">Starting index of the number. Character must be a number (0-9), -, or a decimal separator.</param>
        /// <param name="endIndex">Index of the last character of the number in the expression. -1 if no number was parsed.</param>
        /// <param name="result">The parsed number.</param>
        /// <returns>True if a number was found and parsed, false otherwise.</returns>
        private bool TryParseNumber(
            string expression,
            int startIndex,
            out int endIndex,
            out double? result)
        {
            endIndex = -1;
            result = null;

            // Feels a bit hardcoded, but the language of math is unlikely to change in the foreseeable future
            if (!char.IsNumber(expression[startIndex]) &&
                expression[startIndex] != '-' &&
                expression[startIndex] != Symbols.DecimalSeparator)
            {
                return false;
            }

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
            double parseTemp;
            while (startIndex + lookahead <= expression.Length &&
                double.TryParse(expression.Substring(startIndex, lookahead), out parseTemp))
            {
                lastSuccessfulParseResult = parseTemp;
                ++lookahead;
            }

            endIndex = lastSuccessfulParseResult.HasValue ? startIndex + lookahead : -1;
            result = lastSuccessfulParseResult;
            return lastSuccessfulParseResult.HasValue;
        }
    }
}