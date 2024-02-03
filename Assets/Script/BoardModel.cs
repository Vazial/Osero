using System;
using System.Drawing;

namespace Osero
{
    /// <summary>
    /// オセロ盤
    /// </summary>
    public class BoardModel
    {
        private Point[] directions = new Point[]
        {
            new Point(0, 1),    // front
            new Point(0, -1),   // back
            new Point(1, 0),    // right
            new Point(-1, 0),   // left
            new Point(1, 1),    // front right
            new Point(-1, 1),   // front left
            new Point(1, -1),   // back  right
            new Point(-1, -1),  // back left
        };

        public StoneModel[,] Cell;
        private int Length;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Length">マス目の数</param>
        /// <exception cref="ArgumentException">偶数でない場合はエラー</exception>
        public BoardModel(int Length = 8)
        {
            // 盤面を生成して石を初期配置
            this.Length = Length;
            if ((Length % 2 != 0) || (Length <= 2)) { throw new ArgumentException("引数には偶数を指定してください"); };

            Cell = new StoneModel[Length, Length];
            var center = Length / 2;

            Cell[center, center] = new StoneModel(FrontBack.White);
            Cell[center - 1, center - 1] = new StoneModel(FrontBack.White);
            Cell[center - 1, center] = new StoneModel(FrontBack.Black);
            Cell[center, center - 1] = new StoneModel(FrontBack.Black);
        }

        public Nullable<FrontBack> GetFrontBack(Point xy)
        {
            return Cell[xy.X, xy.Y]?.frontback;
        }

        /// <summary>
        /// 指定のマスに石を配置できるかどうか(ひっくり返せる石がない場合はfalse)
        /// </summary>
        /// <param name="xy">配置する座標</param>
        /// <param name="frontback">石の向き</param>
        /// <returns>配置可否</returns>
        public bool CanPut(Point xy, FrontBack frontback)
        {
            if (Cell[xy.X, xy.Y] != null)
            {
                return false;
            }
            var copyCell = new StoneModel[Length, Length];
            for (int yy = 0; yy < Length; ++yy)
            {
                for (int xx = 0; xx < Length; ++xx)
                {
                    copyCell[xx, yy] = Cell[xx, yy]?.Clone();
                }
            }

            var result = Put(xy, frontback);
            Cell = copyCell;
            return result;
        }

        /// <summary>
        /// 石を配置する
        /// </summary>
        /// <param name="xy">配置する座標</param>
        /// <param name="frontback">石の向き</param>
        /// <returns>ひっくり返した石があるかどうか</returns>
        public bool Put(Point xy, FrontBack frontback)
        {
            Cell[xy.X, xy.Y] = new StoneModel(frontback);
            return CheckReverse(xy, frontback);
        }

        private bool CheckReverse(Point xy, FrontBack frontback)
        {
            bool result = false;
            foreach (var direction in directions)
            {
                var IsReverse = Reverse(xy, frontback, direction);
                if (IsReverse) { result = true; }
            }
            return result;
        }

        private bool Reverse(Point xy, FrontBack frontback, Point direction, bool isReverse = false)
        {
            var newPoint = new Point(xy.X + direction.X, xy.Y + direction.Y);

            // 盤外チェック
            if ((newPoint.X < 0) || (newPoint.Y < 0) || (newPoint.X >= Length) || (newPoint.Y >= Length))
            {
                return false;
            }

            // 違う向きの石があれば、その先を確認(その先で同じ向きの石があれば、間の石をひっくり返す)
            if ((Cell[newPoint.X, newPoint.Y] != null) && (Cell[newPoint.X, newPoint.Y].frontback != frontback))
            {
                isReverse = Reverse(newPoint, frontback, direction, true);
                if (isReverse) { Cell[newPoint.X, newPoint.Y].Reverse(); }
                return isReverse;
            }
            else
            {
                // 違う向きの石があり、その先で同じ向きの石があった場合
                if ((isReverse != false) && (Cell[newPoint.X, newPoint.Y]?.frontback == frontback))
                {
                    // これまでの石をひっくり返す
                    return true;
                }
                else
                {
                    // 違う向きの石がなかった場合、もしくはその先で同じ向きの石がなかった場合、ひっくり返さない
                    return false;
                }
            }
        }

        /// <summary>
        /// コンソール表示
        /// </summary>
        public void DebugPrint()
        {
            for (int yy = 0; yy < Length; ++yy)
            {
                for (int xx = 0; xx < Length; ++xx)
                {
                    Console.Write((Cell[xx, yy] != null) ? Cell[xx, yy].frontback.ToString() : "     ");
                    Console.Write(", ");
                }
                Console.WriteLine();
            }
        }
    }
}