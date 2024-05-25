using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiCalcLib.Tokens.Operators
{
    public abstract class ParenthesisOperatorToken : OperatorToken
    {
        public override int Precedence => 5;

        public override EAssociativity Associativity => EAssociativity.Left;
    }
}
