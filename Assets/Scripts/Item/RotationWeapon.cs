using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWeapon : Weapon2
{
    float range;
    
    private void Awake()
    {
        range = 2f;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

    public void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);
        Batch();
    }
    
    public void Init(ItemData data)
    {
        base.Init(data);
        speed = 150;
        Batch();
    }

    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            
            bullet.parent = transform;

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //회전 코드
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * range, Space.World);

            bullet.GetComponent<Projectile>().Init(damage, -100); // -100 is Infinity Per.
        }
    }
}
