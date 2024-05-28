namespace ReiCalcLib.Tokens
{
    /// <summary>
    /// Base class for all operators and functions in a math expression.
    /// </summary>
    public abstract class OperatorToken : Token
    {
        public abstract string ExpressionPattern { get; }

        public abstract int Precedence { get; }

        public abstract EAssociativity Associativity { get; }

        /// <summary>
        /// Performs the operation on the given input tokens and returns the numerical result.
        /// </summary>
        /// <param name="inputTokens">Array of input tokens to operate on. Inputs should generally be passed in ordered from left to right.</param>
        /// <returns><see cref="NumberToken"/> that holds the result of this operation. May be null if the operation failed.</returns>
        public abstract NumberToken Execute(params NumberToken[] inputTokens);
    }
}
