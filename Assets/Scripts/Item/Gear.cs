using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.position = Vector3.zero;

        // Property Set
        type = data.itemType;
        rate = data.coefs[0];
        ApplyGear();
    }
    
    public void Levelup(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        float deafult = GameManager.instance.defaultWeaponSpeedRate;
        GameManager.instance.weaponSpeedRate = deafult + (deafult * rate);
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        
        foreach(Weapon weapon in weapons)
        {
            weapon.InitSpeed();
        }
    }
    
    void SpeedUp()
    {
        GameManager.instance.speed = GameManager.instance.defaultSpeed * (1f + rate);
    }
}
