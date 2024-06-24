using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubStoneManager : MonoBehaviour
{
    [Header("Animator Parameters")]
    public Animator LeftStoneAnimator;
    public Animator RightStoneAnimator;

    [Header("Game Object Parameters")]
    public GameObject StoneLeft;
    public GameObject StoneRight;

    private void Update()
    {
        if (StoneLeft.activeSelf &&  StoneRight.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                RubStone();
            }
        }
    }

    private void RubStone()
    {
        LeftStoneAnimator.SetBool("isRub", true);
        RightStoneAnimator.SetBool("isRub", true);
        Invoke(nameof(StopRub), 3f);
    }

    private void StopRub()
    {
        LeftStoneAnimator.SetBool("isRub", false);
        RightStoneAnimator.SetBool("isRub", false);
    }
}
