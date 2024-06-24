using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class InventoryOptionsManager : MonoBehaviour, IPointerDownHandler
{
    [Header("Game Object Parameters")]
    public GameObject InventoryOptions;
    public GameObject Inventory;

    [Header("Game Object Equip Parameters")]
    public GameObject EquipStick;
    public GameObject EquipAxe;
    public GameObject EquipSpear;
    public GameObject StoneLeft;
    public GameObject StoneRight;

    [Header("Game Object Button Parameters")]
    public GameObject EquipButtonGameObject;
    public GameObject EquipButtonGameObject1;
    public GameObject EquipButtonGameObject2;
    public GameObject UnequipButtonGameObject;
    public GameObject UseButtonGameObject;
    public GameObject UseButtonGameObject1;
    public GameObject useButtonGameObject2;
    public GameObject UseHalfButtonGameObject;
    public GameObject UseHalfButtonGameObject1;
    public GameObject UseHalfButtonGameObject2;

    [Header("Game Object Food Parameters")]
    public GameObject RawTigerMeat;
    public GameObject EquipMushroom;
    public GameObject EquipBread;
    public GameObject EquipRabbitMeat;

    [Header("Inventory Manager Parameters")]
    public InventoryManager inventoryManager;

    [Header("Index Parameters")]
    public int Index;

    [Header("Image Parameters")]
    public Image ItemIcon;

    [Header("Text Parameters")]
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemWeight;
    public TextMeshProUGUI ItemFound;

    [Header("Button Parameters")]
    public Button EquipButton;
    public Button EquipButton1;
    public Button EquipButton2;
    public Button UnequipButton;
    public Button UseButton;
    public Button UseButton1;
    public Button UseButton2;
    public Button UseHalfButton;
    public Button UseHalfButton1;
    public Button UseHalfButton2;

    [Header("Animator Parameters")]
    public Animator RawTigerMeatAnimator;
    public Animator EquipMushroomAnimator;
    public Animator BreadAnimator;
    public Animator RabbitMeatAnimator;

    [Header("Bar Manager Parameters")]
    public BarManager barManager;

    // bool variables to control flow
    private bool DisplayOptions;

    private bool[] CheckEquipArray = new bool[18];

    private DragManager dragManager;

    private bool IsEquipStick = false;
    private bool IsEquipAxe = false;
    private bool IsEquipSpear = false;

    private bool IsEating = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        ManageImageInOptions();

        ManageItemOptions();

        ManageEquipButtonInOptions();

        HandleDisplayEquipButton();

        if (IsEating)
        {
            inventoryManager.HandleDecreaseSlot();
        }
        IsEating = false;
    }

    private void Start()
    {
        DisplayOptions = false;
        ItemIcon.color = new Color(255, 255, 255, 0);

        dragManager = GetComponent<DragManager>();

        EquipButton.onClick.AddListener(delegate { HandleEquipItem(0); });
        EquipButton1.onClick.AddListener(delegate { HandleEquipItem(1); });
        EquipButton2.onClick.AddListener(delegate { HandleEquipItem(2); });
        UnequipButton.onClick.AddListener(HandleUnequipItem);

        UseButton.onClick.AddListener(delegate { HandleEatingAll(0); });
        UseButton1.onClick.AddListener(delegate { HandleEatingAll(1); });
        UseButton2.onClick.AddListener(delegate { HandleEatingAll(2); });

        for (int i = 0; i < CheckEquipArray.Length; ++i)
        {
            CheckEquipArray[i] = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && DisplayOptions == false)
        {
            InventoryOptions.SetActive(true);
            DisplayOptions = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Input.GetKeyDown(KeyCode.R) && DisplayOptions == true)
        {
            InventoryOptions.SetActive(false);
            DisplayOptions = false;
        }
    }

    private bool CheckItemType(string str, string[] arr)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            if (arr[i] == str)
            {
                return true;
            }
        }
        return false;
    } 

    private void ManageImageInOptions()
    {
        Image[] InventoryIconsArray = inventoryManager.GetInventoryIconsArray();
        ItemIcon.sprite = InventoryIconsArray[Index].sprite;
        ItemIcon.color = new Color(255, 255, 255, 255);
    }

    public void ManageEquipButtonInOptions()
    {
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        string[] EquipableItemName = { "Wooden Stick", "Stone Axe", "Stone Spear", "Raw Tiger Meat", "Mushroom", "Bread", "Raw Rabbit Meat", "Rock"};
        string[] UsableItemName = { "Raw Tiger Meat", "Mushroom", "Bread", "Raw Rabbit Meat"};
        switch (Index)
        {
            case 0:
                if (CheckItemType(ItemNameArray[0], EquipableItemName))
                {
                    EquipButtonGameObject.SetActive(true);
                } else
                {
                    EquipButtonGameObject.SetActive(false);
                }
                if (CheckItemType(ItemNameArray[0], UsableItemName))
                {
                    UseButtonGameObject.SetActive(true);
                    UseHalfButtonGameObject.SetActive(true);
                } else
                {
                    UseButtonGameObject.SetActive(false);
                    UseHalfButtonGameObject.SetActive(false);
                }
                break;
            case 1:
                if (CheckItemType(ItemNameArray[1], EquipableItemName))
                {
                    EquipButtonGameObject1.SetActive(true);
                } else
                {
                    EquipButtonGameObject1.SetActive(false);
                }
                if (CheckItemType(ItemNameArray[1], UsableItemName))
                {
                    UseButtonGameObject1.SetActive(true);
                    UseHalfButtonGameObject1.SetActive(true);
                }
                else
                {
                    UseButtonGameObject1.SetActive(false);
                    UseHalfButtonGameObject1.SetActive(false);
                }
                break;
            case 2:
                if (CheckItemType(ItemNameArray[2], EquipableItemName))
                {
                    EquipButtonGameObject2.SetActive(true);
                } else
                {
                    EquipButtonGameObject2.SetActive(false);
                }
                if (CheckItemType(ItemNameArray[2], UsableItemName))
                {
                    useButtonGameObject2.SetActive(true);
                    UseHalfButtonGameObject2.SetActive(true);
                }
                else
                {
                    useButtonGameObject2.SetActive(false);
                    UseHalfButtonGameObject2.SetActive(false);
                }
                break;
        }
    }

    private void ManageItemOptions()
    {
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        switch (ItemNameArray[Index])
        {
            case "Rock":
                ItemName.text = "ROCK";
                ItemWeight.text = "WEIGHT: 1KG";
                ItemFound.text = "FOUND: SOMEWHERE ON GROUND OR MOUNTAINS.";
                break;
            case "Mushroom":
                ItemName.text = "MUSHROOM";
                ItemWeight.text = "WEIGHT: 0.4KG";
                ItemFound.text = "FOUND: NEAR TREES AND FARMS.";
                break;
            case "Wood":
                ItemName.text = "WOOD";
                ItemWeight.text = "WEIGHT: 0.7KG";
                ItemFound.text = "FOUND: IN THE TREES AND SOME STUMPS IN THE JUNGLE.";
                break;
            case "Wooden Stick":
                ItemName.text = "WOODEN STICK";
                ItemWeight.text = "WEIGHT: 0.5KG";
                ItemFound.text = "FOUND: CRAFTED FROM WOOD.";
                break;
            case "Stone Axe":
                ItemName.text = "STONE AXE";
                ItemWeight.text = "WEIGHT: 2.5KG";
                ItemFound.text = "FOUND: CRAFTED FROM WOODEN STICK AND STONE. ";
                break;
            case "Stone Spear":
                ItemName.text = "STONE SPEAR";
                ItemWeight.text = "WEIGHT: 3KG";
                ItemFound.text = "FOUND: CRAFTED FROM STONE AND WOODEN STICK.";
                break;
            case "Raw Tiger Meat":
                ItemName.text = "RAW TIGER MEAT";
                ItemWeight.text = "WEIGHT: 1.1KG";
                ItemFound.text = "FOUND: WHEN YOU KILL THE TIGER.";
                break;
            case "Rye":
                ItemName.text = "RYE";
                ItemWeight.text = "WEIGHT: 0.1KG";
                ItemFound.text = "FOUND: ON THE GRASS FIELD AND SOME VALLEYS.";
                break;
            case "Flour":
                ItemName.text = "FLOUR";
                ItemWeight.text = "WEIGHT: 0.6KG";
                ItemFound.text = "FOUND: CRAFTED FROM RYE.";
                break;
            case "Bread":
                ItemName.text = "BREAD";
                ItemWeight.text = "WEIGHT: 0.3KG";
                ItemFound.text = "FOUND: CRAFTED FROM FLOUR.";
                break;
            case "Raw Rabbit Meat":
                ItemName.text = "RABBIT MEAT";
                ItemWeight.text = "WEIGHT: 0.75KG";
                ItemFound.text = "FOUND: WHEN YOU KILL THE RABBIT";
                break;
            default:
                break;
        }
    }

    public void DeleteItemIcon()
    {
        ItemIcon.sprite = null;
        ItemIcon.color = new Color(255, 255, 255, 0);
    }

    public void ClearInfoWhenDropItem()
    {
        ItemName.text = "";
        ItemWeight.text = "WEIGHT: ";
        ItemFound.text = "FOUND: ";
    }

    public void HandleEquipItem(int index)
    {
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        switch (ItemNameArray[index])
        {
            case "Wooden Stick":
                if (IsEquipAxe == true)
                {
                    EquipAxe.SetActive(false);
                    IsEquipAxe = false;
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                EquipStick.SetActive(true);
                IsEquipStick = true;
                break;
            case "Stone Axe":
                if (IsEquipStick == true)
                {
                    EquipStick.SetActive(false);
                    IsEquipStick = false;
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                EquipAxe.SetActive(true);
                IsEquipAxe = true;
                break;
            case "Stone Spear":
                EquipSpear.SetActive(true);
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                break;
            case "Raw Tiger Meat":
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                RawTigerMeat.SetActive(true);
                break;
            case "Mushroom":
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                EquipMushroom.SetActive(true);
                break;
            case "Bread":
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                EquipBread.SetActive(true);
                break;
            case "Raw Rabbit Meat":
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                EquipRabbitMeat.SetActive(true);
                break;
            case "Rock":
                if (EquipStick.activeSelf)
                {
                    EquipStick.SetActive(false);
                }
                if (EquipAxe.activeSelf)
                {
                    EquipAxe.SetActive(false);
                }
                if (EquipSpear.activeSelf)
                {
                    EquipSpear.SetActive(false);
                }
                if (RawTigerMeat.activeSelf)
                {
                    RawTigerMeat.SetActive(false);
                }
                if (EquipMushroom.activeSelf)
                {
                    EquipMushroom.SetActive(false);
                }
                if (EquipBread.activeSelf)
                {
                    EquipBread.SetActive(false);
                }
                if (EquipRabbitMeat.activeSelf)
                {
                    EquipRabbitMeat.SetActive(false);
                }
                int[] QuantityArray = inventoryManager.GetQuantityArray();
                int tmp = inventoryManager.GetIndexBaseOnItemName("Rock");
                if (QuantityArray[tmp] >= 2)
                {
                    StoneLeft.SetActive(true);
                    StoneRight.SetActive(true);
                }
                break;
            default:
                break;
        }
        CheckEquipArray[index] = true;
        UnequipButtonGameObject.SetActive(true);
        EquipButtonGameObject.SetActive(false);
    }

    public void HandleDisplayEquipButton()
    {
        if (CheckEquipArray[Index] == true)
        {
            UnequipButtonGameObject.SetActive(true);
            EquipButtonGameObject.SetActive(false);
        } else
        {
            UnequipButtonGameObject.SetActive(false);
        }
    }

    public void HandleUnequipItem()
    {
        if (EquipStick.activeSelf)
        {
            EquipStick.SetActive(false);
            IsEquipStick = false;
        }
        if (EquipAxe.activeSelf)
        {
            EquipAxe.SetActive(false);
            IsEquipAxe = false;
        }
        if (EquipSpear.activeSelf)
        {
            EquipSpear.SetActive(false);
        }
        if (RawTigerMeat.activeSelf)
        {
            RawTigerMeat.SetActive(false);
        }
        CheckEquipArray[Index] = false;
        UnequipButtonGameObject.SetActive(false);
    }

    public void HandleEatingAll(int index)
    {
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        switch (ItemNameArray[index])
        {
            case "Raw Tiger Meat":
                if (RawTigerMeat.activeSelf)
                {
                    if (!EquipMushroom.activeSelf && !EquipBread.activeSelf && !EquipRabbitMeat.activeSelf)
                    {
                        RawTigerMeatAnimator.SetBool("isEat", true);
                    }
                    Invoke(nameof(DestroyMeat), 3f);
                    Invoke(nameof(AddHungerBarAfterEating), 5f);
                    inventoryManager.DecreaseAllItemForConsuming(index);
                    CheckEquipArray[index] = false;
                }
                break;
            case "Mushroom":
                if (EquipMushroom.activeSelf)
                {
                    
                    if (!RawTigerMeat.activeSelf && !EquipBread.activeSelf && !EquipRabbitMeat.activeSelf)
                    {
                        EquipMushroomAnimator.SetBool("isEat", true);
                    }
                    Invoke(nameof(DestroyMeat), 3f);
                    Invoke(nameof(AddHungerBarAfterEating), 5f);
                    inventoryManager.DecreaseAllItemForConsuming(index);
                    CheckEquipArray[index] = false;
                }
                break;
            case "Bread":
                if (EquipBread.activeSelf)
                {
                    if (!RawTigerMeat.activeSelf && !EquipMushroom.activeSelf && !EquipRabbitMeat.activeSelf)
                    {
                        BreadAnimator.SetBool("isEat", true);
                    }
                    Invoke(nameof(DestroyMeat), 3f);
                    Invoke(nameof(AddHungerBarAfterEating), 5f);
                    inventoryManager.DecreaseAllItemForConsuming(index);
                    CheckEquipArray[index] = false;
                }
                break;
            case "Raw Rabbit Meat":
                if (EquipRabbitMeat.activeSelf)
                {
                    if (!RawTigerMeat.activeSelf && !EquipBread.activeSelf && !EquipMushroom.activeSelf)
                    {
                        RabbitMeatAnimator.SetBool("isEat", true);
                    }
                    Invoke(nameof(DestroyMeat), 3f);
                    Invoke(nameof(AddHungerBarAfterEating), 5f);
                    inventoryManager.DecreaseAllItemForConsuming(index);
                    CheckEquipArray[index] = false;
                }
                break;
            default:
                break;
        }
        IsEating = true;
    }

    private void DestroyMeat()
    {

        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        switch (ItemNameArray[Index])
        {
            case "Raw Tiger Meat":
                RawTigerMeatAnimator.SetBool("isEat", false);
                RawTigerMeat.SetActive(false);
                break;
            case "Mushroom":
                EquipMushroomAnimator.SetBool("isEat", false);
                EquipMushroom.SetActive(false);
                break;
            case "Bread":
                BreadAnimator.SetBool("isEat", false);
                EquipBread.SetActive(false);
                break;
            case "Raw Rabbit Meat":
                RabbitMeatAnimator.SetBool("isEat", false);
                EquipRabbitMeat.SetActive(false);
                break;
        }
    }

    private void AddHungerBarAfterEating()
    {
        string[] ItemNameArray = inventoryManager.GetItemNameArray();
        switch (ItemNameArray[Index])
        {
            case "Raw Tiger Meat":
                barManager.IncreaseHungerBarWhenEating(4f, 0.08f / 3);
                break;
            case "Mushroom":
                barManager.IncreaseHungerBarWhenEating(1f, 0.02f / 3);
                break;
            case "Bread":
                barManager.IncreaseHungerBarWhenEating(2f, 0.04f / 3);
                break;
            case "Raw Rabbit Meat":
                barManager.IncreaseHungerBarWhenEating(3f, 0.06f / 3);
                break;
        }
    }
}
