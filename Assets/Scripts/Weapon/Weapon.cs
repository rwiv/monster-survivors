using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;
    
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    
    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    
        // ..Test Code..
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
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
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir); 
    }
    
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
    
        if(id == 0)
        {
            Batch();
        }
    }
    
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
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
            bullet.Translate(bullet.up * 1.5f, Space.World);

            // 근거리 무기일 때
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 is Infinity Per.
        }
    }
}
