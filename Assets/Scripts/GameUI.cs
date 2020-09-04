using UnityEngine.Tilemaps;
using UnityEngine;

public class GameUI : MonoBehaviour, IGameUI
{
    //Unity Inspector Variables
    [SerializeField]
    private Camera gameCamera;
    [SerializeField]
    private GameObject dragItem;

    public Vector3 TouchPosition { get; set; }
    public Vector3Int TouchSlot { get; set; }
    public Item TouchItem { get; set; }
    public Vector3 DragPosition { get; set; }
    public Vector3Int DragSlot { get; set; }
    public Item DragItem { get; set; }

    private void Start() {
        dragItem.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //Initial Screen Touch
        if (Input.GetMouseButtonDown(0)) {
            //Initial Touch Position (Screen Space)
            TouchPosition = Input.mousePosition;

            //Initial Touch Position (World Space)
            Vector3 touchWorld = gameCamera.ScreenToWorldPoint(TouchPosition);

            //Set Slot at Initial Touch Location
            TouchSlot = Gameboard.instance.GetSlot(touchWorld);

            //Only if within Gameboard bounds
            if (Gameboard.instance.IsInBoardSlotSize(TouchSlot)) {
                //Set Initial Touch Item
                TouchItem = Gameboard.instance.GetSlotItem(TouchSlot);

                //Build Neightbour List
                Gameboard.instance.BuildNeighbours(TouchSlot);

                //Enable Drag Item
                dragItem.SetActive(true);

                //Set Drag Item Sprite to Initial Click Item Sprite
                dragItem.GetComponent<SpriteRenderer>().sprite = TouchItem.ItemIcon;
            }
        }

        //Drag Screen Touch
        if (Input.GetMouseButton(0)) {
            //Initial Drag Position (Screen Space)
            DragPosition = Input.mousePosition;

            //Initial Drag Position (World Space)
            Vector3 dragWorld = gameCamera.ScreenToWorldPoint(DragPosition);

            //Create adjusted dragWorld Vector with Z changed to be able to see Drag Item when active.
            Vector3 adjustedDragWorld = new Vector3(dragWorld.x, dragWorld.y, 0);

            //Set Slot at Drag Touch Location
            DragSlot = Gameboard.instance.GetSlot(dragWorld);

            //Only if within Gameboard bounds
            if (Gameboard.instance.IsInBoardSlotSize(DragSlot)) {
                //Set Drag Touch Item
                DragItem = Gameboard.instance.GetSlotItem(DragSlot);

                //Update Drag Item Position
                dragItem.transform.position = adjustedDragWorld;
            }
        }

        //Drag Screen Touch Released
        if (Input.GetMouseButtonUp(0)) {

            //Check if Drag Item is in same/its own Slot Position
            if (TouchSlot == DragSlot) {
                dragItem.SetActive(false);
                return;
            }
                
                        
            //Check Drag Items Drop Location Allowed
            bool allowed = false;
            foreach (Vector3Int allowedMoveSlot in Gameboard.instance.NeightbourList) {
                if (DragSlot == allowedMoveSlot) {
                    allowed = true;
                    break;
                }
            }

            //If allowed, Swap Touch and Drag Slot Items
            if (allowed) {
                Gameboard.instance.SwitchSlotItems(TouchSlot, DragSlot);
            } 

            dragItem.SetActive(false);
        }
    }
}
