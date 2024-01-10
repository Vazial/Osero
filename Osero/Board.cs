using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osero
{
    public class Board
    {
        class Box
        {
            public Stone stone { get; set;}
        }
        Box[,] area;

        public Board(int length = 8)
        {
            if (length % 2 != 0)
            {
                throw new ArgumentException("length must be an even number.");
            }
            area = new Box[length, length];

            /*オセロの初期石を置く処理*/
            area[length % 2, length % 2].stone = new Stone(true);
            area[length % 2+1, length % 2].stone = new Stone(false);
            area[length % 2, length % 2+1].stone = new Stone(false);
            area[length % 2+1, length % 2+1].stone = new Stone(true);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(area[i, j].stone + ",");
                }
                Console.WriteLine("");
            }
        }


    }
}
