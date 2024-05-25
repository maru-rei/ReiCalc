using ReiCalcLib.Tokens;
using ReiCalcLib.Tokens.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiCalcLib
{
    // Implementation based on https://en.wikipedia.org/wiki/Shunting_yard_algorithm

    public class ShuntingYard
    {
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
