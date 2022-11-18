using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected int HP;
    [SerializeField] protected int Damage;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int heal;
    [Space(10)]
    [SerializeField] private float cooldownTime = 1.0f;
    Slider barHP;

    private bool isOnCooldown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.gameObject.tag = "Enemy";
        HP = maxHP;
        isOnCooldown = false;
        if (heal > 0)
        {
            StartCoroutine(Heal());
        }

        barHP = GetComponentInChildren<Slider>();
        barHP.maxValue = maxHP;
        barHP.value = HP;
    }

    private IEnumerator Heal()
    {
        yield return new WaitForSeconds(4);
        if (HP != maxHP)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                //temp
                spriteRenderer.color = Color.green;

                yield return new WaitForSeconds(0.5f);
                OnHeal();

                //temp
                spriteRenderer.color = Color.white;
            }
            else
            {
                OnHeal();
            }
        }

        StartCoroutine(Heal());
    }

    public void OnHeal()
    {
        if (HP + heal > maxHP)
        {
            HP = maxHP;
            barHP.value = HP;
        }
        else
        {
            HP += heal;
            barHP.value = HP;
        }
    }

    public void OnReceiveDamage(int Damage)
    {
        if(isOnCooldown) return;

        StartCoroutine(DamageCooldown());

        HP -= Damage;
        barHP.value = HP;
        if (HP <= 0)
        {
            OnDie();
        }
    }

    private IEnumerator DamageCooldown()
    {
        //temp
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        isOnCooldown = true;

        //temp
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(cooldownTime);

        //temp
        spriteRenderer.color = Color.white;

        isOnCooldown = false;
    }

    public abstract void OnDie();
}
