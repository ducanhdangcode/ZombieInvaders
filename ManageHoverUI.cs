using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageHoverUI : MonoBehaviour
{
    [Header("Game Object Parameters")]
    public GameObject DisplayUIName;
    public GameObject DisplayUIHint;
    public GameObject Player;
    public GameObject EquipAxe;
    public GameObject ItemText;

    TextMeshProUGUI displayUINameText;
    TextMeshProUGUI displayUIHintText;

    private const string ReadyToChopText = "Press Q to chop the tree!";
    private string[] TreeName = { "[Birch Tree]", "[Oak Tree]", "[Deciduous Tree]" };
    private Transform CopyHit;

    private void Start()
    {
        displayUINameText = DisplayUIName.GetComponent<TextMeshProUGUI>();
        displayUIHintText = DisplayUIHint.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            CopyHit = hit.transform;

            if (selectionTransform.GetComponent<HoverableObjects>()) 
            {
                if (CheckDistance(hit.transform))
                {
                    displayUINameText.text = selectionTransform.GetComponent<HoverableObjects>().getName();
                    displayUIHintText.text = selectionTransform.GetComponent<HoverableObjects>().getHint();
                    DisplayUIName.SetActive(true);
                    DisplayUIHint.SetActive(true);
                    if (EquipAxe.activeSelf == true && CheckTreeName(displayUINameText.text))
                    {
                        displayUIHintText.text = ReadyToChopText;
                    }
                } 
            } else
            {
                DisplayUIName.SetActive(false);
                DisplayUIHint.SetActive(false);
            }
        }
    }

    private bool CheckDistance(Transform hit)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(hit.position.x - Player.transform.position.x, 2)
            + Mathf.Pow(hit.transform.position.z - Player.transform.position.z, 2));
        if (distance > 50f)
        {
            return false;
        }
        return true;
    }

    public bool CheckTreeName(string value)
    {
        for (int i = 0; i < TreeName.Length; ++i)
        {
            if (value == TreeName[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckReadyToChopTree()
    {
        return CheckTreeName(displayUINameText.text) && EquipAxe.activeSelf;
    }

    public void DisableTextWhenChop()
    {
        ItemText.SetActive(false);
    }

    public void RedisplayWhenInterruptChopping()
    {
        ItemText.SetActive(true);
    }

    public void DestroyWhenFinishChop()
    {
        Destroy(CopyHit.gameObject);
        ItemText.SetActive(true);
    }
}
