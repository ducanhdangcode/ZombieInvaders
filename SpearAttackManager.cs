using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpearAttackManager : MonoBehaviour
{
    [Header("Game Object Parameters")]
    public GameObject EquipSpear;

    [Header("Tiger Parameters")]
    public GameObject TigerHealthBarImage;
    public GameObject Tiger;
    public GameObject TigerInfoScreen;

    [Header("Animator Parameters")]
    public Animator SpearAnimator;

    [Header("Spawn Meat Parameters")]
    public SpawnMeatManager spawnMeatManager;

    private void Update()
    {
        if (gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(ManageAttacking());
        }
    }

    IEnumerator ManageAttacking()
    {
        SpearAnimator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(1.2f);
        SpearAnimator.SetBool("IsAttack", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tiger"))
        {
            Debug.Log("You have collided the tiger!");
            ManageChangeHealthBarTiger();
        }
    }

    private void ManageChangeHealthBarTiger()
    {
        TigerHealthBarImage.GetComponent<RectTransform>().position -= new Vector3(10f, 0, 0);
        TigerHealthBarImage.GetComponent<RectTransform>().localScale -= new Vector3(0.1f, 0, 0);
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
}
