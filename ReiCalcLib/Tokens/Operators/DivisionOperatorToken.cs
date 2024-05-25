using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiCalcLib.Tokens.Operators
{
    public class DivisionOperatorToken : OperatorToken
    {
        public override string ExpressionPattern => "/";

        public override int Precedence => 3;

        public override EAssociativity Associativity => EAssociativity.Left;
    }
}
