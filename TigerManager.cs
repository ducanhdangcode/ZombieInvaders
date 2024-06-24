using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class TigerManager : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float speed = 5f;
    public float moveTime = 10f;
    public float stopTime = 3f;

    [Header("Game Object Parameters")]
    public GameObject Tiger;
    public GameObject Player;
    public GameObject TigerInfo;

    [Header("Nav Mesh Parameters")]
    public NavMeshAgent agent;

    float counterTime;
    float stopCounterTime;
    bool isMove;

    int direction;

    public bool isHit = false;

    private void Start()
    {
        isMove = false;
        //moveTime = Random.Range(5f, 10f);
        counterTime = moveTime;
        direction = Random.Range(0, 4);
    }

    void Update()
    {
        if (!CheckDistanceTrace(Tiger.transform, Player.transform))
        {
            if (counterTime > 0)
            {
                SetDirection(direction);
                counterTime -= Time.deltaTime;
            }
            else
            {
                Tiger.GetComponent<Animator>().SetBool("isMove", false);
                direction = Random.Range(0, 4);
                Invoke(nameof(ManageMoveAfterStop), 3f);
            }
            TigerInfo.SetActive(false);
        } else
        {
            ManageTracing();
            TigerInfo.SetActive(true);
        }
        ManageHit();
    }

    private void SetDirection(int val)
    {
        Tiger.GetComponent<Animator>().SetBool("isMove", true);
        switch (val)
        {
            case 0:
                // move to the top -> increase z axis
                Tiger.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                Tiger.transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
                break;
            case 1:
                // move to the right -> increase x axis
                Tiger.transform.localRotation = Quaternion.Euler(0f, 90f, 0);
                Tiger.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                break;
            case 2:
                // move to the right -> decrease x axis
                Tiger.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                Tiger.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
                break;
            case 3:
                // move down -> decrease z axis
                Tiger.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                Tiger.transform.position -= new Vector3(0f, 0f, speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void ManageMoveAfterStop()
    {
        counterTime = moveTime;
    }

    private bool CheckDistanceTrace(Transform Tiger, Transform Player)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Tiger.position.x - Player.position.x, 2)
            + Mathf.Pow(Tiger.position.z - Player.position.z, 2));
        return distance <= 20;
    }

    private bool CheckDistanceHit(Transform Tiger, Transform Player)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(Tiger.position.x - Player.position.x, 2)
            + Mathf.Pow(Tiger.position.z - Player.position.z, 2));
        return distance <= 5;
    }

    private void ManageTracing()
    {
        StartCoroutine(Tracing());
    }    
    
    IEnumerator Tracing()
    {
        yield return new WaitForSeconds(1f);
        agent.SetDestination(Player.transform.position);
        Tiger.GetComponent<Animator>().SetBool("isMove", true);
    }

    private void ManageHit()
    {
        if (CheckDistanceHit(Tiger.transform, Player.transform))
        {
            Tiger.GetComponent<Animator>().SetBool("isHit", true);
            isHit = true;
        } else
        {
            Tiger.GetComponent<Animator>().SetBool("isHit", false);
            isHit = false;
        }
    }
}
