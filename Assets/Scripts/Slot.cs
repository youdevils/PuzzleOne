using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField]
    private Vector3Int slotIndex;

    public Vector3Int SlotIndex { get { return slotIndex; } }

    public bool IsHeld { get; private set; }

    public void OnPointerDown(PointerEventData eventData) {
        IsHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        IsHeld = false;
    }
}
