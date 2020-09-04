using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SlotSprite {
    public Sprite sprite;
    public string spriteName;
}

public class DSpriteLibrary : MonoBehaviour
{
    //Unity Inspector Values
    [SerializeField]
    private SlotSprite spriteBones;
    [SerializeField]
    private SlotSprite spriteBookBlue;
    [SerializeField]
    private SlotSprite spriteCake;
    [SerializeField]
    private SlotSprite spriteChest;
    [SerializeField]
    private SlotSprite spriteCompass;
    [SerializeField]
    private SlotSprite spriteGemGreen;
    [SerializeField]
    private SlotSprite spriteHeart;
    [SerializeField]
    private SlotSprite spriteHourglass;
    [SerializeField]
    private SlotSprite spriteKey;
    [SerializeField]
    private SlotSprite spritePendant;
    [SerializeField]
    private SlotSprite spritePotionRed;
    [SerializeField]
    private SlotSprite spritePresent;
    [SerializeField]
    private SlotSprite spriteScrollRoll;
    [SerializeField]
    private SlotSprite spriteStar;

    //Singleton
    public static DSpriteLibrary instance;

    //Sprite Library
    public Dictionary<int, SlotSprite> Library { get; private set; } = new Dictionary<int, SlotSprite>();
    DSpriteLibrary() {
        if(instance != null) {
            Debug.LogError("Cannot have more than 1 Sprite Library");
            return;
        }
        instance = this;
    }

    public void BuildDB() {
        Library.Add(1, spriteBones);
        Library.Add(2, spriteBookBlue);
        Library.Add(3, spriteCake);
        Library.Add(4, spriteChest);
        Library.Add(5, spriteCompass);
        Library.Add(6, spriteGemGreen);
        Library.Add(7, spriteHeart);
        Library.Add(8, spriteHourglass);
        Library.Add(9, spriteKey);
        Library.Add(10, spritePendant);
        Library.Add(11, spritePotionRed);
        Library.Add(12, spritePresent);
        Library.Add(13, spriteScrollRoll);
        Library.Add(14, spriteStar);
    }

    public SlotSprite GetRandomType() {

        int rand = Random.Range(1, Library.Count);
        SlotSprite ss = Library[rand];
        return ss;
    }
}
