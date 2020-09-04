using UnityEngine;

public interface IGameUI
{
    Vector3 TouchPosition { get; set; }
    Vector3Int TouchSlot { get; set; }
    Item TouchItem { get; set; }
    Vector3 DragPosition { get; set; }
    Vector3Int DragSlot { get; set; }
    Item DragItem { get; set; }
}
