using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiCalcLib.Tokens.Operators
{
    public class PowerOperatorToken : OperatorToken
    {
        public override string ExpressionPattern => "^";

        public override int Precedence => 4;

        public override EAssociativity Associativity => EAssociativity.Right;
    }
}
