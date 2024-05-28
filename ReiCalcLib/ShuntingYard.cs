using ReiCalcLib.Tokens;
using ReiCalcLib.Tokens.Operators;

namespace ReiCalcLib
{
    // Implementation based on https://en.wikipedia.org/wiki/Shunting_yard_algorithm

    public class ShuntingYard
    {
        /// <summary>
        /// Rearranges an array of tokens in infix format (e.g. 1+2) into reverse Polish notation (e.g. 12+).
        /// </summary>
        /// <param name="infixTokens">The token array to be rearranged, must be in infix format.</param>
        /// <returns>The rearranged array of tokens in reverse Polish notation.</returns>
        /// <exception cref="NotImplementedException">Thrown when the algorithm hits an unhandled fault case.</exception>
        public Token[] Run(Token[] infixTokens)
        {
            Queue<Token> inputQueue = new Queue<Token>(infixTokens);
            Queue<Token> outputQueue = new Queue<Token>();
            Stack<OperatorToken> operatorStack = new Stack<OperatorToken>();

            // Pops the operator at the top of the operator stack to the output queue
            void PopOperatorStackToOutput()
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            while (inputQueue.Count > 0)
            {
                Token inputToken = inputQueue.Dequeue();
                if (inputToken is NumberToken)
                {
                    outputQueue.Enqueue(inputToken);
                }
                else if (inputToken is OperatorToken inputOperatorToken)
                {
                    if (inputToken is LeftParenthesisOperatorToken)
                    {
                        operatorStack.Push(inputOperatorToken);
                    }
                    else if (inputToken is RightParenthesisOperatorToken)
                    {
                        // TODO: Check matching parenthesis
                        while (operatorStack.Count > 0 && operatorStack.Peek() is not LeftParenthesisOperatorToken)
                        {
                            PopOperatorStackToOutput();
                        }

                        if (operatorStack.Count > 0)
                        {
                            if (operatorStack.Peek() is LeftParenthesisOperatorToken)
                            {
                                // Discard left parenthesis from operator stack
                                operatorStack.Pop();
                            }
                            else
                            {
                                // TODO: Handle no left parenthesis operator on operator stack
                                throw new NotImplementedException();
                            }
                        }

                        // TODO: Function handling
                    }
                    else
                    {
                        // Non-parenthesis operators
                        while (operatorStack.Count > 0 &&
                            operatorStack.Peek() is not LeftParenthesisOperatorToken &&
                            (operatorStack.Peek().Precedence > inputOperatorToken.Precedence ||
                            (operatorStack.Peek().Precedence == inputOperatorToken.Precedence && inputOperatorToken.Associativity == EAssociativity.Left)))
                        {
                            PopOperatorStackToOutput();
                        }
                        operatorStack.Push(inputOperatorToken);
                    }
                }
            }

            // Pop the remainder of the operator stack to the output
            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() is LeftParenthesisOperatorToken)
                {
                    // TODO: Handle unexpected left parenthesis operator, indicates mismatched parenthesis
                    throw new NotImplementedException();
                }

                PopOperatorStackToOutput();
            }

            return outputQueue.ToArray();
        }
    }
}
