using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Osero
{
    public class OseroController : MonoBehaviour
    {
        [SerializeField]
        private GameObject stoneObject;
        [SerializeField]
        private GameObject userTextObject;
        private Text userText;

        private GameObject[,] StoneObjects;
        private readonly int BoardLength = 8;
        private BoardModel board;
        private FrontBack frontback = FrontBack.Black;

        public OseroController()
        {
            board = new BoardModel(BoardLength);
        }

        public void Start()
        {
            userText = userTextObject.GetComponent<UnityEngine.UI.Text>();

            StoneObjects = new GameObject[BoardLength, BoardLength];

            for (int xx = 0; xx < BoardLength; xx++)
            {
                for (int yy = 0; yy < BoardLength; yy++)
                {
                    var posY = (BoardLength - yy - 1);
                    StoneObjects[xx, posY] = Instantiate(stoneObject, new Vector3((xx - 3.5f), (yy - 3.5f), 0.0f), Quaternion.identity);
                    var script = StoneObjects[xx, posY].GetComponent<StoneController>();
                    script.point = new Point(xx, posY);
                    script.board = this;
                }
            }
        }

        public void Update()
        {
            int count = 0;
            for (int xx = 0; xx < BoardLength; xx++)
            {
                for (int yy = 0; yy < BoardLength; yy++)
                {
                    var frontback = board.GetFrontBack(new Point(xx, yy));
                    var exists = !(frontback is null);

                    var renderer = StoneObjects[xx, yy].GetComponent<Renderer>();
                    renderer.enabled = exists;
                    if (exists)
                    {
                        ++count;
                        switch(frontback)
                        {
                            default:
                                renderer.material.color = UnityEngine.Color.black;
                                break;
                            case FrontBack.White:
                                renderer.material.color = UnityEngine.Color.white;
                                break;
                        }
                    }
                }
            }
        }

        public void OnStoneClick(Point point)
        {
            if (board.CanPut(point, frontback))
            {
                board.Put(point, frontback);
                frontback = (frontback == FrontBack.White) ? FrontBack.Black : FrontBack.White;
                userText.text = (frontback == FrontBack.White) ? "白の手番です。" : "黒の手番です。";
            }
        }

        public void OnPassed()
        {
            frontback = (frontback == FrontBack.White) ? FrontBack.Black : FrontBack.White;
            userText.text = (frontback == FrontBack.White) ? "白の手番です。" : "黒の手番です。";
        }
    }
}