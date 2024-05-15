using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : MonoBehaviour
{
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    protected Player player;
    
    private void Awake()
    {
        player = GameManager.instance.player;
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
    
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        damage = data.baseDamage;
        count = data.baseCount;
        for(int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
}
