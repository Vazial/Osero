namespace Osero
{
    /// <summary>
    /// 向きの色
    /// </summary>
    public enum FrontBack
    {
        White = 0,
        Black = 1,
    }
    /// <summary>
    /// オセロの石
    /// </summary>
    public class StoneModel
    {
        /// <summary>
        /// 向き
        /// </summary>
        public FrontBack frontback { get; private set; }

        public StoneModel(FrontBack frontback)
        {
            this.frontback = frontback;
        }   

        /// <summary>
        /// 石をひっくり返す
        /// </summary>
        public void Reverse()
        {
            frontback = (frontback == FrontBack.White) ? FrontBack.Black : FrontBack.White;
        }
    }
}
