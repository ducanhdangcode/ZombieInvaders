using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

public class InventoryManager : MonoBehaviour
{
    [Header("Game Object Parameters")]
    public GameObject Inventory;
    public GameObject Player;

    [Header("Sprite Parameters")]
    public Sprite RockSprite;
    public Sprite MushroomSprite;
    public Sprite WoodSprite;
    public Sprite StickSprite;
    public Sprite StoneAxeSprite;
    public Sprite StoneSpearSprite;
    public Sprite RawTigerMeatSprite;
    public Sprite RyeSprite;
    public Sprite FlourSprite;
    public Sprite BreadSprite;
    public Sprite RabbitMeatSprite;

    [Header("Image Array Parameters")]
    public Image[] InventoryIcons = new Image[18];

    [Header("Text Array Parameters")]
    public TextMeshProUGUI[] InventoryQuantity = new TextMeshProUGUI[18];

    [Header("Text Parameters")]
    public TextMeshProUGUI WeightText;
    public TextMeshProUGUI SlotText;

    private bool[] CheckItemSlot = new bool[18];
    private int[] Quantity = new int[18];
    private string[] ItemName = new string[18];

    private double weight = 0;
    private int slot = 0;

    private int[] NumberItemCrafted = new int[18];

    private void Start()
    {
        for (int i = 0; i < 18; ++i)
        {
            CheckItemSlot[i] = false;
        }
        for (int i = 0; i < 18; ++i)
        {
            Quantity[i] = 0;
        }
        for (int i = 0; i < 18; ++i)
        {
            ItemName[i] = "None";
        }
        for (int i = 0; i < 18; ++i)
        {
            InventoryIcons[i].sprite = null;
            InventoryIcons[i].color = new Color(255, 255, 255, 0);
        }
        for (int i = 0; i < 18; ++i)
        {
            NumberItemCrafted[i] = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Inventory.activeSelf == false)
            {
                Inventory.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Inventory.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            if (selectionTransform.GetComponent<HoverableObjects>() && selectionTransform.GetComponent<CanPickupObject>())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && CheckDistance(hit.transform))
                {
                    switch (selectionTransform.GetComponent<CanPickupObject>().ItemType)
                    {
                        case CanPickupObject.TypesOfItem.Rock:
                            AddItemToInventory(RockSprite, "Rock");
                            HandleAddWeightRock();
                            break;
                        case CanPickupObject.TypesOfItem.Mushroom:
                            AddItemToInventory(MushroomSprite, "Mushroom");
                            HandleAddWeightMushroom();
                            break;
                        case CanPickupObject.TypesOfItem.Wood:
                            AddItemToInventory(WoodSprite, "Wood");
                            HandleAddWeightWood();
                            break;
                        case CanPickupObject.TypesOfItem.WoodenStick:
                            AddItemToInventory(StickSprite, "Wooden Stick");
                            HandleAddWeightStick();
                            break;
                        case CanPickupObject.TypesOfItem.StoneAxe:
                            AddItemToInventory(StoneAxeSprite, "Stone Axe");
                            HandleAddWeightStoneAxe();
                            break;
                        case CanPickupObject.TypesOfItem.StoneSpear:
                            AddItemToInventory(StoneSpearSprite, "Stone Spear");
                            HandleAddWeightStoneSpear();
                            break;
                        case CanPickupObject.TypesOfItem.TigerMeat:
                            AddItemToInventory(RawTigerMeatSprite, "Raw Tiger Meat");
                            HandleAddWeightRawTigerMeat();
                            break;
                        case CanPickupObject.TypesOfItem.Rye:
                            AddItemToInventory(RyeSprite, "Rye");
                            HandleAddWeightRye();
                            break;
                        case CanPickupObject.TypesOfItem.Flour:
                            AddItemToInventory(FlourSprite, "Flour");
                            HandleAddWeightWheatFlour();
                            break;
                        case CanPickupObject.TypesOfItem.Bread:
                            AddItemToInventory(BreadSprite, "Bread");
                            HandleAddWeightBread();
                            break;
                        case CanPickupObject.TypesOfItem.RabbitMeat:
                            AddItemToInventory(RabbitMeatSprite, "Raw Rabbit Meat");
                            HandleAddWeightRabbitMeat();
                            break;
                    }
                    Debug.Log("Item added to iventory!");
                    Destroy(selectionTransform.GetComponent<CanPickupObject>().gameObject);
                }
            }
        }
    }

    private void AddItemToInventory(Sprite sprite, string _itemName)
    {
        if (GetIndexOfExist(InventoryIcons, sprite) == -1)
        {
            int IndexEmptySlot = GetIndexEmptySlot(CheckItemSlot);
            InventoryIcons[IndexEmptySlot].sprite = sprite;
            InventoryIcons[IndexEmptySlot].color = new Color(255, 255, 255, 255);
            Quantity[IndexEmptySlot]++;
            InventoryQuantity[IndexEmptySlot].text = Quantity[IndexEmptySlot].ToString();
            ItemName[IndexEmptySlot] = _itemName;
            CheckItemSlot[IndexEmptySlot] = true;
            HandleAddSlot();
        } else
        {
            int ExistIndex = GetIndexOfExist(InventoryIcons, sprite);
            Quantity[ExistIndex]++;
            InventoryQuantity[ExistIndex].text = Quantity[ExistIndex].ToString();
        }
    }

    private bool CheckDistance(Transform hit)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(hit.position.x - Player.transform.position.x, 2)
            + Mathf.Pow(hit.transform.position.z - Player.transform.position.z, 2));
        if (distance > 35f)
        {
            return false;
        }
        return true;
    }

    private int GetIndexEmptySlot(bool[] arr)
    {
        for (int i = 0; i < 18; ++i)
        {
            if (arr[i] == false)
            {
                return i;
            }
        }
        return -1;
    }

    private int GetIndexOfExist(Image[] arr, Sprite sprite)
    {
        for (int i = 0; i < 18; ++i)
        {
            if (arr[i].sprite == sprite)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetIndexBaseOnItemName(string val)
    {
        for (int i = 0; i < ItemName.Length; ++i)
        {
            if (ItemName[i] == val)
            {
                return i;
            }
        }
        return -1;
    }

    private void HandleAddWeightRock()
    {
        weight += 1;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleAddWeightMushroom()
    {
        weight += 0.4;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleAddWeightWood()
    {
        weight += 0.7;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    public void HandleAddWeightStick()
    {
        weight += 0.5;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    public void HandleAddWeightStoneAxe()
    {
        weight += 2.5;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    public void HandleAddWeightStoneSpear()
    {
        weight += 3;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleAddWeightRawTigerMeat()
    {
        weight += 1.1;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleAddWeightRye()
    {
        weight += 0.1;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    public void HandleAddWeightWheatFlour()
    {
        weight += 0.6;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    public void HandleAddWeightBread()
    {
        weight += 0.3;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    public void HandleAddWeightRabbitMeat()
    {
        weight += 0.75;
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleDecreaseWeightRock()
    {
        weight -= 1;
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleDecreaseWeightMushroom()
    {
        weight -= 0.4;
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleDecreaseWeightWood()
    {
        weight -= 0.7;
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleDecreaseWeightStick()
    {
        weight -= 0.5;
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleDecreaseWeightStoneAxe()
    {
        weight -= 2.5;
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleDecreaseWeightStoneSpear()
    {
        weight -= 3;
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight + "KG";
    }

    private void HandleAddSlot()
    {
        slot++;
        SlotText.text = "SLOT: " + slot;
    }

    public void HandleDecreaseSlot()
    {
        slot--;
        SlotText.text = "SLOT: " + slot;
    }

    public int[] GetQuantityArray()
    {
        return Quantity;
    }

    public void DropAllItem(int idx)
    {
        switch (ItemName[idx])
        {
            case "Rock":
                weight -= (1 * Quantity[idx]);
                break;
            case "Mushroom":
                weight -= (0.4 * Quantity[idx]);
                break;
            case "Wood":
                weight -= (0.7 * Quantity[idx]);
                break;
            case "Wooden Stick":
                weight -= (0.5 * Quantity[idx]);
                break;
            case "Stone Axe":
                weight -= (2.5 * Quantity[idx]);
                break;
            case "Stone Spear":
                weight -= (3 * Quantity[idx]);
                break;
            case "Raw Tiger Meat":
                weight -= (1.1 * Quantity[idx]);
                break;
            case "Rye":
                weight -= (0.1 * Quantity[idx]);
                break;
            case "Flour":
                weight -= (0.6 * Quantity[idx]);
                break;
            case "Bread":
                weight -= (0.3 * Quantity[idx]);
                break;
            default:
                break;
        }
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight.ToString() + "KG";
        Quantity[idx] = 0;
        InventoryQuantity[idx].text = Quantity[idx].ToString();
        HandleEmpty(idx);
        CheckItemSlot[idx] = false;
        HandleDecreaseSlot();
    }

    public void DecreaseAllItemForConsuming(int idx)
    {
        switch (ItemName[idx])
        {
            case "Rock":
                weight -= (1 * Quantity[idx]);
                break;
            case "Mushroom":
                weight -= (0.4 * Quantity[idx]);
                break;
            case "Wood":
                weight -= (0.7 * Quantity[idx]);
                break;
            case "Wooden Stick":
                weight -= (0.5 * Quantity[idx]);
                break;
            case "Stone Axe":
                weight -= (2.5 * Quantity[idx]);
                break;
            case "Stone Spear":
                weight -= (3 * Quantity[idx]);
                break;
            case "Raw Tiger Meat":
                weight -= (1.1 * Quantity[idx]);
                break;
            case "Rye":
                weight -= (0.1 * Quantity[idx]);
                break;
            case "Flour":
                weight -= (0.6 * Quantity[idx]);
                break;
            case "Bread":
                weight -= (0.3 * Quantity[idx]);
                break;
            default:
                break;
        }
        if (weight < 0.001)
        {
            weight = 0;
        }
        WeightText.text = "WEIGHT: " + weight.ToString() + "KG";
        Quantity[idx] = 0;
        InventoryQuantity[idx].text = Quantity[idx].ToString();
        HandleEmpty(idx);
        CheckItemSlot[idx] = false;
    }

    public Image[] GetInventoryIconsArray()
    {
        return InventoryIcons;
    }

    public string[] GetItemNameArray()
    {
        return ItemName;
    }

    public TextMeshProUGUI[] GetInventoryQuantityArray()
    {
        return InventoryQuantity;
    }
    
    public void DecreaseHalfItem(int index)
    {
        int ItemDropQuantity = Quantity[index] / 2;
        Quantity[index] -= ItemDropQuantity;
        switch (ItemName[index])
        {
            case "Rock":
                weight -= (1 * ItemDropQuantity);
                break;
            case "Mushroom":
                weight -= (0.4 * ItemDropQuantity);
                break;
            case "Wood":
                weight -= (0.7 * ItemDropQuantity);
                break;
            case "Wooden Stick":
                weight -= (0.5 * ItemDropQuantity);
                break;
            case "Stone Axe":
                weight -= (2.5 * ItemDropQuantity);
                break;
            case "Stone Spear":
                weight -= (3 * ItemDropQuantity);
                break;
            case "Raw Tiger Meat":
                weight -= (1.1 * ItemDropQuantity);
                break;
            case "Rye":
                weight -= (0.1 * ItemDropQuantity);
                break;
            case "Flour":
                weight -= (0.6 * ItemDropQuantity);
                break;
            case "Bread":
                weight -= (0.3 * ItemDropQuantity);
                break;
            default:
                break;
        }
        WeightText.text = "WEIGHT: " + weight.ToString() + "KG";
        InventoryQuantity[index].text = Quantity[index].ToString();
    } 

    public void DecreaseOneItem(int index)
    {
        Quantity[index]--;
        switch (ItemName[index])
        {
            case "Rock":
                weight -= 1;
                break;
            case "Mushroom":
                weight -= 0.4;
                break;
            case "Wood":
                weight -= 0.7;
                break;
            case "Wooden Stick":
                weight -= 0.5;
                break;
            case "Stone Axe":
                weight -= 2.5;
                break;
            case "Stone Spear":
                weight -= 3;
                break;
            case "Raw Tiger Meat":
                weight -= 1.1;
                break;
            case "Rye":
                weight -= 0.1;
                break;
            case "Flour":
                weight -= 0.6;
                break;
            case "Bread":
                weight -= 0.3;
                break;
            default:
                break;
        }
        if (weight < 0.001)
        {
            weight = 0;
        }   
        WeightText.text = "WEIGHT: " + weight.ToString() + "KG";
        if (Quantity[index] == 0)
        {
            InventoryIcons[index].sprite = null;
            InventoryIcons[index].color = new Color(255, 255, 255, 0);
            CheckItemSlot[index] = false;
            ItemName[index] = "";
            HandleDecreaseSlot();
        }
        InventoryQuantity[index].text = Quantity[index].ToString();
    }

    public void HandleEmpty(int index)
    {
        if (Quantity[index] == 0)
        {
            InventoryIcons[index].sprite = null;
            InventoryIcons[index].color = new Color(255, 255, 255, 0);
        }
    }

    public void DecreaseOneItemForCraft(int index)
    {
        Quantity[index]--;
        InventoryQuantity[index].text = Quantity[index].ToString();
        switch (ItemName[index])
        {
            case "Rock":
                HandleDecreaseWeightRock();
                break;
            case "Mushroom":
                HandleDecreaseWeightMushroom();
                break;
            case "Wood":
                HandleDecreaseWeightWood();
                break;
            case "Wooden Stick":
                HandleDecreaseWeightStick();
                break;
            case "Stone Axe":
                HandleDecreaseWeightStoneAxe();
                break;
            case "Stone Spear":
                HandleDecreaseWeightStoneSpear();
                break;
            case "Rye":
                weight -= 0.1;
                WeightText.text = "WEIGHT: " + weight.ToString() + "KG";
                break;
            case "Flour":
                weight -= 0.6;
                WeightText.text = "WEIGHT: " + weight.ToString() + "KG";
                break;
        }
        if (Quantity[index] == 0)
        {
            InventoryIcons[index].sprite = null;
            InventoryIcons[index].color = new Color(255, 255, 255, 0);
            CheckItemSlot[index] = false;
            ItemName[index] = "";
            HandleDecreaseSlot();
        }
        NumberItemCrafted[index]++;
    }

    public void ReturnOldQuantityWhenNotDrop()
    {
        for (int i = 0; i < NumberItemCrafted.Length; i++)
        {
            Quantity[i] += NumberItemCrafted[i];
            InventoryQuantity[i].text = Quantity[i].ToString();
            if (CheckItemSlot[i] == true)
            {
                InventoryIcons[i].color = new Color(255, 255, 255, 255);
            }
        }

        for (int i = 0; i < NumberItemCrafted.Length; ++i)
        {
            NumberItemCrafted[i] = 0;
        }

        for (int i = 0; i < NumberItemCrafted.Length; ++i)
        {
            switch (ItemName[i])
            {
                case "Rock":
                    InventoryIcons[i].sprite = RockSprite;
                    break;
                case "Mushroom":
                    InventoryIcons[i].sprite = MushroomSprite;
                    break;
                case "Wood":
                    InventoryIcons[i].sprite = WoodSprite;
                    break;
            }
        }
    }

    public void AddItemFromCraft(Image image, int ItemQuantity, string itemName) 
    {
        if (GetIndexOfExist(InventoryIcons, image.sprite) == -1)
        {
            int EmptyIndex = GetIndexEmptySlot(CheckItemSlot);
            InventoryIcons[EmptyIndex].sprite = image.sprite;
            InventoryIcons[EmptyIndex].color = new Color(255, 255, 255, 255);
            Quantity[EmptyIndex] += ItemQuantity;
            InventoryQuantity[EmptyIndex].text = Quantity[EmptyIndex].ToString();
            ItemName[EmptyIndex] = itemName;
            CheckItemSlot[EmptyIndex] = true;
            HandleAddSlot();
        } else
        {
            int ExistIndex = GetIndexOfExist(InventoryIcons, image.sprite);
            Quantity[ExistIndex]++;
            InventoryQuantity[ExistIndex].text = Quantity[ExistIndex].ToString();
        }
    }

    public void AddWoodFromChoppingTree()
    {
        if (GetIndexOfExist(InventoryIcons, WoodSprite) == -1)
        {
            int EmptyIndex = GetIndexEmptySlot(CheckItemSlot);
            InventoryIcons[EmptyIndex].sprite = WoodSprite;
            InventoryIcons[EmptyIndex].color = new Color(255, 255, 255, 255);
            Quantity[EmptyIndex] += 3;
            InventoryQuantity[EmptyIndex].text = Quantity[EmptyIndex].ToString();
            ItemName[EmptyIndex] = "Wood";
            CheckItemSlot[EmptyIndex] = true;
            HandleAddSlot();
        } else
        {
            int ExistIndex = GetIndexOfExist(InventoryIcons, WoodSprite);
            Quantity[ExistIndex] += 3;
            InventoryQuantity[ExistIndex].text = Quantity[ExistIndex].ToString();
        }

        for (int i = 0; i < 3; ++i)
        {
            HandleAddWeightWood();
        }
    }
}
