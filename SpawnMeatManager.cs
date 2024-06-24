using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeatManager : MonoBehaviour
{
    [Header("Game Object Parameters")]
    public GameObject TigerMeat;
    public GameObject Player;
    public GameObject RabbitMeat;

    public void SpawnTigerMeat()
    {
        Instantiate(TigerMeat, new Vector3(Player.transform.position.x + 0.5f, 0.1f, Player.transform.position.z + 0.5f), Quaternion.identity);
    }

    public void SpawnRabbitMeat()
    {
        Instantiate(RabbitMeat, new Vector3(Player.transform.position.x + 0.5f, 0.1f, Player.transform.position.z + 0.5f), Quaternion.identity);
    }
}
