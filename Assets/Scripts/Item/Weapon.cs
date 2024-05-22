using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float baseSpeed;
    public float speed;
    public float coef;

    float range;
    float timer;
    Player player;
    
    private void Awake()
    {
        player = GameManager.instance.player;
        range = 3f;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
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
        bullet.GetComponent<Bullet>().Init(damage, count, dir, 15f); 
    }

    public void LevelUp(float damage, int count, float coef, float speed)
    {
        this.damage = damage;
        this.count += count;
        this.coef = coef;
        baseSpeed = GetSpeed(baseSpeed, speed);
        InitSpeed();
    
        if(id == 0)
        {
            Batch();
        }
        else if (id == 5)
        {
            BatchRange(coef);
        }
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    
    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        coef = data.baseCoef;
        for(int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        
        baseSpeed = data.baseSpeed;
        InitSpeed();

        switch (id)
        {
            case 0:
                Batch();
                break;
            case 1:
                break;
            case 5:
                BatchRange(coef);
                break;
        }
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void InitSpeed()
    {
        float rate = GameManager.instance.weaponSpeedRate;
        switch (id)
        {
            case 0:
                speed = baseSpeed * rate;
                break;
            case 1:
                speed = baseSpeed / rate;
                break;
        }
    }
    
    
    float GetSpeed(float speed, float rate)
    {
        switch (id)
        {
            case 0:
                return speed * rate;
            case 1:
                return speed / rate;
            default:
                return speed;
        }
    }

    void BatchRange(float coef)
    {
        GameObject bullet = GameManager.instance.pool.Get(prefabId);
        bullet.GetComponent<RangeBullet>().Init(damage, coef);
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
