using Osero;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneController : MonoBehaviour, IPointerClickHandler
{
    public OseroController board;
    public Point point;

    public void OnPointerClick(PointerEventData eventData)
    {
        board.OnStoneClick(point);
    }
}
