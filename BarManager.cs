using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    [Header("Health Bar Parameters")]
    public GameObject HealthBar;
    public GameObject HungerBar;

    [Header("Game Object Parameters")]
    public GameObject DamageScreen;

    private bool isDamage = false;

    private float CounterTime;

    private float HungerTime = 10f;

    private void Start()
    {
        CounterTime = 0;
    }

    private void Update()
    {
        if (CounterTime < HungerTime)
        {
            CounterTime += Time.deltaTime;
        } else
        {
            HandleHunger(9f, 0.06f);
            CounterTime = 0;
        }
    }

    public void GetDamage(float res, float _res)
    {
        HealthBar.GetComponent<RectTransform>().localPosition -= new Vector3(res, 0, 0);

        HealthBar.GetComponent<RectTransform>().localScale -= new Vector3(_res, 0, 0);

        StartCoroutine(DisplayDamageScreen());
    }

    IEnumerator DisplayDamageScreen()
    {
        DamageScreen.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        DamageScreen.SetActive(false);
    }

    public void IncreaseHungerBarWhenEating(float res, float _res)
    {
        HungerBar.GetComponent<RectTransform>().localPosition += new Vector3(res, 0, 0);
        HungerBar.GetComponent<RectTransform>().localScale += new Vector3(_res, 0, 0);
        if (HungerBar.GetComponent<RectTransform>().localScale.x > 1f)
        {
            float tmp = HungerBar.GetComponent<RectTransform>().localScale.x - 1f;
            HungerBar.GetComponent<RectTransform>().localPosition -= new Vector3(150 * tmp, 0, 0);
        }
    }

    private void HandleHunger(float res, float _res)
    {
        HungerBar.GetComponent<RectTransform>().localPosition -= new Vector3(res, 0, 0);
        HungerBar.GetComponent<RectTransform>().localScale -= new Vector3(_res, 0, 0);
    }
}
