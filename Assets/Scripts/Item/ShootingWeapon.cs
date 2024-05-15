using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : Weapon2
{
    float timer;

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        
        timer += Time.deltaTime;
        if (timer > speed)
        {
            timer = 0f;
            Fire();
        }
    }

    public void Init(ItemData data)
    {
        base.Init(data);
        // Basic Set
        speed = 0.3f;
    }
    
    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;
    
        Vector3 targetPso = player.scanner.nearestTarget.position;
        Vector3 dir = (targetPso - transform.position).normalized;
        dir = dir.normalized;
    
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        // bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir); 
    }
    
}
