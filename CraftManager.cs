using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    [Header("Game Object Parameters")]
    public GameObject CraftScreen;

    [Header("Image Array")]
    public Image[] CraftItemIcon = new Image[10];
    public Image[] CraftSlotIcon = new Image[4];

    [Header("Text Array")]
    public TextMeshProUGUI[] CraftItemNumber = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] CraftSlotItemNumber = new TextMeshProUGUI[4];

    [Header("Inventory Manager Parameter")]
    public InventoryManager inventoryManager;

    [Header("Sprite Parameters")]
    public Sprite RockSprite;
    public Sprite MushroomSprite;
    public Sprite StickSprite;
    public Sprite AxeSprite;
    public Sprite SpearSprite;
    public Sprite FlourSprite;
    public Sprite BreadSprite;

    [Header("Image Parameter")]
    public Image CraftItemResult;

    [Header("Text Parameter")]
    public TextMeshProUGUI CraftResultNumber;

    [Header("Manage Craft Result")]
    public ManageCraftResult manageCraftResult;

    private bool isDisplayCraft;

    private string[] CraftRecipe = new string[4];

    private Dictionary<string, Sprite> RecipeAndResult = new Dictionary<string, Sprite>();
    private Dictionary<string, string> RecipeAndName = new Dictionary<string, string>();

    private int[] CraftSlotQuantity = new int[4];

    private int[] CraftItemQuantity = new int[10];

    private int MinMaterialNumber = 0;

    private int CountExist = 0;

    private void Start()
    {
        isDisplayCraft = false;
        for (int i = 0; i < CraftItemIcon.Length; ++i)
        {
            CraftItemIcon[i].sprite = null;
        }
        for (int i = 0; i < CraftItemNumber.Length; ++i)
        {
            CraftItemNumber[i].text = "0";
        }
        for (int i = 0; i < CraftRecipe.Length; ++i)
        {
            CraftRecipe[i] = "0";
        }
        for (int i = 0; i < CraftSlotItemNumber.Length; ++i)
        {
            CraftSlotItemNumber[i].text = "0";
        }
        for(int i = 0; i < CraftSlotQuantity.Length; ++i)
        {
            CraftSlotQuantity[i] = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && isDisplayCraft == false)
        {
            CraftScreen.SetActive(true);
            isDisplayCraft = true;
            Cursor.lockState = CursorLockMode.Confined;
            if (manageCraftResult.CheckCrafted() == false)
            {
                ReturnOldValueToCraftWhenDisplay();
            }
        } else if (Input.GetKeyDown(KeyCode.T) && isDisplayCraft == true)
        {
            RefreshWhenCloseCraftScreen();
            CraftScreen.SetActive(false);
            isDisplayCraft = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        CopyFromInventory();

        CountExist = CountExistMaterial();
        Debug.Log(CountExist);
    }

    private void CopyFromInventory()
    {
        Image[] CopyArrayIcons = inventoryManager.GetInventoryIconsArray();
        for (int i = 0; i < CraftItemIcon.Length; ++i)
        {
            CraftItemIcon[i].sprite = CopyArrayIcons[i].sprite;
            CraftItemIcon[i].color = CopyArrayIcons[i].color;
        }
        for (int i = 0; i < CraftItemQuantity.Length; ++i)
        {
            CraftItemQuantity[i] = inventoryManager.GetQuantityArray()[i];
        }
        for (int i = 0; i < CraftItemQuantity.Length; ++i)
        {
            CraftItemNumber[i].text = CraftItemQuantity[i].ToString();
        }
    }

    public string GetItemName(int index)
    {
        string[] GetItemNameArray = inventoryManager.GetItemNameArray();
        return GetItemNameArray[index];
    }

    public void SetRecipe(int index, string str)
    {
        CraftRecipe[index] = str;
    } 

    public string GetFinalRecipe()
    {
        string Recipe = "";
        for (int i = 0; i < CraftRecipe.Length; ++i)
        {
            Recipe += CraftRecipe[i];
        }
        return Recipe;
    }

    public void BuildRecipe()
    {
        // build single recipe
        RecipeAndResult["M000"] = null;
        RecipeAndResult["0M00"] = null;
        RecipeAndResult["00M0"] = null;
        RecipeAndResult["000M"] = null;

        RecipeAndResult["R000"] = null; 
        RecipeAndResult["0R00"] = null;
        RecipeAndResult["00R0"] = null;
        RecipeAndResult["000R"] = null;


        RecipeAndResult["W000"] = null;
        RecipeAndResult["0W00"] = null;
        RecipeAndResult["00W0"] = null;
        RecipeAndResult["000w"] = null;


        RecipeAndResult["0000"] = null;
        RecipeAndResult["0"] = null;

        RecipeAndResult["Ws000"] = null;
        RecipeAndResult["0Ws00"] = null;
        RecipeAndResult["00Ws0"] = null;
        RecipeAndResult["000Ws"] = null;

        RecipeAndResult["Rye000"] = null;
        RecipeAndResult["0Rye00"] = null;
        RecipeAndResult["00Rye0"] = null;
        RecipeAndResult["000Rye"] = null;

        RecipeAndResult["Flour000"] = null;
        RecipeAndResult["0Flour00"] = null;
        RecipeAndResult["00Flour0"] = null;
        RecipeAndResult["000Flour"] = null;

        // build complex recipe
        RecipeAndResult["RM"] = RockSprite;
        RecipeAndResult["MR"] = MushroomSprite;
        RecipeAndResult["WW00"] = StickSprite;
        RecipeAndResult["R00Ws"] = AxeSprite;
        RecipeAndResult["Ws00R"] = SpearSprite;
        RecipeAndResult["FlourFlour00"] = BreadSprite;

        // build number-required recipe
        if (MinMaterialNumber >= 3 && CountExist > 1)
        {
            RecipeAndResult["RyeRye00"] = FlourSprite;
            //int ResultQuantity = (int) (MinMaterialNumber / 3);
            //CraftResultNumber.text = ResultQuantity.ToString();
        } else
        {
            RecipeAndResult["RyeRye00"] = null;
        }
    }

    public void BuildRecipeName()
    {
        // build simple name
        RecipeAndName["W000"] = "";
        RecipeAndName["0W00"] = "";
        RecipeAndName["00W0"] = "";
        RecipeAndName["000W"] = "";
        RecipeAndName["M000"] = "";
        RecipeAndName["0M00"] = "";
        RecipeAndName["00M0"] = "";
        RecipeAndName["000M"] = "";
        RecipeAndName["R000"] = "";
        RecipeAndName["0R00"] = "";
        RecipeAndName["00R0"] = "";
        RecipeAndName["000R"] = "";

        RecipeAndName["WW00"] = "Wooden Stick";
        RecipeAndName["R00Ws"] = "Stone Axe";
        RecipeAndName["Ws00R"] = "Stone Spear";
        RecipeAndName["FlourFlour00"] = "Bread";

        // number-required recipe
        RecipeAndName["RyeRye00"] = "";
        if (MinMaterialNumber >= 3 && CountExist > 1)
        {
            RecipeAndName["RyeRye00"] = "Flour";
        }
    }

    public void SetCraftItemResult()
    {
        BuildRecipe();
        if (GetFinalRecipe().Length > 1) 
        {
            CraftItemResult.sprite = RecipeAndResult[GetFinalRecipe()];
            if (CraftItemResult.sprite != null)
            {
                CraftItemResult.color = new Color(255, 255, 255, 255);
            } else
            {
                CraftItemResult.color = new Color(255, 255, 255, 0);
            }
        }
    }

    public bool IsContained(string[] arr, string str)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == str)
            {
                return true;
            }
        }
        return false;
    }

    public void DecreaseNumberItem(int index)
    {
        inventoryManager.DecreaseOneItemForCraft(index);
        CraftItemQuantity[index]--;
    } 

    public void ManageDragItemToCraftSlot(int SlotIndex)
    {
        CraftSlotQuantity[SlotIndex]++;
        CraftSlotItemNumber[SlotIndex].text = CraftSlotQuantity[SlotIndex].ToString();
        MinMaterialNumber = GetMinMaterialNumber();
    }
    
    public int GetNumberCraftResult()
    {
        string[] NumberRequiredRecipe = { "RyeRye00" };
        string str = GetFinalRecipe();
        int Result = 0;
        if (CraftItemResult.sprite != null && !IsContained(NumberRequiredRecipe, str))
        {
            Result = CraftSlotQuantity[0];
            for (int i = 0; i < CraftSlotQuantity.Length; i++)
            {
                if (CraftSlotQuantity[i] != 0 && CraftSlotQuantity[i] < Result)
                {
                    Result = CraftSlotQuantity[i];
                }
            }
        } else if (CraftItemResult.sprite != null && IsContained(NumberRequiredRecipe, str))
        {
            switch (GetFinalRecipe())
            {
                case "RyeRye00":
                    Result = (int)(MinMaterialNumber / 3);
                    break;
            }
        }
        CraftResultNumber.text = Result.ToString();
        return Result;
    }

    public int CountExistMaterial()
    {
        int cnt = 0;
        for (int i = 0; i < CraftSlotQuantity.Length; ++i)
        {
            if (CraftSlotQuantity[i] != 0)
            {
                ++cnt;
            }
        }
        return cnt;
    }
    
    public int GetMinMaterialNumber()
    {
        int Result = 100;
        for (int i = 0; i < CraftSlotQuantity.Length; ++i)
        {
            if (CraftSlotQuantity[i] != 0 && CraftSlotQuantity[i] < Result)
            {
                Result = CraftSlotQuantity[i];
            }
        }
        return Result;
    }

    public void CopySprite(int index)
    {
        Image[] InventoryIconsArray = inventoryManager.GetInventoryIconsArray();
        CraftItemIcon[index] = InventoryIconsArray[index];
    }

    public bool CheckEmpty(int index)
    {
        return CraftItemQuantity[index] == 0;
    }

    public void HandleWhenEmptySlot(int index)
    {
        inventoryManager.HandleEmpty(index);
    }

    public void RefreshWhenCloseCraftScreen()
    {
        for (int i = 0; i < CraftSlotIcon.Length; ++i)
        {
            CraftSlotIcon[i].sprite = null;
            CraftSlotIcon[i].color = new Color(255, 255, 255, 0);
            CraftSlotQuantity[i] = 0;
            CraftSlotItemNumber[i].text = CraftSlotQuantity[i].ToString();
        }
        CraftItemResult.sprite = null;
        CraftItemResult.color = new Color(255, 255, 255, 0);
        CraftResultNumber.text = "0";

        for (int i = 0; i < CraftRecipe.Length; ++i)
        {
            CraftRecipe[i] = "0";
        }
    }

    public void ReturnOldValueToCraftWhenDisplay()
    {
        inventoryManager.ReturnOldQuantityWhenNotDrop();
    }

    public string GetCraftResultName()
    {
        BuildRecipeName();
        return RecipeAndName[GetFinalRecipe()];
    }

    public void HandleCrafting()
    {
        int NumberResultBeforeCrafting = 0;
        if (GetFinalRecipe() == "RyeRye00")
        {
            NumberResultBeforeCrafting = 3 * GetNumberCraftResult();
        } else
        {
            NumberResultBeforeCrafting = GetNumberCraftResult();    
        }
        for (int i = 0; i < CraftSlotQuantity.Length; ++i)
        {
            if (CraftSlotQuantity[i] != 0)
            {
                CraftSlotQuantity[i] -= NumberResultBeforeCrafting;
            }
            CraftSlotItemNumber[i].text = CraftSlotQuantity[i].ToString(); 
        }
        for (int i = 0; i < CraftSlotQuantity.Length; ++i)
        {
            if (CraftSlotQuantity[i] == 0)
            {
                CraftSlotIcon[i].sprite = null;
                CraftSlotIcon[i].color = new Color(255, 255, 255, 0);
                CraftRecipe[i] = "0";
            }
        }
        CraftItemResult.sprite = null;
        CraftItemResult.color = new Color(255, 255, 255, 0);
        CraftResultNumber.text = "0";
    }
}
