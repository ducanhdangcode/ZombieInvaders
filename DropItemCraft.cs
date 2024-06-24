using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropItemCraft : MonoBehaviour, IDropHandler
{
    public int Index;
    public CraftManager craftManager;

    string RecipeSign = "";

    private int[] ItemQuantity = new int[4];

    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        if (eventData.pointerDrag != null)
        {
            Sprite sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            if (sprite != null)
            {
                if (GetComponent<Image>().sprite == null || (GetComponent<Image>().sprite != null && GetComponent<Image>().sprite == sprite))
                {
                    eventData.pointerDrag.SetActive(false);
                    string ItemName = eventData.pointerDrag.GetComponent<HandleItemCraft>().GetItemDropName(eventData.pointerDrag.GetComponent<HandleItemCraft>().Index);
                    GetComponent<Image>().sprite = sprite;
                    GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    switch (ItemName)
                    {
                        case "Rock":
                            RecipeSign = "R";
                            break;
                        case "Mushroom":
                            RecipeSign = "M";
                            break;
                        case "Wood":
                            RecipeSign = "W";
                            break;
                        case "Wooden Stick":
                            RecipeSign = "Ws";
                            break;
                        case "Rye":
                            RecipeSign = "Rye";
                            break;
                        case "Flour":
                            RecipeSign = "Flour";
                            break;
                        default:
                            break;
                    }
                    craftManager.SetRecipe(Index, RecipeSign);
                    Debug.Log(craftManager.GetFinalRecipe());
                    craftManager.ManageDragItemToCraftSlot(Index);
                    craftManager.SetCraftItemResult();
                    craftManager.GetNumberCraftResult();
                    eventData.pointerDrag.GetComponent<HandleItemCraft>().DecreaseItemWhenDrop();
                    eventData.pointerDrag.GetComponent<HandleItemCraft>().SetPosition();
                    eventData.pointerDrag.GetComponent<HandleItemCraft>().HandleWhenEmpty();
                    eventData.pointerDrag.SetActive(true);
                } else
                {
                    eventData.pointerDrag.GetComponent<HandleItemCraft>().SetPosition();
                }
            }
        }
    }
}
