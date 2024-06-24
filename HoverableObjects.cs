using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverableObjects : MonoBehaviour
{
    [Header("String Parameters")]
    public string displayName = "";
    public string displayHint = "";

    public string getName()
    {
        return displayName;
    }

    public string getHint()
    {
        return displayHint;
    }

}
