using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChopTreeManager : MonoBehaviour
{
    [Header("Manage Hover UI Parameters")]
    public ManageHoverUI manageHoverUI;

    [Header("Game Object Parameters")]
    public GameObject EquipAxe;
    public GameObject HandleChoppingProcessScreen;
    public GameObject ChoppingResultText;

    [Header("Animator Parameters")]
    public Animator AxeAnimator;

    [Header("Slider Parameters")]
    public Slider ChopProcessingSlider;

    [Header("Inventory Manager Parameters")]
    public InventoryManager inventoryManager;

    private bool IsChopping;

    private void Start()
    {
        IsChopping = false;
    }

    private void Update()
    {
        if (manageHoverUI.CheckReadyToChopTree() && Input.GetKeyDown(KeyCode.Q) && IsChopping == false)
        {
            Debug.Log("Chop Processing...");
            AxeAnimator.SetBool("IsChop", true);
            StartCoroutine(Chopping());
            IsChopping = true;
        } else if (Input.GetKeyDown(KeyCode.Q) && IsChopping == true)
        {
            StopAllCoroutines();
            AxeAnimator.SetBool("IsChop", false);
            HandleChoppingProcessScreen.SetActive(false);
            manageHoverUI.RedisplayWhenInterruptChopping();
            IsChopping = false;
        }
    }

    IEnumerator Chopping()
    {
        manageHoverUI.DisableTextWhenChop();
        HandleChoppingProcessScreen.SetActive(true);

        yield return new WaitForSeconds(1f);

        while (ChopProcessingSlider.value < 1)
        {
            ChopProcessingSlider.value += 0.15f;
            yield return new WaitForSeconds(0.8f);
        }

        if (ChopProcessingSlider.value >= 1f)
        {
            ChopProcessingSlider.value = 1f;
            manageHoverUI.DestroyWhenFinishChop();
            HandleChoppingProcessScreen.SetActive(false);
            AxeAnimator.SetBool("IsChop", false);
            ChoppingResultText.SetActive(true);

            yield return new WaitForSeconds(1f);
            ChoppingResultText.SetActive(false);

            inventoryManager.AddWoodFromChoppingTree();

            ChopProcessingSlider.value = 0f;
        }
    }
}
