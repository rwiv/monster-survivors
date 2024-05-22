using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName ="Scriptble Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee,Range,Glove,Shoe,Heal,Flare }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Info")]
    public float baseDamage;
    public int baseCount;
    public float baseCoef;
    public float baseSpeed;
    public float[] coefs;
    public float[] damages;
    public int[] counts;
    public float[] speeds;


    [Header("# Weapon")]
    public GameObject projectile;

}
