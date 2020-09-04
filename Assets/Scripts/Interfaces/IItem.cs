using UnityEngine;
using UnityEngine.Tilemaps;

public interface IItem
{
    ItemType ItemType { get; set; }
    string ItemName { get; set; }
    Sprite ItemIcon { get; set; }
    Tile ItemTile { get; set; }
    Vector3Int ItemSlotLocation { get; set; }
}

public enum ItemType {
    Bones,
    Book,
    Cake, 
    Chest,
    Compass,
    Gem,
    Heart,
    Hourglass,
    Key,
    Pendant,
    Potion,
    Present,
    Scroll,
    Star,
    NOTSET
}