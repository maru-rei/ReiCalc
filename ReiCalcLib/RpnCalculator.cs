using ReiCalcLib.Tokens;

namespace ReiCalcLib
{
    public class RpnCalculator
    {
        /// <summary>
        /// Solves an expression that is supplied in reverse Polish notation and returns the result.
        /// </summary>
        /// <param name="rpnTokens">The array of tokens that represents the math expression in reverse Polish notation.</param>
        /// <returns>Result of the calculation.</returns>
        public double Calculate(Token[] rpnTokens)
        {
            Stack<Token> stack = new Stack<Token>();
            Queue<Token> inputQueue = new Queue<Token>(rpnTokens);

            while (inputQueue.Count > 0)
            {
                if (inputQueue.Peek() is NumberToken)
                {
                    stack.Push(inputQueue.Dequeue());
                }
                else
                {
                    OperatorToken operatorToken = inputQueue.Dequeue() as OperatorToken;
                    // TODO: Add support for operators that take more or less than two number tokens (e.g. sin)
                    // Assuming the current set of operators all take two number tokens for now
                    NumberToken rValue = stack.Pop() as NumberToken;
                    NumberToken lValue = stack.Pop() as NumberToken;

                    stack.Push(operatorToken.Execute(lValue, rValue));
                }
            }

            return (stack.Pop() as NumberToken).Value;
        }
    }
}
