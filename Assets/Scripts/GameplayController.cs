using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.WSA;

public class GameplayController : MonoBehaviour
{
    public Sprite spriteBones;
    public Sprite spriteBookBlue;
    public Sprite spriteBookGreen;
    public Sprite spriteBookRed;
    public Sprite spriteCake;
    public Sprite spriteChest;
    public Sprite spriteCompass;
    public Sprite spriteGemBlue;
    public Sprite spriteGemGreen;
    public Sprite spriteGemRed;
    public Sprite spriteHeart;
    public Sprite spriteHourglass;
    public Sprite spriteKey;
    public Sprite spritePendant;
    public Sprite spritePotionPurple;
    public Sprite spritePotionRed;
    public Sprite spritePresent;
    public Sprite spriteScrollOpen;
    public Sprite spriteScrollRoll;
    public Sprite spriteStar;

    private Dictionary<int, Sprite> spriteLibrary = new Dictionary<int, Sprite>();

    public Tilemap gameBoard;

    public GameObject prefabBrick;

    private Vector3 clickPosition;
    private Tile clickTile;
    private Vector3Int clickSlot;
    private List<Vector3Int> neightbourSlots;
    private Vector3 dragPosition;
    private Tile dragTile;
    private Vector3Int dragSlot;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    GameObject dragObject;

    // Start is called before the first frame update
    void Start()
    {
        neightbourSlots = new List<Vector3Int>();
        clickTile = new Tile();
        dragTile = new Tile();
        dragObject = Instantiate(prefabBrick);
        dragObject.SetActive(false);


        spriteLibrary.Add(0, spriteBones);
        spriteLibrary.Add(1, spriteBookBlue);
        spriteLibrary.Add(2, spriteBookGreen);
        spriteLibrary.Add(3, spriteBookRed);
        spriteLibrary.Add(4, spriteCake);
        spriteLibrary.Add(5, spriteChest);
        spriteLibrary.Add(6, spriteCompass);
        spriteLibrary.Add(7, spriteGemBlue);
        spriteLibrary.Add(8, spriteGemGreen);
        spriteLibrary.Add(9, spriteGemRed);
        spriteLibrary.Add(10, spriteHeart);
        spriteLibrary.Add(11, spriteHourglass);
        spriteLibrary.Add(12, spriteKey);
        spriteLibrary.Add(13, spritePendant);
        spriteLibrary.Add(14, spritePotionPurple);
        spriteLibrary.Add(15, spritePotionRed);
        spriteLibrary.Add(16, spritePresent);
        spriteLibrary.Add(17, spriteScrollOpen);
        spriteLibrary.Add(18, spriteScrollRoll);
        spriteLibrary.Add(19, spriteStar);
   
        for (int i = 0; i < 6; i++) {
            for (int t = 0; t < 8; t++) {
                Tile tile = new Tile();
                tile.name = i.ToString() + t.ToString();
                tile.sprite = GetRandomSprite();
                gameBoard.SetTile(new Vector3Int(i, -t, 0), tile);
            }
        }
    }

    private Sprite GetRandomSprite() {
        int rand = Random.Range(0, spriteLibrary.Count);
        return spriteLibrary[rand];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            clickPosition = Input.mousePosition;
            Vector3 clickPositionWorld = Camera.main.ScreenToWorldPoint(clickPosition);
            clickSlot = gameBoard.WorldToCell(clickPositionWorld);
            text1.text = clickSlot.ToString();
            clickTile = gameBoard.GetTile(gameBoard.WorldToCell(clickPositionWorld)) as Tile;
            gameBoard.SetTile(clickSlot, null);
            neightbourSlots.Clear();
            neightbourSlots.Add(new Vector3Int(clickSlot.x + 1, clickSlot.y, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x - 1, clickSlot.y, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x, clickSlot.y + 1, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x, clickSlot.y - 1, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x + 1, clickSlot.y + 1, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x - 1, clickSlot.y - 1, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x - 1, clickSlot.y + 1, 0));
            neightbourSlots.Add(new Vector3Int(clickSlot.x + 1, clickSlot.y - 1, 0));

            foreach (Vector3Int vector in neightbourSlots) {
                gameBoard.SetTileFlags(vector, TileFlags.None);
                gameBoard.SetColor(vector, Color.red);
                    }
        }

        if (Input.GetMouseButton(0)) {
            dragPosition = Input.mousePosition;
            Vector3 dragPositionWorld = Camera.main.ScreenToWorldPoint(dragPosition);
            dragSlot = gameBoard.WorldToCell(dragPositionWorld);
            dragTile = gameBoard.GetTile(gameBoard.WorldToCell(dragPositionWorld)) as Tile;
            text2.text = gameBoard.WorldToCell(dragPositionWorld).ToString();
            text3.text = dragTile.name;
            dragObject.SetActive(true);
            dragObject.GetComponent<SpriteRenderer>().sprite = clickTile.sprite;
            gameBoard.SetTile(clickSlot, dragTile);
            dragPositionWorld.z = -1;
            dragObject.transform.position = dragPositionWorld;
        }

        if (Input.GetMouseButtonUp(0)) {

            foreach (Vector3Int vector in neightbourSlots) {
                gameBoard.SetColor(vector, Color.white);
            }

            if (clickTile.sprite != null && dragTile.sprite != null && clickTile.name != dragTile.name) {
                int xClick = clickSlot.x;
                int yClick = clickSlot.y;
                if ((dragSlot.x == xClick + 1 || dragSlot.x == xClick - 1 || dragSlot.x == xClick)
                    &&
                    (dragSlot.y == yClick + 1 || dragSlot.y == yClick - 1 || dragSlot.y == yClick)) {
                    gameBoard.SetTile(clickSlot, dragTile);
                    gameBoard.SetTile(dragSlot, clickTile);
                } else {
                    gameBoard.SetTile(clickSlot, clickTile);
                    gameBoard.SetTile(dragSlot, dragTile);
                }
            } 

            clickPosition = Vector3.zero;
            clickSlot = Vector3Int.zero;
            clickTile = new Tile();
            dragPosition = Vector3.zero;
            dragSlot = Vector3Int.zero;
            dragTile = new Tile();
            dragObject.SetActive(false);

        }

        

    }
}
