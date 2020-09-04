using UnityEngine;
using System.Collections.Generic;

public interface IGameboard {
    int ColumnCount { get; set; }
    int RowCount { get; set; }
    Dictionary<Vector3Int, GameObject> SlotList { get; }
    List<Vector3Int> NeightbourList { get; }
    List<Item> GameItemList { get; }
    Dictionary<string, SlotSprite> LineUpList { get; }
    Vector3Int GetSlot(Vector3 worldPositionVector3);
    void BuildNeighbours(Vector3Int centralItem);
    void SwitchSlotItems(Vector3Int slotA, Vector3Int slotB);
    Item GetSlotItem(Vector3Int slotLocation);
    void RefreshGameBoard();
}