using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPickupObject : MonoBehaviour
{
    // script to define can pickup object
    public enum TypesOfItem
    {
        Stone, 
        Rock, 
        Leaves, 
        TigerMeat,
        Mushroom,
        Wood,
        WoodenStick,
        StoneAxe,
        StoneSpear,
        Rye,
        Flour,
        Bread,
        RabbitMeat,
    }

    public TypesOfItem ItemType;

    public float GetWeight()
    {
        float weight = 0;
        switch(ItemType)
        {
            case TypesOfItem.Rock:
                weight = 1f;
                break;
            case TypesOfItem.Mushroom:
                weight = 0.4f;
                break;
            default:
                break;
        }
        return weight;
    }
}
