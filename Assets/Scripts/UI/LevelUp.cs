using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }
    
    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
    }
    
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
    
    public void Select(int index)
    {
        items[index].OnClick();
    }
    
    void Next()
    {
        List<Item> pools = new List<Item>();
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
            if (item.level != item.data.damages.Length)
            {
                pools.Add(item);
            }
        }

        List<Item> targets = new List<Item>();
        if (pools.Count <= 3)
        {
            foreach (Item item in pools)
            {
                targets.Add(item);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                int ranIdx = Random.Range(0, pools.Count);
                targets.Add(pools[ranIdx]);
                pools.RemoveAt(ranIdx);
            }
        }

        foreach (Item target in targets)
        {
            target.gameObject.SetActive(true);
        }
    }
}
