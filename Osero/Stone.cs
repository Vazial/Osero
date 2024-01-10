using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osero
{
    public class Stone
    {
        public bool isWhite { get; private set; }
        public Stone(bool isWhite)
        {
            this.isWhite = isWhite;
        }
    }
}
