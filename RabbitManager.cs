using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitManager : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float speed = 0.2f;
    public float moveTime = 10f;
    public float stopTime = 3f;

    [Header("Game Object Parameters")]
    public GameObject Player;
    public GameObject RabbitInfo;
    public GameObject RabbitHealthBarImage;

    [Header("Spawn Meat Manager")]
    public SpawnMeatManager spawnMeatManager;

    private float counterTime;
    private int direction;

    private Animator animator;

    private void Start()
    {
        counterTime = moveTime;
        direction = Random.Range(0, 4);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (counterTime > 0)
        {
            animator.SetBool("isRun", true);
            SetDirection(direction);
            counterTime -= Time.deltaTime;
        } else
        {
            animator.SetBool("isRun", false);
            direction = Random.Range(0, 4);
            Invoke(nameof(ManageMoveAfterStop), 3f);
        }

        if (CheckDistance())
        {
            RabbitInfo.SetActive(true);
        } else
        {
            RabbitInfo.SetActive(false);
        }
    }

    private void SetDirection(int val)
    {
        switch (val)
        {
            case 0:
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
                break;
            case 1:
                transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                break;
            case 2:
                transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
                break;
            case 3:
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void ManageMoveAfterStop()
    {
        counterTime = moveTime;
    }

    private bool CheckDistance()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2)
            + Mathf.Pow(transform.position.z - Player.transform.position.z, 2));
        return distance <= 20;
    }

    public void GetDamage(float delta, float scale)
    {
        RabbitHealthBarImage.GetComponent<RectTransform>().position -= new Vector3(delta, 0f, 0f);
        RabbitHealthBarImage.GetComponent<RectTransform>().localScale -= new Vector3(scale, 0f, 0f);
        if (RabbitHealthBarImage.GetComponent<RectTransform>().localScale.x <= 0f) 
        {
            ManageRabbitDead();
            spawnMeatManager.SpawnRabbitMeat();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spear"))
        {
            Debug.Log("Collided");
            GetDamage(30f, 0.3f);
            InDangerMode();
        }
    }

    private void ManageRabbitDead()
    {
        Destroy(gameObject);
        RabbitInfo.SetActive(false);
    }

    private void InDangerMode()
    {
        speed = 2.5f;
        animator.SetBool("isDanger", true);
        Invoke(nameof(CalmAfterDanger), 3f);
    }

    private void CalmAfterDanger()
    {
        speed = 1f;
        animator.SetBool("isDanger", false);
    }
}
