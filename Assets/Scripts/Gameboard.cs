using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gameboard : MonoBehaviour, IGameboard {

    //Singleton
    public static Gameboard instance;

    //[SerializeField]
    private Tilemap tilemap;
    //Unity Serialised Fields
    #region GameSlots

    [SerializeField]
    private GameObject A1;
    [SerializeField]
    private GameObject A2;
    [SerializeField]
    private GameObject A3;
    [SerializeField]
    private GameObject A4;
    [SerializeField]
    private GameObject A5;
    [SerializeField]
    private GameObject A6;
    [SerializeField]
    private GameObject A7;
    [SerializeField]
    private GameObject A8;


    [SerializeField]
    private GameObject B1;
    [SerializeField]
    private GameObject B2;
    [SerializeField]
    private GameObject B3;
    [SerializeField]
    private GameObject B4;
    [SerializeField]
    private GameObject B5;
    [SerializeField]
    private GameObject B6;
    [SerializeField]
    private GameObject B7;
    [SerializeField]
    private GameObject B8;

    [SerializeField]
    private GameObject C1;
    [SerializeField]
    private GameObject C2;
    [SerializeField]
    private GameObject C3;
    [SerializeField]
    private GameObject C4;
    [SerializeField]
    private GameObject C5;
    [SerializeField]
    private GameObject C6;
    [SerializeField]
    private GameObject C7;
    [SerializeField]
    private GameObject C8;

    [SerializeField]
    private GameObject D1;
    [SerializeField]
    private GameObject D2;
    [SerializeField]
    private GameObject D3;
    [SerializeField]
    private GameObject D4;
    [SerializeField]
    private GameObject D5;
    [SerializeField]
    private GameObject D6;
    [SerializeField]
    private GameObject D7;
    [SerializeField]
    private GameObject D8;

    [SerializeField]
    private GameObject E1;
    [SerializeField]
    private GameObject E2;
    [SerializeField]
    private GameObject E3;
    [SerializeField]
    private GameObject E4;
    [SerializeField]
    private GameObject E5;
    [SerializeField]
    private GameObject E6;
    [SerializeField]
    private GameObject E7;
    [SerializeField]
    private GameObject E8;

    [SerializeField]
    private GameObject F1;
    [SerializeField]
    private GameObject F2;
    [SerializeField]
    private GameObject F3;
    [SerializeField]
    private GameObject F4;
    [SerializeField]
    private GameObject F5;
    [SerializeField]
    private GameObject F6;
    [SerializeField]
    private GameObject F7;
    [SerializeField]
    private GameObject F8;

    [SerializeField]
    private GameObject G1;
    [SerializeField]
    private GameObject G2;
    [SerializeField]
    private GameObject G3;
    [SerializeField]
    private GameObject G4;
    [SerializeField]
    private GameObject G5;
    [SerializeField]
    private GameObject G6;
    [SerializeField]
    private GameObject G7;
    [SerializeField]
    private GameObject G8;

    [SerializeField]
    private GameObject H1;
    [SerializeField]
    private GameObject H2;
    [SerializeField]
    private GameObject H3;
    [SerializeField]
    private GameObject H4;
    [SerializeField]
    private GameObject H5;
    [SerializeField]
    private GameObject H6;
    [SerializeField]
    private GameObject H7;
    [SerializeField]
    private GameObject H8;

    [SerializeField]
    private GameObject I1;
    [SerializeField]
    private GameObject I2;
    [SerializeField]
    private GameObject I3;
    [SerializeField]
    private GameObject I4;
    [SerializeField]
    private GameObject I5;
    [SerializeField]
    private GameObject I6;
    [SerializeField]
    private GameObject I7;
    [SerializeField]
    private GameObject I8;

    [SerializeField]
    private GameObject J1;
    [SerializeField]
    private GameObject J2;
    [SerializeField]
    private GameObject J3;
    [SerializeField]
    private GameObject J4;
    [SerializeField]
    private GameObject J5;
    [SerializeField]
    private GameObject J6;
    [SerializeField]
    private GameObject J7;
    [SerializeField]
    private GameObject J8;

    [SerializeField]
    private GameObject K1;
    [SerializeField]
    private GameObject K2;
    [SerializeField]
    private GameObject K3;
    [SerializeField]
    private GameObject K4;
    [SerializeField]
    private GameObject K5;
    [SerializeField]
    private GameObject K6;
    [SerializeField]
    private GameObject K7;
    [SerializeField]
    private GameObject K8;
    #endregion
    [SerializeField]
    private Camera gameCamera;

    //Interface Properties
    public int ColumnCount { get; set; } = 8;
    public int RowCount { get; set; } = 11;
    
    public Dictionary<Vector3Int, GameObject> SlotList { get; set; } = new Dictionary<Vector3Int, GameObject>();
    public List<Vector3Int> NeightbourList { get; private set; } = new List<Vector3Int>();
    public List<Item> GameItemList { get; private set; } = new List<Item>();
    public Dictionary<string, SlotSprite> LineUpList { get; private set; } = new Dictionary<string, SlotSprite>();

    //Constructor (For Singleton)
    Gameboard() {
        if(instance != null) {
            Debug.LogError("Cannot have more than 1 Gameboard");
            return;
        }
        instance = this;
    }

    void Start() {
        //Build Sprite Library
        DSpriteLibrary.instance.BuildDB();

        //Build GameObject Database
        BuildSlotObjectDatabase();

        //Start New Game
        RefreshGameBoard();
    }

    public Vector3Int GetSlot(Vector3 worldPositionVector3) {

        Vector2 worldPosition = new Vector2(worldPositionVector3.x, worldPositionVector3.y);

        RaycastHit2D hitInformation = Physics2D.Raycast(worldPosition, gameCamera.transform.forward);

        if (hitInformation.collider != null) {
            GameObject touchedObject = hitInformation.transform.gameObject;
            if(touchedObject.tag == "Slot") {
                return touchedObject.GetComponent<Slot>().SlotIndex;
            }
        }
        return new Vector3Int(999,999,999);
    }

    public Item GetSlotItem(Vector3Int slotLocation) {
        foreach (Item item in GameItemList) {
            if(item.ItemSlotLocation == slotLocation) {
                return item;
            }
        }
        return null;
    }

    public void SwitchSlotItems(Vector3Int slotA, Vector3Int slotB) {
        //Get Items
        Item itemA = GetSlotItem(slotA);
        Item itemB = GetSlotItem(slotB);

        //Switch Items Position
        if (itemA != null) {
            itemA.ItemSlotLocation = slotB;
            SlotList[slotB].GetComponent<Image>().sprite = itemA.ItemIcon;
        } else {
            SlotList[slotB].GetComponent<Image>().sprite = null;
        }
        if (itemB != null) {
            itemB.ItemSlotLocation = slotA;
            SlotList[slotA].GetComponent<Image>().sprite = itemB.ItemIcon;
        } else {
            SlotList[slotA].GetComponent<Image>().sprite = null;
        }
    }

    public void RefreshGameBoard() {

        //Clear Previous Item Lineup
        LineUpList.Clear();

        //Build New Item Lineup
        while (LineUpList.Count < RowCount) {
            SlotSprite ss = DSpriteLibrary.instance.GetRandomType();
            if (LineUpList.Count == 0) {
                LineUpList.Add(ss.spriteName, ss);
            } else {
                if (!LineUpList.ContainsKey(ss.spriteName)) {
                    LineUpList.Add(ss.spriteName, ss);
                }
            }
        }

        //Create a list of items based on the types generated
        BuildFreshGameItemList(LineUpList);

        //Shuffle the list to randomise the content
        ShuffledItemList(GameItemList);

        //Extract Slot Positions (to allow index interation)
        List<Vector3Int> slotIndexes = new List<Vector3Int>();
        foreach (Vector3Int slotPosition in SlotList.Keys) {
            slotIndexes.Add(slotPosition);
        }

        //Give all Items a slot position
        if (slotIndexes.Count != GameItemList.Count) {
            Debug.LogError("Amount of items doesn't match board slots.");
        } else {
            for (int i = 0; i < slotIndexes.Count; i++) {
                GameItemList[i].ItemSlotLocation = slotIndexes[i]; 
            }
        }

        //Set Gameboard Items to UI
        foreach(Item item in GameItemList) {
            if (!SlotList.ContainsKey(item.ItemSlotLocation)) {
                Debug.LogError("Item has incompatible slot location:" + item.ItemSlotLocation);
            } else {
                SlotList[item.ItemSlotLocation].GetComponent<Image>().sprite = item.ItemIcon;
            }
        }
    }

    private void Update() {
        GetMatchGroup();

        DropItems();
    }

    private void DestoryItems(List<Item> _deadList) { 
        foreach(Item item in _deadList) {
            if(GameItemList.Contains(item)) {
                GameItemList.Remove(item);
            }

            if (SlotList.ContainsKey(item.ItemSlotLocation)) {
                SlotList[item.ItemSlotLocation].GetComponent<Image>().sprite = null;
            }
        }
    }

    private void DropItems() {
        foreach (Item item in GameItemList) {
            Vector3Int underPosition = new Vector3Int(item.ItemSlotLocation.x, item.ItemSlotLocation.y + 1, 0);

            if (IsInBoardSlotSize(underPosition) && GetSlotItem(underPosition) == null) {
                SlotList[item.ItemSlotLocation].GetComponent<Image>().sprite = null;
                item.ItemSlotLocation = underPosition;
                SlotList[item.ItemSlotLocation].GetComponent<Image>().sprite = item.ItemIcon;
            }
        }
    }

    #region Matching Logic

    private void GetMatchGroup() {
        List<Item> matchList = new List<Item>();

        foreach (Item item in GameItemList) {
            GetMatchRight(matchList, item.ItemSlotLocation, item.ItemType);
            GetMatchLeft(matchList, item.ItemSlotLocation, item.ItemType);
            GetMatchUp(matchList, item.ItemSlotLocation, item.ItemType);
            GetMatchDown(matchList, item.ItemSlotLocation, item.ItemType);
            matchList = matchList.Distinct().ToList();
            if(matchList.Count > 2) {
                DestoryItems(matchList);
                matchList.Clear();
                break;
            } else {
                matchList.Clear();
            }
        }
    }

    private List<Item> GetMatchRight (List<Item> _list, Vector3Int from, ItemType itemType) {
        
        //From locations item
        Item thisItem = GetSlotItem(from);
        if(thisItem == null)
            return _list;

        if (thisItem.ItemType == itemType) {

            //Add this item to the list as it matches.
            if(!_list.Contains(thisItem))
                _list.Add(thisItem);

            //Build neighbour slots
            Vector3Int Right = new Vector3Int(from.x + 1, from.y, 0);
            if (IsInBoardSlotSize(Right)) {
                GetMatchRight(_list, Right, itemType);
            }

        } else {
            return _list;
        }
        return _list;
    }

    private List<Item> GetMatchLeft(List<Item> _list, Vector3Int from, ItemType itemType) {

        //From locations item
        Item thisItem = GetSlotItem(from);
        if (thisItem == null)
            return _list;

        if (thisItem.ItemType == itemType) {

            //Add this item to the list as it matches.
            if (!_list.Contains(thisItem))
                _list.Add(thisItem);

            //Build neighbour slots
            Vector3Int Left = new Vector3Int(from.x - 1, from.y, 0);
            if (IsInBoardSlotSize(Left)) {
                GetMatchLeft(_list, Left, itemType);
            }

        } else {
            return _list;
        }
        return _list;
    }

    private List<Item> GetMatchUp(List<Item> _list, Vector3Int from, ItemType itemType) {

        //From locations item
        Item thisItem = GetSlotItem(from);
        if (thisItem == null)
            return _list;

        if (thisItem.ItemType == itemType) {

            //Add this item to the list as it matches.
            if (!_list.Contains(thisItem))
                _list.Add(thisItem);

            //Build neighbour slots
            Vector3Int Up = new Vector3Int(from.x, from.y - 1, 0);
            if (IsInBoardSlotSize(Up)) {
                GetMatchLeft(_list, Up, itemType);
            }

        } else {
            return _list;
        }
        return _list;
    }

    private List<Item> GetMatchDown(List<Item> _list, Vector3Int from, ItemType itemType) {

        //From locations item
        Item thisItem = GetSlotItem(from);
        if (thisItem == null)
            return _list;

        if (thisItem.ItemType == itemType) {

            //Add this item to the list as it matches.
            if (!_list.Contains(thisItem))
                _list.Add(thisItem);

            //Build neighbour slots
            Vector3Int Down = new Vector3Int(from.x, from.y + 1, 0);
            if (IsInBoardSlotSize(Down)) {
                GetMatchLeft(_list, Down, itemType);
            }

        } else {
            return _list;
        }
        return _list;
    }

    #endregion 

    private void BuildFreshGameItemList(Dictionary<string, SlotSprite> slotTypes) {
        GameItemList.Clear();
        foreach(SlotSprite ss in slotTypes.Values) {
            for (int i = 0; i < ColumnCount; i++) {
                Item item = new Item();
                item.ItemIcon = ss.sprite;
                item.ItemName = ss.spriteName + i;
                item.ItemTile.sprite = item.ItemIcon;
                switch (ss.spriteName) {
                    case "Bones":
                        item.ItemType = ItemType.Bones;
                        break;
                    case "Book":
                        item.ItemType = ItemType.Book;
                        break;
                    case "Cake":
                        item.ItemType = ItemType.Cake;
                        break;
                    case "Chest":
                        item.ItemType = ItemType.Chest;
                        break;
                    case "Compass":
                        item.ItemType = ItemType.Compass;
                        break;
                    case "Gem":
                        item.ItemType = ItemType.Gem;
                        break;
                    case "Heart":
                        item.ItemType = ItemType.Heart;
                        break;
                    case "Hourglass":
                        item.ItemType = ItemType.Hourglass;
                        break;
                    case "Key":
                        item.ItemType = ItemType.Key;
                        break;
                    case "Pendant":
                        item.ItemType = ItemType.Pendant;
                        break;
                    case "Potion":
                        item.ItemType = ItemType.Potion;
                        break;
                    case "Present":
                        item.ItemType = ItemType.Present;
                        break;
                    case "Scroll":
                        item.ItemType = ItemType.Scroll;
                        break;
                    case "Star":
                        item.ItemType = ItemType.Star;
                        break;
                    default:
                        Debug.LogError("Unknown item name/key: " + ss.spriteName);
                        break;
                }

                GameItemList.Add(item);
            }
        }
    }

    private void ShuffledItemList(List<Item> resolvedPuzzleList) {
        List<Item> shuffledList = resolvedPuzzleList.OrderBy(x => Random.value).ToList();
        GameItemList = shuffledList;
    }

    public void BuildNeighbours(Vector3Int centralItem) {
        List<Vector3Int> neighbours = new List<Vector3Int>();

        Vector3Int OnRight = new Vector3Int(centralItem.x + 1, centralItem.y, 0);
        if(IsInBoardSlotSize(OnRight))
            neighbours.Add(OnRight);

        Vector3Int OnLeft = new Vector3Int(centralItem.x - 1, centralItem.y, 0);
        if (IsInBoardSlotSize(OnLeft))
            neighbours.Add(OnLeft);

        Vector3Int OnTop = new Vector3Int(centralItem.x, centralItem.y + 1, 0);
        if (IsInBoardSlotSize(OnTop))
            neighbours.Add(OnTop);

        Vector3Int OnBottom = new Vector3Int(centralItem.x, centralItem.y - 1, 0);
        if (IsInBoardSlotSize(OnBottom))
            neighbours.Add(OnBottom);

        Vector3Int TopRight = new Vector3Int(centralItem.x + 1, centralItem.y + 1, 0);
        if (IsInBoardSlotSize(TopRight))
            neighbours.Add(TopRight);

        Vector3Int BottomLeft = new Vector3Int(centralItem.x - 1, centralItem.y - 1, 0);
        if (IsInBoardSlotSize(BottomLeft))
            neighbours.Add(BottomLeft);

        Vector3Int TopLeft = new Vector3Int(centralItem.x - 1, centralItem.y + 1, 0);
        if (IsInBoardSlotSize(TopLeft))
            neighbours.Add(TopLeft);

        Vector3Int BottomRight = new Vector3Int(centralItem.x + 1, centralItem.y - 1, 0);
        if (IsInBoardSlotSize(BottomRight))
            neighbours.Add(BottomRight);

        NeightbourList = neighbours;
    }

    public bool IsInBoardSlotSize(Vector3Int value) {
        if (value == new Vector3Int(999, 999, 999))
            return false;

        if (value.x <= ColumnCount - 1 &&
            value.x > -1 &&
            value.y <= RowCount - 1 &&
            value.y > -1)
            return true;
        else
            return false;
    }

    private void BuildSlotObjectDatabase() {
        SlotList.Add(A1.GetComponent<Slot>().SlotIndex, A1);
        SlotList.Add(A2.GetComponent<Slot>().SlotIndex, A2);
        SlotList.Add(A3.GetComponent<Slot>().SlotIndex, A3);
        SlotList.Add(A4.GetComponent<Slot>().SlotIndex, A4);
        SlotList.Add(A5.GetComponent<Slot>().SlotIndex, A5);
        SlotList.Add(A6.GetComponent<Slot>().SlotIndex, A6);
        SlotList.Add(A7.GetComponent<Slot>().SlotIndex, A7);
        SlotList.Add(A8.GetComponent<Slot>().SlotIndex, A8);

        SlotList.Add(B1.GetComponent<Slot>().SlotIndex, B1);
        SlotList.Add(B2.GetComponent<Slot>().SlotIndex, B2);
        SlotList.Add(B3.GetComponent<Slot>().SlotIndex, B3);
        SlotList.Add(B4.GetComponent<Slot>().SlotIndex, B4);
        SlotList.Add(B5.GetComponent<Slot>().SlotIndex, B5);
        SlotList.Add(B6.GetComponent<Slot>().SlotIndex, B6);
        SlotList.Add(B7.GetComponent<Slot>().SlotIndex, B7);
        SlotList.Add(B8.GetComponent<Slot>().SlotIndex, B8);

        SlotList.Add(C1.GetComponent<Slot>().SlotIndex, C1);
        SlotList.Add(C2.GetComponent<Slot>().SlotIndex, C2);
        SlotList.Add(C3.GetComponent<Slot>().SlotIndex, C3);
        SlotList.Add(C4.GetComponent<Slot>().SlotIndex, C4);
        SlotList.Add(C5.GetComponent<Slot>().SlotIndex, C5);
        SlotList.Add(C6.GetComponent<Slot>().SlotIndex, C6);
        SlotList.Add(C7.GetComponent<Slot>().SlotIndex, C7);
        SlotList.Add(C8.GetComponent<Slot>().SlotIndex, C8);

        SlotList.Add(D1.GetComponent<Slot>().SlotIndex, D1);
        SlotList.Add(D2.GetComponent<Slot>().SlotIndex, D2);
        SlotList.Add(D3.GetComponent<Slot>().SlotIndex, D3);
        SlotList.Add(D4.GetComponent<Slot>().SlotIndex, D4);
        SlotList.Add(D5.GetComponent<Slot>().SlotIndex, D5);
        SlotList.Add(D6.GetComponent<Slot>().SlotIndex, D6);
        SlotList.Add(D7.GetComponent<Slot>().SlotIndex, D7);
        SlotList.Add(D8.GetComponent<Slot>().SlotIndex, D8);

        SlotList.Add(E1.GetComponent<Slot>().SlotIndex, E1);
        SlotList.Add(E2.GetComponent<Slot>().SlotIndex, E2);
        SlotList.Add(E3.GetComponent<Slot>().SlotIndex, E3);
        SlotList.Add(E4.GetComponent<Slot>().SlotIndex, E4);
        SlotList.Add(E5.GetComponent<Slot>().SlotIndex, E5);
        SlotList.Add(E6.GetComponent<Slot>().SlotIndex, E6);
        SlotList.Add(E7.GetComponent<Slot>().SlotIndex, E7);
        SlotList.Add(E8.GetComponent<Slot>().SlotIndex, E8);

        SlotList.Add(F1.GetComponent<Slot>().SlotIndex, F1);
        SlotList.Add(F2.GetComponent<Slot>().SlotIndex, F2);
        SlotList.Add(F3.GetComponent<Slot>().SlotIndex, F3);
        SlotList.Add(F4.GetComponent<Slot>().SlotIndex, F4);
        SlotList.Add(F5.GetComponent<Slot>().SlotIndex, F5);
        SlotList.Add(F6.GetComponent<Slot>().SlotIndex, F6);
        SlotList.Add(F7.GetComponent<Slot>().SlotIndex, F7);
        SlotList.Add(F8.GetComponent<Slot>().SlotIndex, F8);

        SlotList.Add(G1.GetComponent<Slot>().SlotIndex, G1);
        SlotList.Add(G2.GetComponent<Slot>().SlotIndex, G2);
        SlotList.Add(G3.GetComponent<Slot>().SlotIndex, G3);
        SlotList.Add(G4.GetComponent<Slot>().SlotIndex, G4);
        SlotList.Add(G5.GetComponent<Slot>().SlotIndex, G5);
        SlotList.Add(G6.GetComponent<Slot>().SlotIndex, G6);
        SlotList.Add(G7.GetComponent<Slot>().SlotIndex, G7);
        SlotList.Add(G8.GetComponent<Slot>().SlotIndex, G8);

        SlotList.Add(H1.GetComponent<Slot>().SlotIndex, H1);
        SlotList.Add(H2.GetComponent<Slot>().SlotIndex, H2);
        SlotList.Add(H3.GetComponent<Slot>().SlotIndex, H3);
        SlotList.Add(H4.GetComponent<Slot>().SlotIndex, H4);
        SlotList.Add(H5.GetComponent<Slot>().SlotIndex, H5);
        SlotList.Add(H6.GetComponent<Slot>().SlotIndex, H6);
        SlotList.Add(H7.GetComponent<Slot>().SlotIndex, H7);
        SlotList.Add(H8.GetComponent<Slot>().SlotIndex, H8);

        SlotList.Add(I1.GetComponent<Slot>().SlotIndex, I1);
        SlotList.Add(I2.GetComponent<Slot>().SlotIndex, I2);
        SlotList.Add(I3.GetComponent<Slot>().SlotIndex, I3);
        SlotList.Add(I4.GetComponent<Slot>().SlotIndex, I4);
        SlotList.Add(I5.GetComponent<Slot>().SlotIndex, I5);
        SlotList.Add(I6.GetComponent<Slot>().SlotIndex, I6);
        SlotList.Add(I7.GetComponent<Slot>().SlotIndex, I7);
        SlotList.Add(I8.GetComponent<Slot>().SlotIndex, I8);

        SlotList.Add(J1.GetComponent<Slot>().SlotIndex, J1);
        SlotList.Add(J2.GetComponent<Slot>().SlotIndex, J2);
        SlotList.Add(J3.GetComponent<Slot>().SlotIndex, J3);
        SlotList.Add(J4.GetComponent<Slot>().SlotIndex, J4);
        SlotList.Add(J5.GetComponent<Slot>().SlotIndex, J5);
        SlotList.Add(J6.GetComponent<Slot>().SlotIndex, J6);
        SlotList.Add(J7.GetComponent<Slot>().SlotIndex, J7);
        SlotList.Add(J8.GetComponent<Slot>().SlotIndex, J8);

        SlotList.Add(K1.GetComponent<Slot>().SlotIndex, K1);
        SlotList.Add(K2.GetComponent<Slot>().SlotIndex, K2);
        SlotList.Add(K3.GetComponent<Slot>().SlotIndex, K3);
        SlotList.Add(K4.GetComponent<Slot>().SlotIndex, K4);
        SlotList.Add(K5.GetComponent<Slot>().SlotIndex, K5);
        SlotList.Add(K6.GetComponent<Slot>().SlotIndex, K6);
        SlotList.Add(K7.GetComponent<Slot>().SlotIndex, K7);
        SlotList.Add(K8.GetComponent<Slot>().SlotIndex, K8);
    }
}
