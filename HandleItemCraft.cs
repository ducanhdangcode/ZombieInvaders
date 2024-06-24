using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleItemCraft : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public int Index;
    public CraftManager craftManager;

    public Canvas canvas;

    private Vector3 StoreOldPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        StoreOldPosition = rectTransform.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Craft item begins to be dragged!");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On drag craft item");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End drag craft item");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public string GetItemDropName(int Index)
    {
        return craftManager.GetItemName(Index);
    }

    public void DecreaseItemWhenDrop()
    {
        craftManager.DecreaseNumberItem(Index);
    }

    public void RecreatingImageAfterDrop()
    {
        if (!craftManager.CheckEmpty(Index))
        {
            Image image = GetComponent<Image>();
            craftManager.CopySprite(Index);
        } 
    }

    public void SetPosition()
    {
        rectTransform.transform.position = StoreOldPosition;
    }

    public void HandleWhenEmpty()
    {
        craftManager.HandleWhenEmptySlot(Index);
    }
}
