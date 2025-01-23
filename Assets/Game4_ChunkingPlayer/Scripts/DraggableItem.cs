using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public bool isSnapOnSlot = false;
    public GameObject parentDrag; //จาก UI
    public GameObject parentDrop; //ในพื้นที่เล่นเกม
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isSnapOnSlot = false;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        offset = rectTransform.position - Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
        rectTransform.position = new Vector3(newPosition.x, newPosition.y, rectTransform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (!isSnapOnSlot)
        {
            parentDrag.SetActive(true);
            Debug.Log("OnEndDrag");
            //Destroy(gameObject);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void SnapToSlot(GameObject _dropGO)
    {
        isSnapOnSlot = true;
        parentDrop = _dropGO;
    }

}
