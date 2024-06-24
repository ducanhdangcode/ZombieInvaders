using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStickAttack : MonoBehaviour
{
    [Header("Tiger Parameters")]
    public GameObject TigerHealthBarImage;
    public GameObject Tiger;
    public GameObject TigerInfoScreen;

    [Header("Spawn Meat Parameters")]
    public SpawnMeatManager spawnMeatManager;

    private Animator StickAnimator;

    private void Start()
    {
        StickAnimator = GetComponent<Animator>();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && gameObject.activeSelf == true)
        {
            StartCoroutine(Hitting());
        }
    }

    IEnumerator Hitting()
    {
        StickAnimator.SetBool("isHit", true);
        yield return new WaitForSeconds(1f);
        StickAnimator.SetBool("isHit", false);
    }

    private void ManageChangeHealthBarTiger()
    {
        TigerHealthBarImage.GetComponent<RectTransform>().position -= new Vector3(3f, 0, 0);
        TigerHealthBarImage.GetComponent<RectTransform>().localScale -= new Vector3(0.03f, 0, 0);
        if (TigerHealthBarImage.GetComponent<RectTransform>().localScale == new Vector3(0, 1, 1))
        {
            ManageTigerDead();
            spawnMeatManager.SpawnTigerMeat();
        }
    }

    private void ManageTigerDead()
    {
        Destroy(Tiger);
        TigerInfoScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tiger"))
        {
            ManageChangeHealthBarTiger();
        }
    }
}
