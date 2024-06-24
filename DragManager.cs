using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Canvas Parameters")]
    public Canvas canvas;

    [Header("Game Object Parameters")]
    public GameObject RockPrefab;
    public GameObject MushroomPrefab;
    public GameObject StumpPrefab;
    public GameObject StickPrefab;
    public GameObject StoneAxePrefab;
    public GameObject SpearPrefab;
    public GameObject RawTigerMeatPrefab;
    public GameObject RyePrefab;
    public GameObject FlourPrefab;
    public GameObject BreadPrefab;

    public GameObject Player;
    public GameObject DropOptionScreen;

    public GameObject EquipStick;
    public GameObject EquipAxe;
    public GameObject EquipSpear;

    [Header("Index Parameters")]
    public int Index = 0;

    [Header("Inventory Manager Parameters")]
    public InventoryManager inventoryManager;


    private InventoryOptionsManager inventoryOptionsManager;


    private Vector3 StoredOldPosition;
    private Image image;
    private bool DisplayOptions;
    private RectTransform rectTransform;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        StoredOldPosition = rectTransform.transform.position;
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        DisplayOptions = false;
        inventoryOptionsManager = GetComponent<InventoryOptionsManager>();  
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag!");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        canvasGroup.alpha = 0.6f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag!");
        if (CheckOutOfBoard())
        {
            if (CheckDropAll())
            {
                HandleDropAllItem();
            }
            if (CheckDropHalf())
            {
                HandleDropHalfItem();
            } 
            if (CheckDropOne())
            {
                HandleDropOneItem();
            }
            rectTransform.transform.position = StoredOldPosition;
        } else
        {
            rectTransform.transform.position = StoredOldPosition;
        }
        DropOptionScreen.SetActive(false);
        canvasGroup.alpha = 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Pointer Down!");
        DisplayOptions = true;

        DropOptionScreen.SetActive(true);
    }

    private bool CheckOutOfBoard()
    {
        if (rectTransform.anchoredPosition.x > 164 || rectTransform.anchoredPosition.x < -436
            || rectTransform.anchoredPosition.y > 221 || rectTransform.anchoredPosition.y < -179)
        {
            return true;
        }
        return false;
    }

    private bool CheckDropAll()
    {
        if (rectTransform.anchoredPosition.x > -480 && rectTransform.anchoredPosition.x < -330
            && rectTransform.anchoredPosition.y > -222 && rectTransform.anchoredPosition.y < -172)
        {
            return true;
        }
        return false;
    }

    private bool CheckDropHalf()
    {
        if (rectTransform.anchoredPosition.x > -293 && rectTransform.anchoredPosition.x < -143
            && rectTransform.anchoredPosition.y > -222 && rectTransform.anchoredPosition.y < -172)
        {
            return true;
        }
        return false;
    }

    private bool CheckDropOne()
    {
        if (rectTransform.anchoredPosition.x > -105 && rectTransform.anchoredPosition.x < 45
            && rectTransform.anchoredPosition.y > -222 && rectTransform.anchoredPosition.y < -172)
        {
            return true;
        }
        return false;
    }

   private void HandleDropAllItem()
   { 
        int[] quantity = inventoryManager.GetQuantityArray();
        int ItemQuantity = quantity[Index];
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        GameObject ItemPrefab = null;
        Vector3 Rotation = new Vector3(0, 0, 0);
        switch (ItemNameArray[Index])
        {
            case "Rock":
                ItemPrefab = RockPrefab;
                break;
            case "Mushroom":
                ItemPrefab = MushroomPrefab;
                break;
            case "Wood":
                ItemPrefab = StumpPrefab;
                break;
            case "Wooden Stick":
                ItemPrefab = StickPrefab;
                Rotation = new Vector3(0f, 0f, 90f);
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                break;
            case "Stone Axe":
                ItemPrefab = StoneAxePrefab;
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                break;
            case "Stone Spear":
                ItemPrefab = SpearPrefab;
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                break;
            case "Raw Tiger Meat":
                ItemPrefab = RawTigerMeatPrefab;
                break;
            case "Rye":
                ItemPrefab = RyePrefab;
                break;
            case "Flour":
                ItemPrefab = FlourPrefab;
                break;
            case "Bread":
                ItemPrefab = BreadPrefab;
                break;
            default:
                break;
        }
        for (int i = 1; i <= ItemQuantity; ++i)
        {
            Instantiate(ItemPrefab, Player.transform.position + new Vector3(1 + i, -1, 1 + i), Quaternion.Euler(Rotation));
        }
        inventoryManager.DropAllItem(Index);
        inventoryOptionsManager.DeleteItemIcon();
        inventoryOptionsManager.ClearInfoWhenDropItem();
        image.sprite = null;
        image.color = new Color(255, 255, 255, 0);
    }

    private void HandleDropHalfItem()
    {
        int[] quantity = inventoryManager.GetQuantityArray();
        int ItemQuantity = quantity[Index] / 2;
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        GameObject ItemPrefab = null;
        switch (ItemNameArray[Index])
        {
            case "Rock":
                ItemPrefab = RockPrefab;
                break;
            case "Mushroom":
                ItemPrefab = MushroomPrefab;
                break;
            case "Wood":
                ItemPrefab = StumpPrefab;
                break;
            case "Wooden Stick":
                ItemPrefab = StickPrefab;
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                break;
            case "Stone Axe":
                ItemPrefab = StoneAxePrefab;
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                break;
            case "Stone Spear":
                ItemPrefab = SpearPrefab;
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                break;
            case "Raw Tiger Meat":
                ItemPrefab = RawTigerMeatPrefab;
                break;
            case "Rye":
                ItemPrefab = RyePrefab;
                break;
            case "Flour":
                ItemPrefab = FlourPrefab;
                break;
            case "Bread":
                ItemPrefab = BreadPrefab;
                break;
            default:
                break;
        }
        for (int i = 1; i <= ItemQuantity; ++i)
        {
            Instantiate(ItemPrefab, Player.transform.position + new Vector3(1 + i, -1, 1 + i), Quaternion.identity);
        }
        inventoryManager.DecreaseHalfItem(Index);
    }

    private void HandleDropOneItem()
    {
        int[] quantity = inventoryManager.GetQuantityArray();
        int ItemQuantity = quantity[Index] - 1;
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        GameObject ItemPrefab = null;
        switch (ItemNameArray[Index])
        {
            case "Rock":
                ItemPrefab = RockPrefab;
                break;
            case "Mushroom":
                ItemPrefab = MushroomPrefab;
                break;
            case "Wood":
                ItemPrefab = StumpPrefab;
                break;
            case "Wooden Stick":
                ItemPrefab = StickPrefab;
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                break;
            case "Stone Axe":
                ItemPrefab = StoneAxePrefab;
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                break;
            case "Stone Spear":
                ItemPrefab = SpearPrefab;
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                break;
            case "Raw Tiger Meat":
                ItemPrefab = RawTigerMeatPrefab;
                break;
            case "Rye":
                ItemPrefab = RyePrefab;
                break;
            case "Flour":
                ItemPrefab = FlourPrefab;
                break;
            case "Bread":
                ItemPrefab = BreadPrefab;
                break;
            default:
                break;
        }
        Instantiate(ItemPrefab, Player.transform.position + new Vector3(1, -1, 1), Quaternion.identity);
        inventoryManager.DecreaseOneItem(Index);
    }

}
