using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiCalcLib.Tokens.Operators
{
    public class RightParenthesisOperatorToken : ParenthesisOperatorToken
    {
        public override string ExpressionPattern => ")";
    }
}
