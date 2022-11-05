using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected int HP;
    [SerializeField] protected int maxHP;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReceiveDamage(int Damage)
    {
        HP -= Damage;
        if (HP <= 0)
        {
            OnDie();
        }
    }

    public abstract void OnDie();
}
