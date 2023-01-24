using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float cooldownTime = 1.0f;
    [SerializeField] protected int Damage;

    [Header("HP")]
    [SerializeField] protected int maxHP;
    [SerializeField] protected int heal;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed;

    [Header("Loot")]
    [SerializeField] protected List<GameObject> lootList;
    protected int HP;
    protected float distanceToPlayer;
    private bool isOnCooldown;
    protected bool isAwake = false;
    protected bool isAttacking = false;
    private Slider barHP;
    protected GameObject player;
    protected Rigidbody2D rb2D;


    // Start is called before the first frame update
    protected virtual void Setup()
    {
        this.gameObject.tag = "Enemy";
        HP = maxHP;
        isOnCooldown = false;
        if (heal > 0) StartCoroutine(Heal());

        barHP = GetComponentInChildren<Slider>();
        barHP.maxValue = maxHP;
        barHP.value = HP;

        player = GameObject.Find("Player");
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected bool PlayerCheck()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return false;
        }
        return true;
    }

    protected void AwakeDistanceCheck(float activationValue)
    {
        distanceToPlayer = Vector2.Distance(rb2D.transform.position, player.transform.position);
        if (distanceToPlayer < activationValue)
        {
            isAwake = true;
        }
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

    /// <summary> 
    ///     Put at the end if there are any actions unique to specific enemy
    /// </summary>
    public virtual void OnDie()
    {
        Destroy(this.gameObject);
        if(lootList.Count <= 0) return;
        GameObject.Instantiate(lootList[Random.Range(0, lootList.Count)], transform.position, Quaternion.identity);
    }
}
