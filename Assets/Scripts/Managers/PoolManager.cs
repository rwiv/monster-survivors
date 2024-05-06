using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    
    // 프리팹들
    public GameObject[] prefabs;

    // 하나의 원소는 prefabs[idx] 프리팹 인스턴스 GameObject들의 pool
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index=0;index<pools.Length;index++)
        {
            pools[index] = new List<GameObject>();
        }
    }
    
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀의 놀고 있는 (비활성화) 게임 오브젝트 접근
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select=item;
                select.SetActive(true);
                break;
            }
        }

        // 못 찾았으면?
        if (!select)
        {
            // ... 새롭게 생성하고 select 변수에 할당
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
