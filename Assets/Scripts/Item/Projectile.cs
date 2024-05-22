using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public float damage;
    public int per;

    public void Init(float daamage, int per)
    {
        this.damage = daamage;
        this.per = per; 
    } 
}
