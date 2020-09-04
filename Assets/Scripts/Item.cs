using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Item : IItem
{
    public string ItemName { get; set; } = "default name";

    public Sprite ItemIcon { get; set; }

    public ItemType ItemType { get; set; } = ItemType.NOTSET;

    public Tile ItemTile { get; set; } = new Tile();
    public Vector3Int ItemSlotLocation { get; set; } = new Vector3Int(0, 0, 0);
}
