using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageCraftResult : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Game Object Parameters")]
    public GameObject CraftResultInfo;
    public TextMeshProUGUI CraftResultName;
    public TextMeshProUGUI CraftResultUse;
    public TextMeshProUGUI CraftResultCanCraft;

    [Header("Inventory Manager Parameters")]
    public InventoryManager inventoryManager;

    [Header("Craft Manager Parameters")]
    public CraftManager craftManager;

    private Image CraftResultImage;

    private bool IsCrafted;

    private void Start()
    {
        CraftResultImage = GetComponent<Image>();

        IsCrafted = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int ItemQuantity = craftManager.GetNumberCraftResult();
        string ItemName = craftManager.GetCraftResultName();
        switch(ItemName)
        {
            case "Wooden Stick":
                CraftResultName.text = "NAME: Wooden Stick";
                CraftResultUse.text = "USE: Hit animals, make fire,...";
                CraftResultCanCraft.text = "CAN CRAFT: Stone Axe, Stone Spear,...";
                break;
            case "Stone Axe":
                CraftResultName.text = "NAME: Stone Axe";
                CraftResultUse.text = "USE: Chop trees, hit animals,...";
                CraftResultCanCraft.text = "CAN CRAFT: None";
                break;
            case "Stone Spear":
                CraftResultName.text = "NAME: Stone Spear";
                CraftResultUse.text = "USE: Hunt animals, hit zombies,...";
                CraftResultCanCraft.text = "CAN CRAFT: None";
                break;
            case "Flour":
                CraftResultName.text = "NAME: Wheat Flour";
                CraftResultUse.text = "USE: Make breads, noodles,...";
                CraftResultCanCraft.text = "CAN CRAFT: Any food which use flour.";
                break;
            case "Bread":
                CraftResultName.text = "NAME: Bread";
                CraftResultUse.text = "USE: Basic food.";
                CraftResultCanCraft.text = "CAN CRAFT: None";
                break;
        }

        if (CraftResultImage.sprite != null)
        {
            CraftResultInfo.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CraftResultImage.sprite != null)
        {
            CraftResultInfo.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsCrafted = true;
        int ItemQuantity = craftManager.GetNumberCraftResult();
        string ItemName = craftManager.GetCraftResultName();
        switch (ItemName)
        {
            case "Wooden Stick":
                for (int i = 0; i < ItemQuantity; ++i)
                {
                    inventoryManager.HandleAddWeightStick();
                }
                break;
            case "Stone Axe":
                for (int i = 0; i < ItemQuantity; ++i)
                {
                    inventoryManager.HandleAddWeightStoneAxe();
                }
                break;
            case "Stone Spear":
                for (int i = 0; i < ItemQuantity; ++i)
                {
                    inventoryManager.HandleAddWeightStoneSpear();
                }
                break;
            case "Flour":
                for (int i = 0; i < ItemQuantity; ++i)
                {
                    inventoryManager.HandleAddWeightWheatFlour();
                }
                break;
            case "Bread":
                for (int i = 0; i < ItemQuantity; ++i)
                {
                    inventoryManager.HandleAddWeightBread();
                }
                break;
        }

        inventoryManager.AddItemFromCraft(CraftResultImage, ItemQuantity, ItemName);
        craftManager.HandleCrafting();
        CraftResultInfo.SetActive(false);
        Debug.Log("Item Name: " + ItemName);
    }

    public bool CheckCrafted()
    {
        return IsCrafted;
    }
}
