using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManagement : MonoBehaviour
{
    [Header("Game Object Parameters")]
    public GameObject DropPartlyScreen;

    [Header("Boolean Parameters")]
    public bool IsDisplay = false;

    public void DisplayDropPartlyScreen()
    {
        if (IsDisplay == false)
        {
            DropPartlyScreen.SetActive(true);
            IsDisplay = true;
        } else
        {
            DropPartlyScreen.SetActive(false);
            IsDisplay = false;
        }
    }
}
